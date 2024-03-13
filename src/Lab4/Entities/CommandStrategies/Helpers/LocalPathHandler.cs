using System;
using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;

public class LocalPathHandler : IPathHandler
{
    public string? HandlePath(string basePath, string currentPath, string requestedPath)
    {
        FileSystemException.ThrowIfNull(basePath);

        string pathFromAbsolute = Path.GetFullPath(Path.Combine(basePath, requestedPath));
        string pathFromRelative = Path.GetFullPath(Path.Combine(currentPath, requestedPath));
        string? result = null;
        if (Path.IsPathRooted(pathFromAbsolute) && Path.Exists(pathFromAbsolute))
            result = pathFromAbsolute;
        else if (Path.IsPathRooted(pathFromRelative) && Path.Exists(pathFromRelative))
            result = pathFromRelative;
        if (result is not null && PathIsValid(basePath, result))
        {
            return result;
        }

        return null;
    }

    private static bool PathIsValid(string basePath, string newAbsolutePath)
    {
        return !((basePath.StartsWith(newAbsolutePath, StringComparison.Ordinal)
                  && basePath != newAbsolutePath) ||
                 !newAbsolutePath.StartsWith(basePath, StringComparison.Ordinal));
    }
}