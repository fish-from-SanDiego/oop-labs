using Lab5.Application.Abstractions.Models;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Abstractions.ResultTypes;
using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;

namespace Lab5.Application;

public class AccountLoginService : IAccountLoginService
{
    private readonly IAccountRepository _accountRepository;

    public AccountLoginService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public LoginResult Login(long accountId, int pinCode)
    {
        FetchedAccount fetchedAccount = _accountRepository.FindAccountById(accountId);
        if (fetchedAccount.GetResult is GetError error)
        {
            return new LoginError(error.Message);
        }

        if (fetchedAccount.Account is null)
        {
            return new LoginError("Account with such Id doesn't exist");
        }

        if (!fetchedAccount.Account.PinCode.Equals(pinCode))
        {
            return new LoginError("Wrong pin code");
        }

        return new LoginSuccess(
            new CurrentAccountService(_accountRepository, accountId));
    }
}