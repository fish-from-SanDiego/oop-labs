using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record Timings
{
    public Timings(int rasDelay, int rpDelay, int rcDelay, int casLatency)
    {
        ComponentAttributeException.ThrowIfNonPositive(rasDelay, nameof(rasDelay));
        ComponentAttributeException.ThrowIfNonPositive(rpDelay, nameof(rpDelay));
        ComponentAttributeException.ThrowIfNonPositive(rcDelay, nameof(rcDelay));
        ComponentAttributeException.ThrowIfNonPositive(casLatency, nameof(casLatency));
        RasDelay = rasDelay;
        RpDelay = rpDelay;
        RcDelay = rcDelay;
        CasLatency = casLatency;
    }

    public int CasLatency { get; }
    public int RcDelay { get; }
    public int RpDelay { get; }
    public int RasDelay { get; }
}