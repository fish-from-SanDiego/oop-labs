using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;

public record CommandExecutionSuccess(ICommandExecutionContext NewContext, string Output) : CommandExecutionResult;