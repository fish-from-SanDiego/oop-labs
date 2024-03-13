using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Common;

public class ComponentException : Exception
{
    public ComponentException()
        : base("Component Exception")
    {
    }

    public ComponentException(string message)
        : base(message)
    {
    }

    public ComponentException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameterName = null)
    {
        if (value is null)
        {
            throw new ComponentException(
                "Value should be not null",
                new ArgumentNullException(parameterName));
        }
    }
}