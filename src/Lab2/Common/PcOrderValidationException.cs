using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Common;

public class PcOrderValidationException : Exception
{
    public PcOrderValidationException()
        : base("PC Order Validation Exception")
    {
    }

    public PcOrderValidationException(string message)
        : base(message)
    {
    }

    public PcOrderValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameterName = null)
    {
        if (value is null)
        {
            throw new PcOrderValidationException(
                "Value should be not null",
                new ArgumentNullException(parameterName));
        }
    }
}