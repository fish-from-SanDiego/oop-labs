namespace Lab5.Application.Exceptions;

public class AccountServiceException : Exception
{
    public AccountServiceException()
        : base("Account Service Exception")
    {
    }

    public AccountServiceException(string message)
        : base(message)
    {
    }

    public AccountServiceException(Exception innerException)
        : base("Account Service Exception", innerException)
    {
    }

    public AccountServiceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameters = null)
    {
        if (value is null)
        {
            throw new AccountServiceException("value should be not null", new ArgumentNullException(parameters));
        }
    }
}