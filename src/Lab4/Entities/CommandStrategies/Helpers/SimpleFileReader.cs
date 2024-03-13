using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;

public class SimpleFileReader : ILocalFileReader
{
    private const string SupportedFormat = ".txt";

    public bool TryReadFile(string path, [MaybeNullWhen(false)] out string? result)
    {
        FileSystemException.ThrowIfNull(path, nameof(path));
        result = null;

        if (!Path.IsPathRooted(path) || !File.Exists(path) ||
            !Path.GetExtension(path).Equals(SupportedFormat, StringComparison.Ordinal))
            return false;

        try
        {
            result = File.ReadAllText(path);
        }
        catch (IOException)
        {
            return false;
        }

        return true;
    }
}