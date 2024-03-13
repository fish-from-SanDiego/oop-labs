using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record Power
{
    public Power(double watts)
    {
        ComponentAttributeException.ThrowIfNonPositive(watts);
        Watts = watts;
    }

    public double Watts { get; }
}