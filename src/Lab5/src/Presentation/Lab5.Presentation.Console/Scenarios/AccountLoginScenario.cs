using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Presentation.Console.Exceptions;
using Lab5.Presentation.Console.Extensions;
using Lab5.Presentation.Console.Models;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios;

public class AccountLoginScenario : IScenario
{
    private readonly IAccountLoginService _accountLoginService;

    public AccountLoginScenario(IAccountLoginService accountLoginService)
    {
        _accountLoginService = accountLoginService;
    }

    public string Name => "Login as account owner";

    public ScenarioRunnerContext Run()
    {
        string creationString =
            AnsiConsole.Ask<string>("Enter your account's Id and pin code (in format: [green]id pin_code[/]): ");
        string[] tokens = creationString.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length != 2 ||
            long.TryParse(tokens[0], out long accountId) is false ||
            int.TryParse(tokens[1], out int pinCode) is false)
        {
            AnsiConsole.WriteLine("Invalid format; try again");
            return new ScenarioRunnerContext(new[] { this }, string.Empty);
        }

        LoginResult loginResult = _accountLoginService.Login(accountId, pinCode);
        if (loginResult is LoginError error)
        {
            AnsiConsole.WriteLine($"Error: {error.Message}");
            return new ScenarioRunnerContext(new[] { this }, string.Empty);
        }

        ICurrentAccountService currentAccountService =
            (loginResult as LoginSuccess ?? throw new ScenarioException("unexpected type")).CurrentAccountService;

        AnsiConsole.WriteLine("Successful login; entering account mode");

        return currentAccountService.GetAllActionsScenarioContext();
    }
}