using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;

public class FileSystemException : Exception
{
    public FileSystemException()
        : base("File System Exception")
    {
    }

    public FileSystemException(string message)
        : base(message)
    {
    }

    public FileSystemException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public FileSystemException(Exception innerException)
        : base("File System Exception", innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameterName = null)
    {
        if (value is null)
        {
            throw new FileSystemException(
                "Value should be not null",
                new ArgumentNullException(parameterName));
        }
    }
}