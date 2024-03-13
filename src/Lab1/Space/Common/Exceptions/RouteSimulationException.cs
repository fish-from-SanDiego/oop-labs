using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

public class RouteSimulationException : Exception
{
    public RouteSimulationException()
        : base("Route Simulation Exception")
    {
    }

    public RouteSimulationException(string message)
        : base(message)
    {
    }

    public RouteSimulationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameters = null)
    {
        if (value is null)
        {
            throw new RouteSimulationException(
                "Value should be not null",
                new ArgumentNullException(parameters));
        }
    }
}