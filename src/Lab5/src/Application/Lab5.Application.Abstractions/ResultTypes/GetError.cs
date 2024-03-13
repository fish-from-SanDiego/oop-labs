namespace Lab5.Application.Abstractions.ResultTypes;

public sealed record GetError(string Message) : GetResult;