using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;

public interface ILocalFileReader
{
    bool TryReadFile(string path, [MaybeNullWhen(false)] out string? result);
}