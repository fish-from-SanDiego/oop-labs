using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Common;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public class FileShowCommand : IFileSystemCommand
{
    private readonly IReadOnlyDictionary<OutputMode, ITextWriter> _textWriters;
    private readonly IReadOnlyDictionary<FileSystemType, IFileShower> _fileShowers;
    private readonly OutputMode _outputMode;
    private readonly string _path;

    public FileShowCommand(
        OutputMode outputMode,
        string path,
        IEnumerable<OutputModeBasedStrategy<ITextWriter>> textWriters,
        IEnumerable<SystemTypeBasedStrategy<IFileShower>> fileShowers)
    {
        FileSystemException.ThrowIfNull(textWriters, nameof(textWriters));
        FileSystemException.ThrowIfNull(fileShowers, nameof(fileShowers));
        FileSystemException.ThrowIfNull(outputMode, nameof(outputMode));
        FileSystemException.ThrowIfNull(path, nameof(path));

        _textWriters = textWriters.ToStrategyDictionary();
        _fileShowers = fileShowers.ToStrategyDictionary();
        _outputMode = outputMode;
        _path = path;
    }

    internal OutputMode OutputMode => _outputMode;

    public CommandExecutionResult Execute(ICommandExecutionContext context)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        return _textWriters.ContainsKey(_outputMode)
            ? _fileShowers.ContainsKey(context.FileSystemType)
                ? _fileShowers[context.FileSystemType].Show(context, _path, _textWriters[_outputMode])
                : new CommandExecutionError("Unable to show file")
            : new CommandExecutionError("Unknown output mode");
    }
}