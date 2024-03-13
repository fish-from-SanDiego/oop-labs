using System;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public class PowerSupplyBuilder : IPowerSupplyBuilder
{
    private string? _name;
    private Power? _peakLoad;

    public IPowerSupplyBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public IPowerSupplyBuilder WithPeakLoad(Power peakLoad)
    {
        _peakLoad = peakLoad;
        return this;
    }

    public PowerSupply Build() => new PowerSupply(
        _name ?? throw new BuilderException(new InvalidOperationException(nameof(_name))),
        _peakLoad ?? throw new BuilderException(new InvalidOperationException(nameof(_peakLoad))));
}