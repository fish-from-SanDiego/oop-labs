using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;

public interface IPcCaseBuilderDirector
{
    IPcCaseBuilder Direct(IPcCaseBuilder builder);
}