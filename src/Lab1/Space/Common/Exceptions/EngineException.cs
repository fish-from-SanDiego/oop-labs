using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

public class EngineException : Exception
{
    public EngineException()
        : base("Engine Exception")
    {
    }

    public EngineException(string message)
        : base(message)
    {
    }

    public EngineException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNonPositive(double value, string? parameterName = null)
    {
        if (value <= 0)
        {
            throw new EngineException(
                "Attempting to pass non-positive value",
                new ArgumentException(parameterName));
        }
    }
}