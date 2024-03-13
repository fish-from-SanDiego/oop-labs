using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public interface IFileSystemPathChanger
{
    CommandExecutionResult GotoPath(ICommandExecutionContext context, string path);
}