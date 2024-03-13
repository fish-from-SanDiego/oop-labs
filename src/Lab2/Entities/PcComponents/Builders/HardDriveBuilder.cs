using System;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public class HardDriveBuilder : IHardDriveBuilder
{
    private string? _name;
    private MemoryCapacity? _capacity;
    private RotationalSpeed? _spindleSpeed;
    private Power? _powerConsumption;

    public HardDrive Build() => new HardDrive(
        _name ?? throw new BuilderException(new InvalidOperationException(nameof(_name))),
        _capacity ?? throw new BuilderException(new InvalidOperationException(nameof(_capacity))),
        _spindleSpeed ?? throw new BuilderException(new InvalidOperationException(nameof(_spindleSpeed))),
        _powerConsumption ?? throw new BuilderException(new InvalidOperationException(nameof(_powerConsumption))));

    public IHardDriveBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public IHardDriveBuilder WithCapacity(MemoryCapacity capacity)
    {
        _capacity = capacity;
        return this;
    }

    public IHardDriveBuilder WithSpindleSpeed(RotationalSpeed spindleSpeed)
    {
        _spindleSpeed = spindleSpeed;
        return this;
    }

    public IHardDriveBuilder WithPowerConsumption(Power powerConsumption)
    {
        _powerConsumption = powerConsumption;
        return this;
    }
}