using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public class LocalSystemConnector : IFileSystemConnector
{
    public CommandExecutionResult Connect(ICommandExecutionContext context, string address, FileSystemType mode)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        if (context.FileSystemType is not FileSystemDisconnected)
            return new CommandExecutionError("Attempting to connect before disconnecting from previous system");

        if (!Path.IsPathRooted(address) || !Directory.Exists(address))
            return new CommandExecutionError("Invalid Path");

        return new CommandExecutionSuccess(
            new CommandExecutionContext(new LocalFileSystem(), string.Empty, Path.GetFullPath(address)),
            "Connection successful");
    }
}