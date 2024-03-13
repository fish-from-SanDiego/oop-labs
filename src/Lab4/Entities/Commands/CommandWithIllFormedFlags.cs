using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public class CommandWithIllFormedFlags : IFileSystemCommand
{
    private const string ErrorMessage = "Check command flags";

    public CommandExecutionResult Execute(ICommandExecutionContext context)
    {
        return new CommandExecutionError(ErrorMessage);
    }
}