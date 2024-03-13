using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

public class SpaceEnvironmentException : Exception
{
    public SpaceEnvironmentException()
        : base("Space Environment Exception")
    {
    }

    public SpaceEnvironmentException(string message)
        : base(message)
    {
    }

    public SpaceEnvironmentException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameters = null)
    {
        if (value is null)
        {
            throw new SpaceEnvironmentException(
                "Value should be not null",
                new ArgumentNullException(parameters));
        }
    }
}