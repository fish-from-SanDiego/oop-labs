using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface IPowerSupplyBuilder : IPcComponentBuilder<PowerSupply>
{
    IPowerSupplyBuilder WithName(string name);
    IPowerSupplyBuilder WithPeakLoad(Power peakLoad);
}