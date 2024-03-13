using Lab5.Application.Abstractions.Entities;
using Lab5.Application.Abstractions.Models;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Abstractions.ResultTypes;
using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Application.Exceptions;
using Lab5.Application.Models;

namespace Lab5.Application;

public class CurrentAccountService : ICurrentAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly long _accountId;

    public CurrentAccountService(IAccountRepository accountRepository, long accountId)
    {
        _accountRepository = accountRepository;
        _accountId = accountId;
    }

    public OperationResult<long> AccountBalance
    {
        get
        {
            FetchedAccount fetchedAccount = _accountRepository.FindAccountById(_accountId);
            if (fetchedAccount.GetResult is GetError)
            {
                return new OperationError<long>("Failed to view balance (failed to connect to database)");
            }

            if (fetchedAccount.Account is null)
            {
                return new OperationError<long>("Failed to view balance (account with such Id doesn't exist)");
            }

            return new OperationSuccess<long>(fetchedAccount.Account.Balance);
        }
    }

    public OperationResult<IReadOnlyCollection<Transaction>> TransactionHistory
    {
        get
        {
            IFetchedCollection<Transaction> fetchedHistory =
                _accountRepository.GetAllTransactionsByAccountId(_accountId);
            return fetchedHistory.GetResult is GetError error
                ? new OperationError<IReadOnlyCollection<Transaction>>(error.Message)
                : new OperationSuccess<IReadOnlyCollection<Transaction>>(fetchedHistory.Collection);
        }
    }

    public OperationResult<Transaction> DepositMoney(long amount)
    {
        if (amount <= 0)
        {
            return new OperationError<Transaction>("Cannot deposit non-positive amount of money");
        }

        FetchedAccount fetchedAccount = _accountRepository.FindAccountById(_accountId);
        if (fetchedAccount.GetResult is GetError)
        {
            return new OperationError<Transaction>("Failed to deposit money (failed to connect to database)");
        }

        if (fetchedAccount.Account is null)
        {
            return new OperationError<Transaction>("Failed to deposit money (account with such Id doesn't exist)");
        }

        long oldBalance = fetchedAccount.Account.Balance;
        UpdateResult result =
            _accountRepository.UpdateAccountBalance(_accountId, oldBalance + amount);
        return result switch
        {
            UpdateError error => new OperationError<Transaction>(error.Message),
            UpdateSuccess
                => new OperationSuccess<Transaction>(new Transaction(_accountId, oldBalance, oldBalance + amount)),
            _ => throw new AccountServiceException(new InvalidOperationException("unexpected type")),
        };
    }

    public OperationResult<Transaction> WithdrawMoney(long amount)
    {
        if (amount <= 0)
        {
            return new OperationError<Transaction>("Cannot withdraw non-positive amount of money");
        }

        FetchedAccount fetchedAccount = _accountRepository.FindAccountById(_accountId);
        if (fetchedAccount.GetResult is GetError)
        {
            return new OperationError<Transaction>("Failed to withdraw money (failed to connect to database)");
        }

        if (fetchedAccount.Account is null)
        {
            return new OperationError<Transaction>("Failed to withdraw money (account with such Id doesn't exist)");
        }

        long oldBalance = fetchedAccount.Account.Balance;

        if (amount > oldBalance)
        {
            return new OperationError<Transaction>("Attempting to withdraw more money than is in the account balance");
        }

        UpdateResult result = _accountRepository.UpdateAccountBalance(_accountId, oldBalance - amount);
        return result switch
        {
            UpdateError error => new OperationError<Transaction>(error.Message),
            UpdateSuccess => new OperationSuccess<Transaction>(
                new Transaction(_accountId, oldBalance, oldBalance - amount)),
            _ => throw new AccountServiceException(new InvalidOperationException("unexpected type")),
        };
    }
}