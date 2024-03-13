using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record CpuCores
{
    public CpuCores(int number, Frequency singleCoreFrequency)
    {
        ComponentAttributeException.ThrowIfNonPositive(number, nameof(number));
        ComponentAttributeException.ThrowIfNull(singleCoreFrequency, nameof(singleCoreFrequency));

        Number = number;
        SingleCoreFrequency = singleCoreFrequency;
    }

    public int Number { get; }
    public Frequency SingleCoreFrequency { get; }
}