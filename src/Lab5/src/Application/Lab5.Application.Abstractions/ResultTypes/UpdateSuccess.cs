namespace Lab5.Application.Abstractions.ResultTypes;

public sealed record UpdateSuccess(string Message) : UpdateResult(Message);