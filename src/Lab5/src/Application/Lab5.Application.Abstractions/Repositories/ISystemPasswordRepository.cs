using Lab5.Application.Abstractions.Entities;
using Lab5.Application.Abstractions.ResultTypes;

namespace Lab5.Application.Abstractions.Repositories;

public interface ISystemPasswordRepository
{
    IFetchedCollection<string> GetAllPasswords();
    RegisterResult<string> RegisterPassword(string password);
}