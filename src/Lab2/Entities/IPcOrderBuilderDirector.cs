namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public interface IPcOrderBuilderDirector
{
    IPcOrderBuilder Direct(IPcOrderBuilder builder);
}