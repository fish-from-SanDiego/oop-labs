using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

public class ProtectionException : Exception
{
    public ProtectionException()
        : base("Protection Exception")
    {
    }

    public ProtectionException(string message)
        : base(message)
    {
    }

    public ProtectionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameters = null)
    {
        if (value is null)
        {
            throw new ProtectionException(
                "Value should be not null",
                new ArgumentNullException(parameters));
        }
    }
}