using Lab5.Presentation.Console.Models;

namespace Lab5.Presentation.Console;

public interface IScenario
{
    public string Name { get; }

    ScenarioRunnerContext Run();
}