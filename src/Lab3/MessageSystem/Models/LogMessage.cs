using System;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;

public record LogMessage
{
    public LogMessage(string message, DateTime logTime)
    {
        MessageSystemException.ThrowIfNull(message, nameof(message));

        Message = message;
        LogTime = logTime;
    }

    public string Message { get; }
    public DateTime LogTime { get; }
}