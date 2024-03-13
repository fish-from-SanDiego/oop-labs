namespace Lab5.Presentation.Console.Exceptions;

public class ScenarioException : Exception
{
    public ScenarioException()
        : base("Scenario Exception")
    {
    }

    public ScenarioException(string message)
        : base(message)
    {
    }

    public ScenarioException(Exception innerException)
        : base("Scenario Exception", innerException)
    {
    }

    public ScenarioException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? value, string? parameters = null)
    {
        if (value is null)
        {
            throw new ScenarioException("value should be not null", new ArgumentNullException(parameters));
        }
    }
}