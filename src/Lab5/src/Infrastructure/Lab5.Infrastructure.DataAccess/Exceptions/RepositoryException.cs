namespace Lab5.Infrastructure.DataAccess.Exceptions;

public class RepositoryException : Exception
{
    public RepositoryException()
        : base("Repository Exception")
    {
    }

    public RepositoryException(string message)
        : base(message)
    {
    }

    public RepositoryException(Exception innerException)
        : base("Repository Exception", innerException)
    {
    }

    public RepositoryException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameters = null)
    {
        if (value is null)
        {
            throw new RepositoryException("value should be not null", new ArgumentNullException(parameters));
        }
    }
}