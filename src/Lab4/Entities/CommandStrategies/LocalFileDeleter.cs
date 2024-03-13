using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public class LocalFileDeleter : IFileDeleter
{
    private readonly IPathHandler _pathHandler;

    public LocalFileDeleter(IPathHandler pathHandler)
    {
        FileSystemException.ThrowIfNull(pathHandler, nameof(pathHandler));

        _pathHandler = pathHandler;
    }

    public CommandExecutionResult Delete(ICommandExecutionContext context, string path)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));
        FileSystemException.ThrowIfNull(path, nameof(path));

        string? absolutePath = _pathHandler.HandlePath(context.ConnectionPath, context.CurrentPath, path);
        if (absolutePath is null || !File.Exists(absolutePath))
            return new CommandExecutionError("Invalid path");
        string fileName = Path.GetFileName(absolutePath);
        try
        {
            File.Delete(absolutePath);
        }
        catch (IOException)
        {
            return new CommandExecutionError("Error while deleting file");
        }

        return new CommandExecutionSuccess(context, $"File {fileName} deleted successfully");
    }
}