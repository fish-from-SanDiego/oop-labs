using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public class LocalFileRenamer : IFileRenamer
{
    private readonly IPathHandler _pathHandler;

    public LocalFileRenamer(IPathHandler pathHandler)
    {
        FileSystemException.ThrowIfNull(pathHandler, nameof(pathHandler));

        _pathHandler = pathHandler;
    }

    public CommandExecutionResult Rename(ICommandExecutionContext context, string path, string newName)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));
        FileSystemException.ThrowIfNull(path, nameof(path));
        FileSystemException.ThrowIfNull(newName, nameof(newName));

        string? absolutePath = _pathHandler.HandlePath(context.ConnectionPath, context.CurrentPath, path);
        if (absolutePath is null || !File.Exists(absolutePath))
            return new CommandExecutionError("Invalid path");
        string fileName = Path.GetFileName(absolutePath);
        try
        {
            File.Move(absolutePath, Path.Combine(Path.GetDirectoryName(absolutePath) ?? string.Empty, newName));
        }
        catch (IOException)
        {
            return new CommandExecutionError("Error while renaming file");
        }

        return new CommandExecutionSuccess(context, $"File {fileName} renamed to {newName} successfully");
    }
}