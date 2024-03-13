namespace Lab5.Application.Contracts.ResultTypes;

public sealed record LoginError(string Message) : LoginResult;