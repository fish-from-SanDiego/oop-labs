using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Common;

public static class FileSystemInfoExtensions
{
    public static bool IsDirectory(this FileSystemInfo fileSystemInfo)
    {
        FileSystemException.ThrowIfNull(fileSystemInfo, nameof(fileSystemInfo));

        return (fileSystemInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
    }
}