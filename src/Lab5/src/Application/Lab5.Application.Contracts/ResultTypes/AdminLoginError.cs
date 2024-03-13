namespace Lab5.Application.Contracts.ResultTypes;

public sealed record AdminLoginError(string Message) : AdminLoginResult;