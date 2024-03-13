using Lab5.Application.Contracts.ResultTypes;

namespace Lab5.Application.Contracts;

public interface IAdminLoginService
{
    AdminLoginResult Login(string password);
}