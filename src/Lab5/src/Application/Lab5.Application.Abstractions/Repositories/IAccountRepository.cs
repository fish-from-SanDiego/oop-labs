using Lab5.Application.Abstractions.Entities;
using Lab5.Application.Abstractions.Models;
using Lab5.Application.Abstractions.ResultTypes;
using Lab5.Application.Models;

namespace Lab5.Application.Abstractions.Repositories;

public interface IAccountRepository
{
    FetchedAccount FindAccountById(long accountId);
    RegisterResult<Account> RegisterAccount(long accountId, int pinCode);
    UpdateResult UpdateAccountBalance(long accountId, long newBalance);
    IFetchedCollection<Transaction> GetAllTransactionsByAccountId(long accountId);
}