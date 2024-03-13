using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Common;

public class BuilderException : Exception
{
    public BuilderException()
        : base("Builder Exception")
    {
    }

    public BuilderException(string message)
        : base(message)
    {
    }

    public BuilderException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public BuilderException(Exception innerException)
        : base("Builder Exception", innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameterName = null)
    {
        if (value is null)
        {
            throw new BuilderException(
                "Value should be not null",
                new ArgumentNullException(parameterName));
        }
    }
}