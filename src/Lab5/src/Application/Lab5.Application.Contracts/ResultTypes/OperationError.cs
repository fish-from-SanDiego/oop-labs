namespace Lab5.Application.Contracts.ResultTypes;

public sealed record OperationError<T>(string Message) : OperationResult<T>;