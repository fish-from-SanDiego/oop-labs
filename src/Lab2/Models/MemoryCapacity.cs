using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record MemoryCapacity
{
    public MemoryCapacity(double gigabytes)
    {
        ComponentAttributeException.ThrowIfNonPositive(gigabytes);

        Gigabytes = gigabytes;
    }

    public double Gigabytes { get; }
}