using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public class LocalFileMover : IFileMover
{
    private readonly IPathHandler _pathHandler;

    public LocalFileMover(IPathHandler pathHandler)
    {
        FileSystemException.ThrowIfNull(pathHandler, nameof(pathHandler));

        _pathHandler = pathHandler;
    }

    public CommandExecutionResult Move(ICommandExecutionContext context, string sourcePath, string destinationPath)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));
        FileSystemException.ThrowIfNull(sourcePath, nameof(sourcePath));
        FileSystemException.ThrowIfNull(destinationPath, nameof(destinationPath));

        string? sourceAbsolutePath = _pathHandler.HandlePath(context.ConnectionPath, context.CurrentPath, sourcePath);
        string? destinationAbsolutePath =
            _pathHandler.HandlePath(context.ConnectionPath, context.CurrentPath, destinationPath);
        if (sourceAbsolutePath is null || destinationAbsolutePath is null || !File.Exists(sourceAbsolutePath) ||
            !Directory.Exists(destinationAbsolutePath))
            return new CommandExecutionError("Invalid path");
        string fileName = Path.GetFileName(sourceAbsolutePath);
        try
        {
            File.Move(sourceAbsolutePath, Path.Combine(destinationAbsolutePath, fileName));
        }
        catch (IOException)
        {
            return new CommandExecutionError("Error while moving file");
        }

        return new CommandExecutionSuccess(
            context,
            $"File {fileName} successfully moved to {Path.GetRelativePath(context.ConnectionPath, destinationAbsolutePath)}");
    }
}