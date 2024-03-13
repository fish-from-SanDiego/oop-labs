using Lab5.Application.Contracts.ResultTypes;

namespace Lab5.Application.Contracts;

public interface IAccountLoginService
{
    LoginResult Login(long accountId, int pinCode);
}