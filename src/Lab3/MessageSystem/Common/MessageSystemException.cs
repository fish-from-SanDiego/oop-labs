using System;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Common;

public class MessageSystemException : Exception
{
    public MessageSystemException()
        : base("Message System Exception")
    {
    }

    public MessageSystemException(string message)
        : base(message)
    {
    }

    public MessageSystemException(Exception innerException)
        : base("Message System Exception", innerException)
    {
    }

    public MessageSystemException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameters = null)
    {
        if (value is null)
        {
            throw new MessageSystemException("value should be not null", new ArgumentNullException(parameters));
        }
    }
}