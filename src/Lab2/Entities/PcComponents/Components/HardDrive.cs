using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public class HardDrive : IPcComponent<HardDrive>, IPcComponent, IHardDriveBuilderDirector
{
    internal HardDrive(string name, MemoryCapacity capacity, RotationalSpeed spindleSpeed, Power powerConsumption)
    {
        Connection = new SataConnection();
        Name = name;
        Capacity = capacity;
        SpindleSpeed = spindleSpeed;
        PowerConsumption = powerConsumption;
    }

    public string Name { get; }
    public MemoryCapacity Capacity { get; }
    public RotationalSpeed SpindleSpeed { get; }
    public Power PowerConsumption { get; }
    public SataConnection Connection { get; }

    public virtual HardDrive Clone() => new HardDrive(Name, Capacity, SpindleSpeed, PowerConsumption);

    public IHardDriveBuilder Direct(IHardDriveBuilder builder)
    {
        BuilderException.ThrowIfNull(builder);

        return builder.WithName(Name).WithCapacity(Capacity).WithSpindleSpeed(SpindleSpeed)
            .WithPowerConsumption(PowerConsumption);
    }
}