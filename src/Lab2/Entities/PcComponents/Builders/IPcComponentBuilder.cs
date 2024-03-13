using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface IPcComponentBuilder<out T>
    where T : IPcComponent
{
    T Build();
}