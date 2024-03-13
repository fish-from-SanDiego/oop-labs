using Itmo.Dev.Platform.Postgres.Models;
using Lab5.Application.Abstractions.Entities;
using Lab5.Application.Abstractions.Models;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Abstractions.ResultTypes;
using Lab5.Application.Models;
using Lab5.Infrastructure.DataAccess.Entities;
using Lab5.Infrastructure.DataAccess.Exceptions;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly NpgsqlDataSourceBuilder _dataSourceBuilder;

    public AccountRepository(PostgresConnectionConfiguration configuration)
    {
        RepositoryException.ThrowIfNull(configuration);

        _dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.ToConnectionString());
    }

    public FetchedAccount FindAccountById(long accountId)
    {
        const string sql = """
                           select account_id, account_balance, pin_code
                           from accounts
                           where account_id = :accountId
                           """;

        using NpgsqlDataSource dataSource = _dataSourceBuilder.Build();
        using NpgsqlCommand command = dataSource.CreateCommand(sql);
        command.Parameters.Add(new NpgsqlParameter<long>("accountId", accountId));

        NpgsqlDataReader reader;
        try
        {
            reader = command.ExecuteReader();
        }
        catch (NpgsqlException)
        {
            return new FetchedAccount(null, new GetError("Failed to get the account (failed to connect database)"));
        }

        Account? account = reader.Read() is false
            ? null
            : new Account(
                Id: reader.GetInt64(0),
                Balance: reader.GetInt64(1),
                PinCode: reader.GetInt32(2));

        reader.Dispose();
        return new FetchedAccount(account, new GetSuccess());
    }

    public RegisterResult<Account> RegisterAccount(long accountId, int pinCode)
    {
        const string sql = """
                           insert into accounts
                           (account_id, account_balance, pin_code)
                           values (:accountId, 0, :pinCode)
                           """;
        const string badConnectionMessage = "Failed to register account (failed to connect database)";

        FetchedAccount fetchedAccount = FindAccountById(accountId);
        Account? account = fetchedAccount.Account;

        if (account is not null)
            return new RegisterError<Account>("Account with such Id already exists");

        if (fetchedAccount.GetResult is GetError)
            return new RegisterError<Account>(badConnectionMessage);

        using NpgsqlDataSource dataSource = _dataSourceBuilder.Build();
        using NpgsqlCommand command = dataSource.CreateCommand(sql);
        command.Parameters.Add(new NpgsqlParameter<long>("accountId", accountId));
        command.Parameters.Add(new NpgsqlParameter<int>("pinCode", pinCode));

        try
        {
            command.ExecuteNonQuery();
        }
        catch (NpgsqlException)
        {
            return new RegisterError<Account>(badConnectionMessage);
        }

        return new RegisterSuccess<Account>(new Account(accountId, 0, pinCode));
    }

    public UpdateResult UpdateAccountBalance(long accountId, long newBalance)
    {
        const string sql = """
                           update accounts
                           set account_balance = :newBalance
                           where account_id = :accountId;

                           insert into account_transaction_histories
                           (account_id, transaction_number, balance_before_transaction, balance_after_transaction)
                           values (
                                   :accountId,
                                   (select coalesce(max(transaction_number), 0)
                                    from account_transaction_histories
                                    where account_id = :accountId) + 1,
                                   :oldBalance,
                                   :newBalance)
                           """;
        const string badConnectionMessage = "Failed to update balance (failed to connect database)";

        FetchedAccount fetchedAccount = FindAccountById(accountId);
        Account? account = fetchedAccount.Account;

        if (fetchedAccount.GetResult is GetError)
            return new UpdateError(badConnectionMessage);

        if (account is null)
            return new UpdateError("Account with such Id doesn't exist");

        using NpgsqlDataSource dataSource = _dataSourceBuilder.Build();
        using NpgsqlCommand command = dataSource.CreateCommand(sql);
        command.Parameters.Add(new NpgsqlParameter<long>("accountId", accountId));
        command.Parameters.Add(new NpgsqlParameter<long>("newBalance", newBalance));
        command.Parameters.Add(new NpgsqlParameter<long>("oldBalance", account.Balance));

        try
        {
            command.ExecuteNonQuery();
        }
        catch (NpgsqlException ex)
        {
            return new UpdateError(badConnectionMessage + ex.Message);
        }

        return new UpdateSuccess($"Balance updated successfully: new balance is {newBalance}");
    }

    public IFetchedCollection<Transaction> GetAllTransactionsByAccountId(long accountId)
    {
        const string sql = """
                           select account_id,
                                  balance_before_transaction,
                                  balance_after_transaction
                           from account_transaction_histories
                           where account_id = :accountId
                           order by transaction_number
                           """;

        using NpgsqlDataSource dataSource = _dataSourceBuilder.Build();
        using NpgsqlCommand command = dataSource.CreateCommand(sql);
        command.Parameters.Add(new NpgsqlParameter<long>("accountId", accountId));
        var resultCollection = Enumerable.Empty<Transaction>().ToList();

        NpgsqlDataReader reader;

        try
        {
            reader = command.ExecuteReader();
        }
        catch (NpgsqlException)
        {
            return new FetchedCollection<Transaction>(
                resultCollection,
                new GetError("Failed to get the transactions (failed to connect database)"));
        }

        while (reader.Read())
        {
            resultCollection.Add(new Transaction(
                AccountId: reader.GetInt64(0),
                BalanceBeforeTransaction: reader.GetInt64(1),
                BalanceAfterTransaction: reader.GetInt64(2)));
        }

        reader.Dispose();
        return new FetchedCollection<Transaction>(resultCollection, new GetSuccess());
    }
}