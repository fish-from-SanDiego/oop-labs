using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Common;

public class ComponentAttributeException : Exception
{
    public ComponentAttributeException()
        : base("Component Attribute Exception")
    {
    }

    public ComponentAttributeException(string message)
        : base(message)
    {
    }

    public ComponentAttributeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNonPositive(double value, string? parameterName = null)
    {
        if (value <= 0)
        {
            throw new ComponentAttributeException(
                "Attempting to pass non-positive value",
                new ArgumentException(parameterName));
        }
    }

    public static void ThrowIfNull(object? value, string? parameterName = null)
    {
        if (value is null)
        {
            throw new ComponentAttributeException(
                "Value should be not null",
                new ArgumentNullException(parameterName));
        }
    }
}