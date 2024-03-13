using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Application.Models;
using Lab5.Presentation.Console.Exceptions;
using Lab5.Presentation.Console.Models;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios;

public class AccountCreationScenario : IScenario
{
    private readonly IAdminService _adminService;

    public AccountCreationScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Create new account";

    public ScenarioRunnerContext Run()
    {
        string creationString =
            AnsiConsole.Ask<string>("Enter new account's Id and pin code (in format: [green]id pin_code[/]): ");
        string[] tokens = creationString.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length != 2 ||
            long.TryParse(tokens[0], out long accountId) is false ||
            int.TryParse(tokens[1], out int pinCode) is false)
        {
            AnsiConsole.WriteLine("Invalid format; try again");
            return new ScenarioRunnerContext(new[] { this }, string.Empty);
        }

        OperationResult<Account> registrationResult = _adminService.RegisterAccount(accountId, pinCode);
        if (registrationResult is OperationError<Account> error)
        {
            AnsiConsole.WriteLine($"Error: {error.Message}");
            return new ScenarioRunnerContext(new[] { this }, string.Empty);
        }

        Account account =
            (registrationResult as OperationSuccess<Account> ?? throw new ScenarioException("unexpected type")).Value;
        AnsiConsole.WriteLine($"Account with Id {account.Id} registered successfully");

        return new ScenarioRunnerContext(new[] { this }, string.Empty);
    }
}