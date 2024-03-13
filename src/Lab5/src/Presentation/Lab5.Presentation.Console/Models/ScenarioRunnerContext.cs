namespace Lab5.Presentation.Console.Models;

public record ScenarioRunnerContext(IEnumerable<IScenario> Scenarios, string Title);