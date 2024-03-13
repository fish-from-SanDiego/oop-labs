using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public interface IFileSystemTraverser
{
    CommandExecutionResult Traverse(ICommandExecutionContext context, int depth);
}