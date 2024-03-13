namespace Lab5.Application.Abstractions.ResultTypes;

public sealed record RegisterError<T>(string Message) : RegisterResult<T>;