using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;

public record Message
{
    public Message(string header, string body, SeverityLevel severity)
    {
        MessageSystemException.ThrowIfNull(header, nameof(header));
        MessageSystemException.ThrowIfNull(body, nameof(body));
        MessageSystemException.ThrowIfNull(severity, nameof(severity));

        Header = header;
        Body = body;
        Severity = severity;
    }

    public string Header { get; }
    public string Body { get; }
    public SeverityLevel Severity { get; }
}