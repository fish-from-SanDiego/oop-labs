using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Common;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Entities;

public class Topic
{
    private readonly IMessageRecipient _messageRecipient;

    public Topic(string name, IMessageRecipient messageRecipient)
    {
        MessageSystemException.ThrowIfNull(name, nameof(name));
        MessageSystemException.ThrowIfNull(messageRecipient, nameof(messageRecipient));

        Name = name;
        _messageRecipient = messageRecipient;
    }

    public string Name { get; }

    public void ForwardMessage(Message message)
    {
        _messageRecipient.ReceiveMessage(message);
    }
}