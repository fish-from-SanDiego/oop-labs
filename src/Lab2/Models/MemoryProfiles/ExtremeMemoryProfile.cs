namespace Itmo.ObjectOrientedProgramming.Lab2.Models.MemoryProfiles;

public record ExtremeMemoryProfile : OverclockedMemoryProfile
{
    public ExtremeMemoryProfile(Timings timings, Voltage voltage, Frequency frequency)
        : base(timings, voltage, frequency)
    {
    }
}