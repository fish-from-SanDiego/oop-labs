using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

public class SpaceSectionException : Exception
{
    public SpaceSectionException()
        : base("Space Section Exception")
    {
    }

    public SpaceSectionException(string message)
        : base(message)
    {
    }

    public SpaceSectionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameterName = null)
    {
        if (value is null)
        {
            throw new SpaceSectionException(
                "Value should be not null",
                new ArgumentNullException(parameterName));
        }
    }
}