using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;

public interface ICpuCoolingSystemBuilderDirector
{
    ICpuCoolingSystemBuilder Direct(ICpuCoolingSystemBuilder builder);
}