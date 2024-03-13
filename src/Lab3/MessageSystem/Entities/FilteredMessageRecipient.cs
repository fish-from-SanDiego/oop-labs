using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Common;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Entities;

public class FilteredMessageRecipient : ITerminalMessageRecipient
{
    private readonly ITerminalMessageRecipient _messageRecipient;
    private readonly HashSet<SeverityLevel> _acceptedSeverityLevels;

    public FilteredMessageRecipient(
        ITerminalMessageRecipient messageRecipient,
        IEnumerable<SeverityLevel> acceptedSeverityLevels)
    {
        MessageSystemException.ThrowIfNull(messageRecipient, nameof(messageRecipient));
        MessageSystemException.ThrowIfNull(acceptedSeverityLevels, nameof(acceptedSeverityLevels));

        _messageRecipient = messageRecipient;
        _acceptedSeverityLevels = acceptedSeverityLevels.ToHashSet();
    }

    public FilteredMessageRecipient(
        ITerminalMessageRecipient messageRecipient,
        params SeverityLevel[] acceptedSeverityLevels)
        : this(messageRecipient, acceptedSeverityLevels as IEnumerable<SeverityLevel>)
    {
    }

    public bool AcceptsSeverityLevel(SeverityLevel severityLevel) => _acceptedSeverityLevels.Contains(severityLevel);

    public bool RemoveSeverityLevel(SeverityLevel severityLevel)
    {
        return _acceptedSeverityLevels.Remove(severityLevel);
    }

    public void AddSeverityLevel(SeverityLevel severityLevel)
    {
        _acceptedSeverityLevels.Add(severityLevel);
    }

    public MessageReceptionResult ReceiveMessage(Message message)
    {
        MessageSystemException.ThrowIfNull(message);

        return !_acceptedSeverityLevels.Contains(message.Severity)
            ? new MessageReceptionError("message not received due to unaccepted severity level")
            : _messageRecipient.ReceiveMessage(message);
    }
}