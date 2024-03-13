namespace Lab5.Application.Contracts.ResultTypes;

public sealed record OperationSuccess<T>(T Value) : OperationResult<T>;