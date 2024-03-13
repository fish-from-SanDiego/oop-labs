using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Common;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public class TreeGotoCommand : IFileSystemCommand
{
    private readonly IReadOnlyDictionary<FileSystemType, IFileSystemPathChanger> _pathChangers;
    private readonly string _path;

    public TreeGotoCommand(string path, IEnumerable<SystemTypeBasedStrategy<IFileSystemPathChanger>> pathChangers)
    {
        FileSystemException.ThrowIfNull(path, nameof(path));
        FileSystemException.ThrowIfNull(pathChangers, nameof(pathChangers));

        _path = path;
        _pathChangers = pathChangers.ToStrategyDictionary();
    }

    public TreeGotoCommand(string path, params SystemTypeBasedStrategy<IFileSystemPathChanger>[] pathChangers)
        : this(path, pathChangers as IEnumerable<SystemTypeBasedStrategy<IFileSystemPathChanger>>)
    {
    }

    public CommandExecutionResult Execute(ICommandExecutionContext context)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        return !_pathChangers.ContainsKey(context.FileSystemType)
            ? new CommandExecutionError("Unable to goto path")
            : _pathChangers[context.FileSystemType].GotoPath(context, _path);
    }
}