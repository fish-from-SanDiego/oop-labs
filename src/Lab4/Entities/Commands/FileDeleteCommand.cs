using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Common;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public class FileDeleteCommand : IFileSystemCommand
{
    private readonly IReadOnlyDictionary<FileSystemType, IFileDeleter> _fileDeleters;
    private readonly string _path;

    public FileDeleteCommand(string path, IEnumerable<SystemTypeBasedStrategy<IFileDeleter>> fileDeleters)
    {
        FileSystemException.ThrowIfNull(fileDeleters, nameof(fileDeleters));
        FileSystemException.ThrowIfNull(path, nameof(path));

        _fileDeleters = fileDeleters.ToStrategyDictionary();
        _path = path;
    }

    public FileDeleteCommand(string path, params SystemTypeBasedStrategy<IFileDeleter>[] fileDeleters)
        : this(path, fileDeleters as IEnumerable<SystemTypeBasedStrategy<IFileDeleter>>)
    {
    }

    public CommandExecutionResult Execute(ICommandExecutionContext context)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        return _fileDeleters.ContainsKey(context.FileSystemType)
            ? _fileDeleters[context.FileSystemType].Delete(context, _path)
            : new CommandExecutionError("Unable to delete file");
    }
}