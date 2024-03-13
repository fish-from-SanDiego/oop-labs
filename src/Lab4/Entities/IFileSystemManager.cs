namespace Itmo.ObjectOrientedProgramming.Lab4.Entities;

public interface IFileSystemManager
{
    string CurrentPath { get; }
    string Handle(string request);
}