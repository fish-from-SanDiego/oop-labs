using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record RotationalSpeed
{
    public RotationalSpeed(int revolutionsPerMinute)
    {
        ComponentAttributeException.ThrowIfNonPositive(revolutionsPerMinute);
        RevolutionsPerMinute = revolutionsPerMinute;
    }

    public int RevolutionsPerMinute { get; }
}