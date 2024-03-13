using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Financial.Common;

public class MoneyException : Exception
{
    public MoneyException()
        : base("Money exception")
    {
    }

    public MoneyException(string message)
        : base(message)
    {
    }

    public MoneyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}