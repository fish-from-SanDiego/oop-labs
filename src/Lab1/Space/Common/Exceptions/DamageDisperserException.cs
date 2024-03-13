using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

public class DamageDisperserException : Exception
{
    public DamageDisperserException()
        : base("Damage Disperser Exception")
    {
    }

    public DamageDisperserException(string message)
        : base(message)
    {
    }

    public DamageDisperserException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}