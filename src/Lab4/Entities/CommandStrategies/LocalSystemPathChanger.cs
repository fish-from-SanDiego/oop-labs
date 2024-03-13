using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public class LocalSystemPathChanger : IFileSystemPathChanger
{
    private readonly IPathHandler _pathHandler;

    public LocalSystemPathChanger(IPathHandler pathHandler)
    {
        FileSystemException.ThrowIfNull(pathHandler, nameof(pathHandler));

        _pathHandler = pathHandler;
    }

    public CommandExecutionResult GotoPath(ICommandExecutionContext context, string path)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));
        FileSystemException.ThrowIfNull(path, nameof(path));

        string? newAbsolutePath = _pathHandler.HandlePath(context.ConnectionPath, context.CurrentPath, path);
        if (newAbsolutePath is null)
            return new CommandExecutionError("Invalid path");
        string newCurrentPath = Path.GetRelativePath(context.ConnectionPath, newAbsolutePath);

        return new CommandExecutionSuccess(
            new CommandExecutionContext(
                context.FileSystemType,
                newCurrentPath,
                context.ConnectionPath),
            $"Successfully went to {newCurrentPath}");
    }
}