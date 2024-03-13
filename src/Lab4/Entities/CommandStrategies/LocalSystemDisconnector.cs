using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public class LocalSystemDisconnector : IFileSystemDisconnector
{
    public CommandExecutionResult Disconnect(ICommandExecutionContext context)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        if (context.FileSystemType is not LocalFileSystem)
            return new CommandExecutionError("Unable to disconnect from unconnected system");

        return new CommandExecutionSuccess(
            new CommandExecutionContext(new FileSystemDisconnected(), string.Empty, string.Empty),
            "Disconnection successful");
    }
}