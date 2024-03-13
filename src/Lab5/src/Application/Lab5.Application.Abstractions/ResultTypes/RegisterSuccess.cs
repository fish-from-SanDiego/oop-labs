namespace Lab5.Application.Abstractions.ResultTypes;

public sealed record RegisterSuccess<T>(T Value) : RegisterResult<T>;