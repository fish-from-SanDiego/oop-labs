using System;

namespace Itmo.ObjectOrientedProgramming.Lab3.Output.Common;

public class OutputException : Exception
{
    public OutputException()
        : base("Output Exception")
    {
    }

    public OutputException(string message)
        : base(message)
    {
    }

    public OutputException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameters = null)
    {
        if (value is null)
        {
            throw new OutputException("value should be not null", new ArgumentNullException(parameters));
        }
    }
}