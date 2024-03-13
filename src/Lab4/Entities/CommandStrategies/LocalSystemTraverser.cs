using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Itmo.ObjectOrientedProgramming.Lab4.Common;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;

public class LocalSystemTraverser : IFileSystemTraverser
{
    private const int InitialDepth = 1;
    private const int IndentsNumber = 3;
    private const string InitialPrefix = "";
    private readonly char _fileSymbol;
    private readonly char _directorySymbol;
    private readonly char _indentSymbol;
    private readonly StringBuilder _output;

    public LocalSystemTraverser(char fileSymbol, char directorySymbol, char indentSymbol)
    {
        _output = new StringBuilder();
        _fileSymbol = fileSymbol;
        _directorySymbol = directorySymbol;
        _indentSymbol = indentSymbol;
    }

    public CommandExecutionResult Traverse(ICommandExecutionContext context, int depth)
    {
        FileSystemException.ThrowIfNull(context, nameof(context));

        _output.Clear();
        _output.Append(
            NumberFormatInfo.InvariantInfo,
            $"{_directorySymbol}{context.CurrentPath}\n");
        TraverseTree(Path.Combine(context.ConnectionPath, context.CurrentPath), InitialDepth, depth, InitialPrefix);

        return new CommandExecutionSuccess(context, _output.ToString());
    }

    private void TraverseTree(string initialPath, int currentDepth, int maxDepth, string prefix)
    {
        if (currentDepth > maxDepth)
        {
            return;
        }

        var directoryInfo = new DirectoryInfo(initialPath);
        var fileSystemInfos = directoryInfo.GetFileSystemInfos()
            .OrderBy(fInfo => !fInfo.IsDirectory())
            .ThenBy(fInfo => fInfo.Name)
            .ToList();
        foreach (FileSystemInfo fileSystemInfo in fileSystemInfos.SkipLast(1))
        {
            _output.Append(prefix + "├──");
            if (fileSystemInfo.IsDirectory())
            {
                _output.Append(NumberFormatInfo.InvariantInfo, $"{_directorySymbol}{fileSystemInfo.Name}\n");
                TraverseTree(
                    fileSystemInfo.FullName,
                    currentDepth + 1,
                    maxDepth,
                    prefix + "│" + new string(_indentSymbol, IndentsNumber - 1));
            }
            else
            {
                _output.Append(NumberFormatInfo.InvariantInfo, $"{_fileSymbol}{fileSystemInfo.Name}\n");
            }
        }

        FileSystemInfo? lastFileSystemInfo = fileSystemInfos.LastOrDefault();
        if (lastFileSystemInfo is null)
            return;
        _output.Append(prefix + "└──");
        if (lastFileSystemInfo.IsDirectory())
        {
            _output.Append(NumberFormatInfo.InvariantInfo, $"{_directorySymbol}{lastFileSystemInfo.Name}\n");
            TraverseTree(
                lastFileSystemInfo.FullName,
                currentDepth + 1,
                maxDepth,
                prefix + new string(_indentSymbol, IndentsNumber));
        }
        else
        {
            _output.Append(NumberFormatInfo.InvariantInfo, $"{_fileSymbol}{lastFileSystemInfo.Name}\n");
        }
    }
}