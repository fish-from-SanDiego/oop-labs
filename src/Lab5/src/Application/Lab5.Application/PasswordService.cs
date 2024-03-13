using Lab5.Application.Abstractions.Entities;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Abstractions.ResultTypes;
using Lab5.Application.Contracts;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Application.Exceptions;

namespace Lab5.Application;

public class PasswordService : IPasswordService
{
    private readonly ISystemPasswordRepository _systemPasswordRepository;

    public PasswordService(ISystemPasswordRepository systemPasswordRepository)
    {
        _systemPasswordRepository = systemPasswordRepository;
    }

    public OperationResult<string> RegisterPassword(string password)
    {
        RegisterResult<string> registerResult = _systemPasswordRepository.RegisterPassword(password);

        return registerResult switch
        {
            RegisterError<string> error => new OperationError<string>(error.Message),
            RegisterSuccess<string> success => new OperationSuccess<string>(success.Value),
            _ => throw new AccountServiceException(new InvalidOperationException("unexpected type")),
        };
    }

    public OperationResult<int> GetPasswordsCount()
    {
        IFetchedCollection<string> passwordCollection = _systemPasswordRepository.GetAllPasswords();

        if (passwordCollection.GetResult is GetError error)
        {
            return new OperationError<int>(error.Message);
        }

        return new OperationSuccess<int>(passwordCollection.Count);
    }
}