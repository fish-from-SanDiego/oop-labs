using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Common;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public class DisconnectCommand : IFileSystemCommand
{
    private readonly IReadOnlyDictionary<FileSystemType, IFileSystemDisconnector> _disconnectors;

    public DisconnectCommand(IEnumerable<SystemTypeBasedStrategy<IFileSystemDisconnector>> disconnectors)
    {
        FileSystemException.ThrowIfNull(disconnectors, nameof(disconnectors));

        _disconnectors = disconnectors.ToStrategyDictionary();
    }

    public DisconnectCommand(params SystemTypeBasedStrategy<IFileSystemDisconnector>[] disconnectors)
        : this(disconnectors as IEnumerable<SystemTypeBasedStrategy<IFileSystemDisconnector>>)
    {
    }

    public CommandExecutionResult Execute(ICommandExecutionContext context)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        return !_disconnectors.ContainsKey(context.FileSystemType)
            ? new CommandExecutionError("Unable to disconnect from file system")
            : _disconnectors[context.FileSystemType].Disconnect(context);
    }
}