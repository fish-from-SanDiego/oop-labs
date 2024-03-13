using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public class PowerSupply : IPcComponent<PowerSupply>, IPcComponent, IPowerSupplyBuilderDirector
{
    internal PowerSupply(string name, Power peakLoad)
    {
        Name = name;
        PeakLoad = peakLoad;
    }

    public string Name { get; }
    public Power PeakLoad { get; }

    public virtual PowerSupply Clone() => new PowerSupply(Name, PeakLoad);

    public IPowerSupplyBuilder Direct(IPowerSupplyBuilder builder)
    {
        BuilderException.ThrowIfNull(builder);

        return builder.WithName(Name).WithPeakLoad(PeakLoad);
    }
}