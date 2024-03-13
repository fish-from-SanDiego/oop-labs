using System.Collections.Generic;
using System.Collections.Immutable;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class PcOrder : IPcOrder
{
    internal PcOrder(
        MotherBoard motherBoard,
        CentralProcessor centralProcessor,
        CpuCoolingSystem cpuCoolingSystem,
        PcCase pcCase,
        PowerSupply powerSupply,
        IEnumerable<RandomAccessMemory> ramComponents,
        IEnumerable<GraphicsCard> graphicsCards,
        IEnumerable<HardDrive> hardDrives,
        IEnumerable<SolidStateDrive> solidStateDrives,
        WiFiAdapter? wiFiAdapter)
    {
        MotherBoard = motherBoard;
        CentralProcessor = centralProcessor;
        CpuCoolingSystem = cpuCoolingSystem;
        PcCase = pcCase;
        PowerSupply = powerSupply;
        RamComponents = ramComponents.ToImmutableList();
        GraphicsCards = graphicsCards.ToImmutableList();
        HardDrives = hardDrives.ToImmutableList();
        SolidStateDrives = solidStateDrives.ToImmutableList();
        WiFiAdapter = wiFiAdapter;
    }

    public MotherBoard MotherBoard { get; }
    public CentralProcessor CentralProcessor { get; }
    public CpuCoolingSystem CpuCoolingSystem { get; }
    public PcCase PcCase { get; }
    public PowerSupply PowerSupply { get; }
    public WiFiAdapter? WiFiAdapter { get; }
    public IReadOnlyCollection<RandomAccessMemory> RamComponents { get; }
    public IReadOnlyCollection<GraphicsCard> GraphicsCards { get; }
    public IReadOnlyCollection<HardDrive> HardDrives { get; }
    public IReadOnlyCollection<SolidStateDrive> SolidStateDrives { get; }

    public IPcOrderBuilder Direct(IPcOrderBuilder builder)
    {
        BuilderException.ThrowIfNull(builder);
        return builder.WithMotherBoard(MotherBoard).WithCentralProcessor(CentralProcessor)
            .WithCpuCoolingSystem(CpuCoolingSystem).WithPcCase(PcCase).WithPowerSupply(PowerSupply)
            .WithWiFiAdapter(WiFiAdapter).WithRamComponents(RamComponents).WithGraphicsCards(GraphicsCards)
            .WithHardDrives(HardDrives).WithSolidStateDrives(SolidStateDrives);
    }
}