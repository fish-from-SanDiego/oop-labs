using Lab5.Application.Contracts;
using Lab5.Presentation.Console.Models;
using Lab5.Presentation.Console.Scenarios;

namespace Lab5.Presentation.Console.Extensions;

public static class CurrentAccountExtensions
{
    public static ScenarioRunnerContext GetAllActionsScenarioContext(this ICurrentAccountService currentAccountService)
    {
        var scenarios = new List<IScenario>
        {
            new DepositMoneyScenario(currentAccountService),
            new WithdrawMoneyScenario(currentAccountService),
            new ViewAccountBalanceScenario(currentAccountService),
            new ViewTransactionHistoryScenario(currentAccountService),
        };
        return new ScenarioRunnerContext(scenarios, "Select Action");
    }
}