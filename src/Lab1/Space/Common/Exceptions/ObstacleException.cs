using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

public class ObstacleException : Exception
{
    public ObstacleException()
        : base("Obstacle Exception")
    {
    }

    public ObstacleException(string message)
        : base(message)
    {
    }

    public ObstacleException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNegative(int value, string? parameterName = null)
    {
        if (value < 0)
        {
            throw new ObstacleException(
                "Value should be non-negative",
                new ArgumentException(parameterName));
        }
    }
}