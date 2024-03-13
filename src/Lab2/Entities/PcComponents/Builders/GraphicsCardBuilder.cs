using System;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public class GraphicsCardBuilder : IGraphicsCardBuilder
{
    private string? _name;
    private TwoDimensions? _size;
    private MemoryCapacity? _capacity;
    private PciEConnection? _connection;
    private Frequency? _chipFrequency;
    private Power? _powerConsumption;

    public GraphicsCard Build() => new GraphicsCard(
        _name ?? throw new BuilderException(new InvalidOperationException(nameof(_name))),
        _size ?? throw new BuilderException(new InvalidOperationException(nameof(_size))),
        _capacity ?? throw new BuilderException(new InvalidOperationException(nameof(_capacity))),
        _connection ?? throw new BuilderException(new InvalidOperationException(nameof(_connection))),
        _chipFrequency ?? throw new BuilderException(new InvalidOperationException(nameof(_chipFrequency))),
        _powerConsumption ?? throw new BuilderException(new InvalidOperationException(nameof(_powerConsumption))));

    public IGraphicsCardBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public IGraphicsCardBuilder WithSize(TwoDimensions size)
    {
        _size = size;
        return this;
    }

    public IGraphicsCardBuilder WithVideoMemoryCapacity(MemoryCapacity capacity)
    {
        _capacity = capacity;
        return this;
    }

    public IGraphicsCardBuilder WithConnection(PciEConnection connection)
    {
        _connection = connection;
        return this;
    }

    public IGraphicsCardBuilder WithChipFrequency(Frequency chipFrequency)
    {
        _chipFrequency = chipFrequency;
        return this;
    }

    public IGraphicsCardBuilder WithPowerConsumption(Power powerConsumption)
    {
        _powerConsumption = powerConsumption;
        return this;
    }
}