using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record Voltage
{
    public Voltage(double volts)
    {
        ComponentAttributeException.ThrowIfNonPositive(volts, nameof(volts));

        Volts = volts;
    }

    public double Volts { get; }
}