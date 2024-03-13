using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;

public interface IGraphicsCardBuilderDirector
{
    IGraphicsCardBuilder Direct(IGraphicsCardBuilder builder);
}