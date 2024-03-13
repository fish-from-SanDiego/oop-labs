using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Common;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public class TreeListCommand : IFileSystemCommand
{
    private readonly IReadOnlyDictionary<FileSystemType, IFileSystemTraverser> _traversers;
    private readonly int _depth;

    public TreeListCommand(int depth, IEnumerable<SystemTypeBasedStrategy<IFileSystemTraverser>> traversers)
    {
        FileSystemException.ThrowIfNull(traversers, nameof(traversers));
        if (depth < 0)
        {
            throw new FileSystemException(
                "value validation error",
                new ArgumentException("value should be non-negative", nameof(depth)));
        }

        _depth = depth;
        _traversers = traversers.ToStrategyDictionary();
    }

    public TreeListCommand(int depth, params SystemTypeBasedStrategy<IFileSystemTraverser>[] traversers)
        : this(depth, traversers as IEnumerable<SystemTypeBasedStrategy<IFileSystemTraverser>>)
    {
    }

    internal int Depth => _depth;

    public CommandExecutionResult Execute(ICommandExecutionContext context)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        return !_traversers.ContainsKey(context.FileSystemType)
            ? new CommandExecutionError("Unable to show tree list")
            : _traversers[context.FileSystemType].Traverse(context, _depth);
    }
}