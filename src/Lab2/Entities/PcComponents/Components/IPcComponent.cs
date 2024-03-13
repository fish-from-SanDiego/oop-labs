namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public interface IPcComponent<out T>
    where T : IPcComponent
{
    T Clone();
}

public interface IPcComponent
{
    string Name { get; }
}