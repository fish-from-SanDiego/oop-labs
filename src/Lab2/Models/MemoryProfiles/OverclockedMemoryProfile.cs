using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.MemoryProfiles;

public abstract record OverclockedMemoryProfile
{
    protected OverclockedMemoryProfile(Timings timings, Voltage voltage, Frequency frequency)
    {
        ComponentAttributeException.ThrowIfNull(timings, nameof(timings));
        ComponentAttributeException.ThrowIfNull(voltage, nameof(voltage));
        ComponentAttributeException.ThrowIfNull(frequency, nameof(frequency));

        Timings = timings;
        Voltage = voltage;
        Frequency = frequency;
    }

    public Timings Timings { get; }
    public Voltage Voltage { get; }
    public Frequency Frequency { get; }
}