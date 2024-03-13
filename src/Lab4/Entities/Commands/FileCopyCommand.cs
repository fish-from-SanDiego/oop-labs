using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Common;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public class FileCopyCommand : IFileSystemCommand
{
    private readonly IReadOnlyDictionary<FileSystemType, IFileCopier> _fileCopiers;
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileCopyCommand(
        string sourcePath,
        string destinationPath,
        IEnumerable<SystemTypeBasedStrategy<IFileCopier>> fileCopiers)
    {
        FileSystemException.ThrowIfNull(sourcePath, nameof(sourcePath));
        FileSystemException.ThrowIfNull(destinationPath, nameof(destinationPath));
        FileSystemException.ThrowIfNull(fileCopiers, nameof(fileCopiers));

        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
        _fileCopiers = fileCopiers.ToStrategyDictionary();
    }

    public FileCopyCommand(
        string sourcePath,
        string destinationPath,
        params SystemTypeBasedStrategy<IFileCopier>[] fileCopiers)
        : this(sourcePath, destinationPath, fileCopiers as IEnumerable<SystemTypeBasedStrategy<IFileCopier>>)
    {
    }

    public CommandExecutionResult Execute(ICommandExecutionContext context)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        return !_fileCopiers.ContainsKey(context.FileSystemType)
            ? new CommandExecutionError("Unable to copy file")
            : _fileCopiers[context.FileSystemType].Copy(context, _sourcePath, _destinationPath);
    }
}