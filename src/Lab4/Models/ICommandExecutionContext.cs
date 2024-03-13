namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public interface ICommandExecutionContext
{
    FileSystemType FileSystemType { get; }
    string CurrentPath { get; }
    string ConnectionPath { get; }
}