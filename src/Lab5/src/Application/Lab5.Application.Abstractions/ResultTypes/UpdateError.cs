namespace Lab5.Application.Abstractions.ResultTypes;

public sealed record UpdateError(string Message) : UpdateResult(Message);