using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Common;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public class FileMoveCommand : IFileSystemCommand
{
    private readonly IReadOnlyDictionary<FileSystemType, IFileMover> _fileMovers;
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileMoveCommand(
        string sourcePath,
        string destinationPath,
        IEnumerable<SystemTypeBasedStrategy<IFileMover>> fileMovers)
    {
        FileSystemException.ThrowIfNull(sourcePath, nameof(sourcePath));
        FileSystemException.ThrowIfNull(destinationPath, nameof(destinationPath));
        FileSystemException.ThrowIfNull(fileMovers, nameof(fileMovers));

        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
        _fileMovers = fileMovers.ToStrategyDictionary();
    }

    public FileMoveCommand(
        string sourcePath,
        string destinationPath,
        params SystemTypeBasedStrategy<IFileMover>[] fileMovers)
        : this(sourcePath, destinationPath, fileMovers as IEnumerable<SystemTypeBasedStrategy<IFileMover>>)
    {
    }

    public CommandExecutionResult Execute(ICommandExecutionContext context)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        return !_fileMovers.ContainsKey(context.FileSystemType)
            ? new CommandExecutionError("Unable to move file")
            : _fileMovers[context.FileSystemType].Move(context, _sourcePath, _destinationPath);
    }
}