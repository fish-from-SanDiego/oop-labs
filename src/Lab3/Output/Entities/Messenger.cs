using Itmo.ObjectOrientedProgramming.Lab3.Output.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Output.Entities;

public class Messenger : IMessenger
{
    private readonly IContinuousOutputWriter _outputWriter;

    public Messenger(IContinuousOutputWriter writer)
    {
        OutputException.ThrowIfNull(writer, nameof(writer));
        _outputWriter = writer;
    }

    public void OutputMessage(string message)
    {
        _outputWriter.Write($"Messenger:\n{message}");
    }
}