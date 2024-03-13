using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public class SolidStateDrive : IPcComponent<SolidStateDrive>, IPcComponent, ISolidStateDriveBuilderDirector
{
    internal SolidStateDrive(
        string name,
        Power powerConsumption,
        MemoryTransferSpeed memoryReadMaxSpeed,
        MemoryTransferSpeed memoryWriteMaxSpeed,
        ComponentConnection connection,
        MemoryCapacity capacity)
    {
        Name = name;
        PowerConsumption = powerConsumption;
        Connection = connection;
        Capacity = capacity;
        MemoryReadMaxSpeed = memoryReadMaxSpeed;
        MemoryWriteMaxSpeed = memoryWriteMaxSpeed;
    }

    public string Name { get; }
    public MemoryCapacity Capacity { get; }
    public MemoryTransferSpeed MemoryReadMaxSpeed { get; }
    public MemoryTransferSpeed MemoryWriteMaxSpeed { get; }
    public Power PowerConsumption { get; }
    public ComponentConnection Connection { get; }

    public virtual SolidStateDrive Clone() =>
        new SolidStateDrive(Name, PowerConsumption, MemoryReadMaxSpeed, MemoryWriteMaxSpeed, Connection, Capacity);

    public ISolidStateDriveBuilder Direct(ISolidStateDriveBuilder builder)
    {
        BuilderException.ThrowIfNull(builder);

        return builder.WithName(Name).WithPowerConsumption(PowerConsumption).WithMemoryReadMaxSpeed(MemoryReadMaxSpeed)
            .WithMemoryWriteMaxSpeed(MemoryWriteMaxSpeed).WithConnection(Connection).WithCapacity(Capacity);
    }
}