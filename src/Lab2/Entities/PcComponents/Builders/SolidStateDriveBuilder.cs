using System;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public class SolidStateDriveBuilder : ISolidStateDriveBuilder
{
    private string? _name;
    private Power? _powerConsumption;
    private MemoryTransferSpeed? _memoryReadMaxSpeed;
    private MemoryTransferSpeed? _memoryWriteMaxSpeed;
    private ComponentConnection? _connection;
    private MemoryCapacity? _capacity;

    public SolidStateDrive Build() => new SolidStateDrive(
        _name ?? throw new BuilderException(new InvalidOperationException(nameof(_name))),
        _powerConsumption ?? throw new BuilderException(new InvalidOperationException(nameof(_powerConsumption))),
        _memoryReadMaxSpeed ?? throw new BuilderException(new InvalidOperationException(nameof(_memoryReadMaxSpeed))),
        _memoryWriteMaxSpeed ?? throw new BuilderException(new InvalidOperationException(nameof(_memoryWriteMaxSpeed))),
        _connection ?? throw new BuilderException(new InvalidOperationException(nameof(_connection))),
        _capacity ?? throw new BuilderException(new InvalidOperationException(nameof(_capacity))));

    public ISolidStateDriveBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ISolidStateDriveBuilder WithCapacity(MemoryCapacity capacity)
    {
        _capacity = capacity;
        return this;
    }

    public ISolidStateDriveBuilder WithMemoryReadMaxSpeed(MemoryTransferSpeed speed)
    {
        _memoryReadMaxSpeed = speed;
        return this;
    }

    public ISolidStateDriveBuilder WithMemoryWriteMaxSpeed(MemoryTransferSpeed speed)
    {
        _memoryWriteMaxSpeed = speed;
        return this;
    }

    public ISolidStateDriveBuilder WithPowerConsumption(Power powerConsumption)
    {
        _powerConsumption = powerConsumption;
        return this;
    }

    public ISolidStateDriveBuilder WithConnection(ComponentConnection connection)
    {
        _connection = connection;
        return this;
    }
}