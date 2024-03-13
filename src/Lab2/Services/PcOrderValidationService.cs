using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services;

public class PcOrderValidationService : IPcOrderValidationService
{
    private const double PowerSupplyMinBoundMultiplier = 0.9d;

    public PcOrderValidationResult Validate(IPcOrder order)
    {
        PcOrderValidationException.ThrowIfNull(order);

        var incompatibleMessages = new List<string>();
        var warrantyCancellationMessages = new List<string>();
        var importantNoteMessages = new List<string>();

        Power totalConsumption = TotalConsumption(order);

        if (!order.PcCase.SupportedMotherBoardFormFactors.Contains(order.MotherBoard.FormFactor))
        {
            incompatibleMessages.Add("Motherboard Form Factor not supported by the case");
        }

        if (!order.MotherBoard.Socket.Equals(order.CentralProcessor.Socket))
        {
            incompatibleMessages.Add("Motherboard and CPU sockets differ");
        }

        if (!order.MotherBoard.Bios.SupportedProcessors.Contains(order.CentralProcessor))
        {
            incompatibleMessages.Add("CPU unsupported by BIOS");
        }

        if (!order.RamComponents
                .All(ram => order.MotherBoard.SupportedMemoryFormFactor.Equals(ram.FormFactor)
                            && order.MotherBoard.DdrStandard.Equals(ram.DdrStandard)))
        {
            incompatibleMessages.Add("RAM unsupported by motherboard");
        }

        if (order.MotherBoard.RamSlots.Number < order.RamComponents.Count)
        {
            incompatibleMessages.Add("Not enough slots for all RAMs");
        }

        if (!order.RamComponents
                .All(ram => ram.JedecFrequenciesInfo
                                .Any(kvp => order.MotherBoard.JedecFrequencies.Contains(kvp.Key))
                            || ram.JedecFrequenciesInfo.MaxBy(kvp => kvp.Key).Key <
                            order.MotherBoard.JedecFrequencies.Max()))
        {
            incompatibleMessages.Add("Motherboard JEDEC setting incompatible with RAM JEDEC settings");
        }

        if (!order.RamComponents
                .All(ram => ram.JedecFrequenciesInfo.MinBy(kvp => kvp.Key).Key <=
                            order.CentralProcessor.MaxSupportedRamFrequency))
        {
            incompatibleMessages.Add("CPU doesn't support RAMs JEDEC frequencies");
        }

        if (order.PowerSupply.PeakLoad.Watts < PowerSupplyMinBoundMultiplier * totalConsumption.Watts)
        {
            incompatibleMessages.Add("Power supply is not powerful enough");
        }

        if (order.MotherBoard.HasIntegratedWiFiAdapter && order.WiFiAdapter is not null)
        {
            incompatibleMessages.Add("Wi-Fi adapters conflict");
        }

        if (order.PcCase.CoolingSystemMaxSize.HeightMm < order.CpuCoolingSystem.Size.HeightMm)
        {
            incompatibleMessages.Add("Not enough place for CPU cooling system in the case");
        }

        if (order.GraphicsCards
            .Any(card => (order.PcCase.GraphicsCardMaxSize.HeightMm < card.Size.HeightMm ||
                          order.PcCase.GraphicsCardMaxSize.WidthMm < card.Size.WidthMm)))
        {
            incompatibleMessages.Add("Not enough place for GPUs in the case");
        }

        if (order.MotherBoard.SataPorts.Number < TotalSataSlotsUsed(order).Number)
        {
            incompatibleMessages.Add("Not enough SATA ports in motherboard");
        }

        if (TotalPciESlotsUsed(order)
            .Any(keyValuePair => !order.MotherBoard.PciEInfo.ContainsKey(keyValuePair.Key) ||
                                 order.MotherBoard.PciEInfo[keyValuePair.Key].Number <
                                 keyValuePair.Value.Number))
        {
            incompatibleMessages.Add("Not enough PCI-E ports in motherboard");
        }

        if (incompatibleMessages.Count != 0)
        {
            return new PcOrderIncompatibleBuild(order, incompatibleMessages);
        }

        if (order.CpuCoolingSystem.MaxHeatDissipation.Watts < order.CentralProcessor.ThermalDesignPower.Watts)
        {
            warrantyCancellationMessages.Add("CPU cooling system is not powerful enough");
        }

        if (warrantyCancellationMessages.Count != 0)
        {
            return new PcOrderWarrantyCancellation(order, warrantyCancellationMessages);
        }

        if (order.PowerSupply.PeakLoad.Watts < totalConsumption.Watts)
        {
            importantNoteMessages.Add("Power supply is not powerful as recommended");
        }

        if (!order.RamComponents
                .All(ram => ram.OverclockedMemoryProfiles.Count == 0
                            || ram.OverclockedMemoryProfiles.Any(xmp =>
                                order.MotherBoard.OverclockedFrequencies.Contains(xmp.Frequency))))
        {
            importantNoteMessages.Add("None of overclocked profiles of 1 or more RAMs is supported by motherboard");
        }

        if (!order.RamComponents
                .All(ram => ram.OverclockedMemoryProfiles.Count == 0
                            || ram.OverclockedMemoryProfiles
                                .MinBy(xmp =>
                                    xmp.Frequency)
                                ?.Frequency <= order.CentralProcessor.MaxSupportedRamFrequency))
        {
            importantNoteMessages.Add("None of overclocked profiles of 1 or more RAMs is supported by CPU");
        }

        if (importantNoteMessages.Count != 0)
        {
            return new PcOrderImportantNote(order, importantNoteMessages);
        }

        return new PcOrderValidationSuccess(order);
    }

    private static Power TotalConsumption(IPcOrder order) => new Power(
        (order.WiFiAdapter is null
            ? 0d
            : order.WiFiAdapter.PowerConsumption.Watts) +
        (order.MotherBoard.IntegratedWiFiAdapter is null
            ? 0d
            : order.MotherBoard.IntegratedWiFiAdapter.PowerConsumption.Watts) +
        order.SolidStateDrives.Sum(drive => drive.PowerConsumption.Watts) +
        order.HardDrives.Sum(drive => drive.PowerConsumption.Watts) +
        order.GraphicsCards.Sum(card => card.PowerConsumption.Watts) +
        order.RamComponents.Sum(ram => ram.PowerConsumption.Watts) +
        order.CentralProcessor.PowerConsumption.Watts);

    private static Slots TotalSataSlotsUsed(IPcOrder order) => new Slots(
        order.HardDrives.Count +
        order.SolidStateDrives.Count(drive => drive.Connection is SataConnection));

    private static Dictionary<PciEConnection, Slots> TotalPciESlotsUsed(IPcOrder order)
    {
        var result = new Dictionary<PciEConnection, Slots>();

        if (order.MotherBoard.IntegratedWiFiAdapter is not null)
        {
            result[order.MotherBoard.IntegratedWiFiAdapter.Connection] = new Slots(1);
        }

        foreach (SolidStateDrive drive in order.SolidStateDrives)
        {
            if (drive.Connection is not PciEConnection connection) continue;
            if (!result.ContainsKey(connection))
            {
                result.Add(connection, new Slots(1));
                continue;
            }

            result[connection] = new Slots(result[connection].Number + 1);
        }

        foreach (GraphicsCard card in order.GraphicsCards)
        {
            if (!result.ContainsKey(card.Connection))
            {
                result.Add(card.Connection, new Slots(1));
                continue;
            }

            result[card.Connection] = new Slots(result[card.Connection].Number + 1);
        }

        return result;
    }
}