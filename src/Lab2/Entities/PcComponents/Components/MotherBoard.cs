using System.Collections.Generic;
using System.Collections.Immutable;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Ddr;
using Itmo.ObjectOrientedProgramming.Lab2.Models.FormFactors;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public class MotherBoard : IPcComponent<MotherBoard>, IPcComponent, IMotherBoardBuilderDirector
{
    internal MotherBoard(
        string name,
        CpuSocket socket,
        IReadOnlyDictionary<PciEConnection, Slots> pciEInfo,
        Slots sataPorts,
        IEnumerable<Frequency> jedecFrequencies,
        IEnumerable<Frequency> overclockedFrequencies,
        DdrStandard ddrStandard,
        Slots ramSlots,
        MotherBoardFormFactor formFactor,
        RandomAccessMemoryFormFactor supportedMemoryFormFactor,
        Bios bios,
        WiFiAdapter? integratedWiFiAdapter)
    {
        Name = name;
        Socket = socket;
        PciEInfo = pciEInfo.ToImmutableDictionary();
        SataPorts = sataPorts;
        JedecFrequencies = jedecFrequencies.ToImmutableSortedSet();
        OverclockedFrequencies = overclockedFrequencies.ToImmutableHashSet();
        DdrStandard = ddrStandard;
        RamSlots = ramSlots;
        FormFactor = formFactor;
        SupportedMemoryFormFactor = supportedMemoryFormFactor;
        Bios = bios;
        IntegratedWiFiAdapter = integratedWiFiAdapter;
    }

    public string Name { get; }
    public CpuSocket Socket { get; }
    public IReadOnlyDictionary<PciEConnection, Slots> PciEInfo { get; }
    public Slots SataPorts { get; }
    public IReadOnlySet<Frequency> JedecFrequencies { get; }
    public IReadOnlySet<Frequency> OverclockedFrequencies { get; }
    public DdrStandard DdrStandard { get; }
    public Slots RamSlots { get; }
    public MotherBoardFormFactor FormFactor { get; }
    public RandomAccessMemoryFormFactor SupportedMemoryFormFactor { get; }
    public Bios Bios { get; }
    public WiFiAdapter? IntegratedWiFiAdapter { get; }
    public bool HasIntegratedWiFiAdapter => IntegratedWiFiAdapter is not null;

    public virtual MotherBoard Clone() => new MotherBoard(
        Name,
        Socket,
        PciEInfo,
        SataPorts,
        JedecFrequencies,
        OverclockedFrequencies,
        DdrStandard,
        RamSlots,
        FormFactor,
        SupportedMemoryFormFactor,
        Bios,
        IntegratedWiFiAdapter);

    public IMotherBoardBuilder Direct(IMotherBoardBuilder builder)
    {
        BuilderException.ThrowIfNull(builder);

        return builder.WithName(Name).WithSocket(Socket).WithPciEInfo(PciEInfo).WithSataPorts(SataPorts)
            .WithJedecFrequencies(JedecFrequencies).WithOverclockedFrequencies(OverclockedFrequencies)
            .WithDdrStandard(DdrStandard).WithRamSlots(RamSlots).WithFormFactor(FormFactor)
            .WithSupportedMemoryFormFactor(SupportedMemoryFormFactor).WithBios(Bios)
            .WithWiFiAdapter(IntegratedWiFiAdapter);
    }
}