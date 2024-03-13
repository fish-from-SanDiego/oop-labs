using Lab5.Application.Contracts.ResultTypes;
using Lab5.Application.Models;

namespace Lab5.Application.Contracts;

public interface IAdminService
{
    OperationResult<Account> RegisterAccount(long accountId, int pinCode);
}