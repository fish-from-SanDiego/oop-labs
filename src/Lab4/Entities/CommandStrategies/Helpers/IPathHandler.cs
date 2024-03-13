namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;

public interface IPathHandler
{
    string? HandlePath(string basePath, string currentPath, string requestedPath);
}