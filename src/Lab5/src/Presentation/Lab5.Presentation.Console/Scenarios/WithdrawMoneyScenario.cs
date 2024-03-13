using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Application.Models;
using Lab5.Presentation.Console.Exceptions;
using Lab5.Presentation.Console.Extensions;
using Lab5.Presentation.Console.Models;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios;

public class WithdrawMoneyScenario : IScenario
{
    private readonly ICurrentAccountService _currentAccountService;

    public WithdrawMoneyScenario(ICurrentAccountService currentAccountService)
    {
        _currentAccountService = currentAccountService;
    }

    public string Name => "Withdraw money";

    public ScenarioRunnerContext Run()
    {
        string amountString = AnsiConsole.Ask<string>("Enter the amount you wish to withdraw: ");
        if (long.TryParse(amountString, out long amount) is false)
        {
            AnsiConsole.WriteLine("Invalid format; try again");
            return new ScenarioRunnerContext(new[] { this }, string.Empty);
        }

        OperationResult<Transaction> withdrawMoneyResult = _currentAccountService.WithdrawMoney(amount);

        if (withdrawMoneyResult is OperationError<Transaction> error)
        {
            AnsiConsole.WriteLine($"Error: {error.Message}");

            return _currentAccountService.GetAllActionsScenarioContext();
        }

        Transaction transaction =
            (withdrawMoneyResult as OperationSuccess<Transaction> ?? throw new ScenarioException("unexpected type"))
            .Value;

        AnsiConsole.WriteLine(
            $"Money successfully withdrawn; account's balance is now {transaction.BalanceAfterTransaction}" +
            $" (was {transaction.BalanceBeforeTransaction})");
        return _currentAccountService.GetAllActionsScenarioContext();
    }
}