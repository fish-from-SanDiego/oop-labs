namespace Lab5.Application.Contracts.ResultTypes;

public sealed record LoginSuccess(ICurrentAccountService CurrentAccountService) : LoginResult;