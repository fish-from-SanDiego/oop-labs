using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record Slots
{
    public Slots(int number)
    {
        ComponentAttributeException.ThrowIfNonPositive(number, nameof(number));

        Number = number;
    }

    public int Number { get; }
}