using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Financial.Common;

public class StockExchangeException : Exception
{
    public StockExchangeException()
        : base("Stock exchange exception")
    {
    }

    public StockExchangeException(string message)
        : base(message)
    {
    }

    public StockExchangeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}