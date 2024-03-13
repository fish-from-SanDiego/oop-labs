using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;

public class ParserException : Exception
{
    public ParserException()
        : base("Parser Exception")
    {
    }

    public ParserException(string message)
        : base(message)
    {
    }

    public ParserException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ParserException(Exception innerException)
        : base("Parser Exception", innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameterName = null)
    {
        if (value is null)
        {
            throw new ParserException(
                "Value should be not null",
                new ArgumentNullException(parameterName));
        }
    }
}