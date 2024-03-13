using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public class ConnectCommand : IFileSystemCommand
{
    private readonly IFileSystemConnector _connector;
    private readonly string _address;
    private readonly FileSystemType _mode;

    public ConnectCommand(IFileSystemConnector connector, string address, FileSystemType mode)
    {
        FileSystemException.ThrowIfNull(connector, nameof(connector));
        FileSystemException.ThrowIfNull(address, nameof(address));
        FileSystemException.ThrowIfNull(mode, nameof(mode));

        _address = address;
        _mode = mode;
        _connector = connector;
    }

    internal FileSystemType Mode => _mode;

    public CommandExecutionResult Execute(ICommandExecutionContext context)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        return _mode is FileSystemUnknown
            ? new CommandExecutionError("Unknown connection mode")
            : _connector.Connect(context, _address, _mode);
    }
}