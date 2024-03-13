using Lab5.Application.Contracts.ResultTypes;

namespace Lab5.Application.Contracts;

public interface IPasswordService
{
    OperationResult<string> RegisterPassword(string password);
    OperationResult<int> GetPasswordsCount();
}