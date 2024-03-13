using System.Globalization;
using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Application.Models;
using Lab5.Presentation.Console.Exceptions;
using Lab5.Presentation.Console.Extensions;
using Lab5.Presentation.Console.Models;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios;

public class ViewTransactionHistoryScenario : IScenario
{
    private readonly ICurrentAccountService _currentAccountService;

    public ViewTransactionHistoryScenario(ICurrentAccountService currentAccountService)
    {
        _currentAccountService = currentAccountService;
    }

    public string Name => "View transaction history";

    public ScenarioRunnerContext Run()
    {
        OperationResult<IReadOnlyCollection<Transaction>> transactionHistoryResult =
            _currentAccountService.TransactionHistory;

        if (transactionHistoryResult is OperationError<IReadOnlyCollection<Transaction>> error)
        {
            AnsiConsole.WriteLine($"Error: {error.Message}");

            return _currentAccountService.GetAllActionsScenarioContext();
        }

        IReadOnlyCollection<Transaction> transactions =
            (transactionHistoryResult as OperationSuccess<IReadOnlyCollection<Transaction>> ??
             throw new ScenarioException("unexpected type"))
            .Value;

        var table = new Table();

        table.AddColumn(new TableColumn("Transaction type").Centered());
        table.AddColumn(new TableColumn("Balance before transaction").Centered());
        table.AddColumn(new TableColumn("Balance after transaction").Centered());

        foreach (Transaction transaction in transactions)
        {
            string type = transaction.BalanceAfterTransaction > transaction.BalanceBeforeTransaction
                ? "Deposit"
                : "Withdrawal";
            table.AddRow(
                type,
                transaction.BalanceBeforeTransaction.ToString(CultureInfo.InvariantCulture),
                transaction.BalanceAfterTransaction.ToString(CultureInfo.InvariantCulture));
        }

        AnsiConsole.Write(table);

        return _currentAccountService.GetAllActionsScenarioContext();
    }
}