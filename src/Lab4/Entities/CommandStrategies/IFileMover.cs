using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public interface IFileMover
{
    public CommandExecutionResult Move(ICommandExecutionContext context, string sourcePath, string destinationPath);
}