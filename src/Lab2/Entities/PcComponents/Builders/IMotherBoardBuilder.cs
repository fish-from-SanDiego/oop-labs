using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Ddr;
using Itmo.ObjectOrientedProgramming.Lab2.Models.FormFactors;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface IMotherBoardBuilder : IPcComponentBuilder<MotherBoard>
{
    IMotherBoardBuilder WithName(string name);
    IMotherBoardBuilder WithSocket(CpuSocket socket);
    IMotherBoardBuilder WithPciEInfo(IReadOnlyDictionary<PciEConnection, Slots> info);
    IMotherBoardBuilder SetPciEInfo(PciEConnection connection, Slots slots);
    IMotherBoardBuilder ClearPciEInfo();
    IMotherBoardBuilder WithSataPorts(Slots sataPorts);
    IMotherBoardBuilder WithJedecFrequencies(IEnumerable<Frequency> frequencies);
    IMotherBoardBuilder AddJedecFrequency(Frequency frequency);
    IMotherBoardBuilder ClearJedecFrequencies();
    IMotherBoardBuilder WithOverclockedFrequencies(IEnumerable<Frequency> frequencies);
    IMotherBoardBuilder AddOverclockedFrequency(Frequency frequency);
    IMotherBoardBuilder ClearOverclockedFrequencies();
    IMotherBoardBuilder WithDdrStandard(DdrStandard standard);
    IMotherBoardBuilder WithRamSlots(Slots ramSlots);
    IMotherBoardBuilder WithFormFactor(MotherBoardFormFactor formFactor);
    IMotherBoardBuilder WithSupportedMemoryFormFactor(RandomAccessMemoryFormFactor formFactor);
    IMotherBoardBuilder WithBios(Bios bios);
    IMotherBoardBuilder WithWiFiAdapter(WiFiAdapter? wiFiAdapter);
    IMotherBoardBuilder WithoutWiFiAdapter();
}