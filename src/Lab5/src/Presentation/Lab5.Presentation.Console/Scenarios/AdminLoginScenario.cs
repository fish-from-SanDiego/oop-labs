using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Presentation.Console.Exceptions;
using Lab5.Presentation.Console.Models;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios;

public class AdminLoginScenario : IScenario
{
    private readonly IAdminLoginService _adminLoginService;

    public AdminLoginScenario(IAdminLoginService adminLoginService)
    {
        _adminLoginService = adminLoginService;
    }

    public string Name => "Login as admin";

    public ScenarioRunnerContext Run()
    {
        string enteredPassword = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the password: ")
                .PromptStyle("blue")
                .Secret());

        AdminLoginResult loginResult = _adminLoginService.Login(enteredPassword);

        if (loginResult is AdminAccessDenied)
        {
            AnsiConsole.MarkupLine("[red]Incorrect[/] password; the session will be stopped");
            return new ScenarioRunnerContext(Enumerable.Empty<IScenario>(), string.Empty);
        }

        if (loginResult is AdminLoginError error)
        {
            AnsiConsole.WriteLine($"Error: {error.Message}");
            return new ScenarioRunnerContext(new[] { this }, Name);
        }

        IAdminService adminService =
            (loginResult as AdminLoginSuccess ?? throw new ScenarioException("unexpected type")).AdminService;

        AnsiConsole.WriteLine("Successful login; entering admin mode");
        return new ScenarioRunnerContext(new[] { new AccountCreationScenario(adminService) }, string.Empty);
    }
}