using Lab5.Application.Abstractions.Entities;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Abstractions.ResultTypes;
using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;

namespace Lab5.Application;

public class AdminLoginService : IAdminLoginService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ISystemPasswordRepository _systemPasswordRepository;

    public AdminLoginService(IAccountRepository accountRepository, ISystemPasswordRepository systemPasswordRepository)
    {
        _accountRepository = accountRepository;
        _systemPasswordRepository = systemPasswordRepository;
    }

    public AdminLoginResult Login(string password)
    {
        IFetchedCollection<string> passwordCollection = _systemPasswordRepository.GetAllPasswords();

        if (passwordCollection.GetResult is GetError error)
        {
            return new AdminLoginError(error.Message);
        }

        if (!passwordCollection.Contains(password))
        {
            return new AdminAccessDenied();
        }

        return new AdminLoginSuccess(new AdminService(_accountRepository));
    }
}