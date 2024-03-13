using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Abstractions.ResultTypes;
using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Application.Exceptions;
using Lab5.Application.Models;

namespace Lab5.Application;

public class AdminService : IAdminService
{
    private readonly IAccountRepository _accountRepository;

    public AdminService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public OperationResult<Account> RegisterAccount(long accountId, int pinCode)
    {
        RegisterResult<Account> registerResult = _accountRepository.RegisterAccount(accountId, pinCode);
        return registerResult switch
        {
            RegisterError<Account> error => new OperationError<Account>(error.Message),
            RegisterSuccess<Account> success => new OperationSuccess<Account>(success.Value),
            _ => throw new AccountServiceException(new InvalidOperationException("unexpected type")),
        };
    }
}