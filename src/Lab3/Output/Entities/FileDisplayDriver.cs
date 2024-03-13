using System;
using System.Drawing;
using System.IO;
using Itmo.ObjectOrientedProgramming.Lab3.Output.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Output.Entities;

public class FileDisplayDriver : StringBuilderBasedDriver
{
    private readonly FileInfo _fileInfo;

    public FileDisplayDriver(FileInfo fileInfo, Color defaultColor)
        : base(defaultColor)
    {
        OutputException.ThrowIfNull(fileInfo, nameof(fileInfo));
        if (!fileInfo.Exists)
            throw new OutputException("file error", new ArgumentException("file doesn't exist", nameof(fileInfo)));
        if (fileInfo.IsReadOnly)
            throw new OutputException("file error", new ArgumentException("file is readonly", nameof(fileInfo)));

        _fileInfo = fileInfo;
    }

    public FileDisplayDriver(FileInfo fileInfo)
        : this(fileInfo, Color.Empty)
    {
    }

    public override void ClearOutput()
    {
        File.WriteAllBytes(_fileInfo.ToString(), Array.Empty<byte>());
    }

    protected override void DisplayString(string str)
    {
        File.AppendAllText(_fileInfo.ToString(), str);
    }
}