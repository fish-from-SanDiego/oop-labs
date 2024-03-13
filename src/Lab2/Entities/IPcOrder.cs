using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public interface IPcOrder : IPcOrderBuilderDirector
{
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
}