using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Application.Models;
using Lab5.Presentation.Console.Exceptions;
using Lab5.Presentation.Console.Extensions;
using Lab5.Presentation.Console.Models;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios;

public class DepositMoneyScenario : IScenario
{
    private readonly ICurrentAccountService _currentAccountService;

    public DepositMoneyScenario(ICurrentAccountService currentAccountService)
    {
        _currentAccountService = currentAccountService;
    }

    public string Name => "Deposit money";

    public ScenarioRunnerContext Run()
    {
        string amountString = AnsiConsole.Ask<string>("Enter the amount you wish to deposit: ");
        if (long.TryParse(amountString, out long amount) is false)
        {
            AnsiConsole.WriteLine("Invalid format; try again");
            return new ScenarioRunnerContext(new[] { this }, string.Empty);
        }

        OperationResult<Transaction> depositMoneyResult = _currentAccountService.DepositMoney(amount);

        if (depositMoneyResult is OperationError<Transaction> error)
        {
            AnsiConsole.WriteLine($"Error: {error.Message}");

            return _currentAccountService.GetAllActionsScenarioContext();
        }

        Transaction transaction =
            (depositMoneyResult as OperationSuccess<Transaction> ?? throw new ScenarioException("unexpected type"))
            .Value;

        AnsiConsole.WriteLine(
            $"Money successfully deposited; account's balance is now {transaction.BalanceAfterTransaction}" +
            $" (was {transaction.BalanceBeforeTransaction})");
        return _currentAccountService.GetAllActionsScenarioContext();
    }
}