using Lab5.Application.Contracts.ResultTypes;
using Lab5.Application.Models;

namespace Lab5.Application.Contracts;

public interface ICurrentAccountService
{
    OperationResult<long> AccountBalance { get; }
    OperationResult<IReadOnlyCollection<Transaction>> TransactionHistory { get; }
    OperationResult<Transaction> DepositMoney(long amount);
    OperationResult<Transaction> WithdrawMoney(long amount);
}