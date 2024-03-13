using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Presentation.Console.Exceptions;
using Lab5.Presentation.Console.Extensions;
using Lab5.Presentation.Console.Models;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios;

public class ViewAccountBalanceScenario : IScenario
{
    private readonly ICurrentAccountService _currentAccountService;

    public ViewAccountBalanceScenario(ICurrentAccountService currentAccountService)
    {
        _currentAccountService = currentAccountService;
    }

    public string Name => "View account's balance";

    public ScenarioRunnerContext Run()
    {
        OperationResult<long> balanceViewResult = _currentAccountService.AccountBalance;

        if (balanceViewResult is OperationError<long> error)
        {
            AnsiConsole.WriteLine($"Error: {error.Message}");

            return _currentAccountService.GetAllActionsScenarioContext();
        }

        long balance =
            (balanceViewResult as OperationSuccess<long> ?? throw new ScenarioException("unexpected type")).Value;

        AnsiConsole.WriteLine($"Account's balance is: {balance}");
        return _currentAccountService.GetAllActionsScenarioContext();
    }
}