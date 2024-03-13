namespace Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;

public record CommandExecutionError(string Message) : CommandExecutionResult;