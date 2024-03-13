using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

public class ShipException : Exception
{
    public ShipException()
        : base("Ship Exception")
    {
    }

    public ShipException(string message)
        : base(message)
    {
    }

    public ShipException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameters = null)
    {
        if (value is null)
        {
            throw new ShipException(
                "Value should be not null",
                new ArgumentNullException(parameters));
        }
    }
}