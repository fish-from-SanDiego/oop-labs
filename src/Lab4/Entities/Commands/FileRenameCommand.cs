using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Common;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public class FileRenameCommand : IFileSystemCommand
{
    private readonly IReadOnlyDictionary<FileSystemType, IFileRenamer> _fileRenamers;
    private readonly string _path;
    private readonly string _newName;

    public FileRenameCommand(
        string path,
        string newName,
        IEnumerable<SystemTypeBasedStrategy<IFileRenamer>> fileRenamers)
    {
        FileSystemException.ThrowIfNull(fileRenamers, nameof(fileRenamers));
        FileSystemException.ThrowIfNull(path, nameof(path));
        FileSystemException.ThrowIfNull(newName, nameof(newName));

        _fileRenamers = fileRenamers.ToStrategyDictionary();
        _path = path;
        _newName = newName;
    }

    public FileRenameCommand(
        string path,
        string newName,
        params SystemTypeBasedStrategy<IFileRenamer>[] fileRenamers)
        : this(path, newName, fileRenamers as IEnumerable<SystemTypeBasedStrategy<IFileRenamer>>)
    {
    }

    public CommandExecutionResult Execute(ICommandExecutionContext context)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        return _fileRenamers.ContainsKey(context.FileSystemType)
            ? _fileRenamers[context.FileSystemType].Rename(context, _path, _newName)
            : new CommandExecutionError("Unable to rename file");
    }
}