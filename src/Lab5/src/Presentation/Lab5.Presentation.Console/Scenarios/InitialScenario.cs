using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Presentation.Console.Exceptions;
using Lab5.Presentation.Console.Models;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios;

public class InitialScenario : IScenario
{
    private readonly IPasswordService _passwordService;
    private readonly IAdminLoginService _adminLoginService;
    private readonly IAccountLoginService _accountLoginService;

    public InitialScenario(
        IPasswordService passwordService,
        IAdminLoginService adminLoginService,
        IAccountLoginService accountLoginService)
    {
        _passwordService = passwordService;
        _adminLoginService = adminLoginService;
        _accountLoginService = accountLoginService;
    }

    public string Name => "Password Registration";

    public ScenarioRunnerContext Run()
    {
        var nextScenarios = new List<IScenario>
        {
            new AdminLoginScenario(_adminLoginService),
            new AccountLoginScenario(_accountLoginService),
        };

        AnsiConsole.WriteLine("Fetching the system passwords");
        OperationResult<int> operationResult = _passwordService.GetPasswordsCount();

        if (operationResult is OperationError<int> error)
        {
            AnsiConsole.WriteLine($"Error: {error.Message}");
            return new ScenarioRunnerContext(new[] { this }, string.Empty);
        }

        int passwordsCount =
            (operationResult as OperationSuccess<int> ?? throw new ScenarioException("unexpected type")).Value;

        if (passwordsCount > 0)
        {
            AnsiConsole.WriteLine("The passwords are ok");

            return new ScenarioRunnerContext(nextScenarios, "Select the operating mode");
        }

        string password = AnsiConsole.Prompt(
            new TextPrompt<string>("Make up the system password: ")
                .PromptStyle("blue")
                .Secret());

        OperationResult<string> registrationResult = _passwordService.RegisterPassword(password);
        if (registrationResult is OperationError<string> registrationError)
        {
            AnsiConsole.WriteLine($"Error: {registrationError.Message}");
            return new ScenarioRunnerContext(new[] { this }, string.Empty);
        }

        AnsiConsole.WriteLine("Password registered successfully");
        return new ScenarioRunnerContext(nextScenarios, "Select the operating mode");
    }
}