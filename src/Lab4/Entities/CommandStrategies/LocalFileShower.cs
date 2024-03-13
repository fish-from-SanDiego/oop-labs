using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public class LocalFileShower : IFileShower
{
    private readonly IPathHandler _pathHandler;
    private readonly ILocalFileReader _reader;

    public LocalFileShower(IPathHandler pathHandler, ILocalFileReader reader)
    {
        FileSystemException.ThrowIfNull(pathHandler, nameof(pathHandler));
        FileSystemException.ThrowIfNull(reader, nameof(reader));

        _pathHandler = pathHandler;
        _reader = reader;
    }

    public CommandExecutionResult Show(ICommandExecutionContext context, string path, ITextWriter writer)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));
        FileSystemException.ThrowIfNull(path, nameof(path));
        FileSystemException.ThrowIfNull(writer, nameof(writer));

        string? absolutePath = _pathHandler.HandlePath(context.ConnectionPath, context.CurrentPath, path);
        if (absolutePath is null || !File.Exists(absolutePath))
            return new CommandExecutionError("Invalid path");
        string fileName = Path.GetFileName(absolutePath);
        if (_reader.TryReadFile(absolutePath, out string? result))
        {
            writer.Output(result);
            return new CommandExecutionSuccess(context, $"File {fileName} showed successfully");
        }

        return new CommandExecutionError($"Error while showing file");
    }
}