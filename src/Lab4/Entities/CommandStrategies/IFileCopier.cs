using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public interface IFileCopier
{
    CommandExecutionResult Copy(ICommandExecutionContext context, string sourcePath, string destinationPath);
}