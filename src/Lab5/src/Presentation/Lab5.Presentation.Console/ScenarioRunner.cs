using Lab5.Presentation.Console.Models;
using Spectre.Console;

namespace Lab5.Presentation.Console;

public class ScenarioRunner : IScenarioRunner
{
    private const int MillisecondsToSleep = 100;
    private ScenarioRunnerContext _currentContext;

    public ScenarioRunner(string initialTitle, IEnumerable<IScenario> initialScenarios)
    {
        _currentContext = new ScenarioRunnerContext(initialScenarios, initialTitle);
    }

    public ScenarioRunner(string initialTitle, params IScenario[] initialScenarios)
        : this(initialTitle, initialScenarios as IEnumerable<IScenario>)
    {
    }

    public bool TryRunScenario()
    {
        var currentScenarios = _currentContext.Scenarios.ToList();

        if (currentScenarios.Any() is false)
        {
            AnsiConsole.WriteLine("Quitting...");

            return false;
        }

        if (currentScenarios.Count == 1)
        {
            _currentContext = currentScenarios.First().Run();
            if (currentScenarios.Any() is false)
                return true;

            AnsiConsole.WriteLine("\nPress any key to continue");
            while (System.Console.KeyAvailable is false)
            {
                Thread.Sleep(MillisecondsToSleep);
            }

            return true;
        }

        SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
            .Title(_currentContext.Title)
            .AddChoices(currentScenarios)
            .UseConverter(x => x.Name);

        IScenario scenario = AnsiConsole.Prompt(selector);
        _currentContext = new ScenarioRunnerContext(new[] { scenario }, scenario.Name);

        return true;
    }
}