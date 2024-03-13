using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public class CommandExecutionContext : ICommandExecutionContext
{
    public CommandExecutionContext(FileSystemType fileSystemType, string currentPath, string connectionPath)
    {
        FileSystemException.ThrowIfNull(fileSystemType, nameof(fileSystemType));
        FileSystemException.ThrowIfNull(currentPath, nameof(currentPath));
        FileSystemException.ThrowIfNull(connectionPath, nameof(connectionPath));

        FileSystemType = fileSystemType;
        CurrentPath = currentPath;
        ConnectionPath = connectionPath;
    }

    public FileSystemType FileSystemType { get; }
    public string CurrentPath { get; }
    public string ConnectionPath { get; }
}