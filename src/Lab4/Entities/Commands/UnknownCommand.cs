using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public class UnknownCommand : IFileSystemCommand
{
    public CommandExecutionResult Execute(ICommandExecutionContext context)
    {
        return new CommandExecutionError("Unknown command; check your writing");
    }
}