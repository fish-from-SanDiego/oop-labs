using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Common;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;
using Itmo.ObjectOrientedProgramming.Lab3.Output.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Entities;

public class MessengerRecipient : ITerminalMessageRecipient
{
    private readonly IMessenger _messenger;

    public MessengerRecipient(IMessenger messenger)
    {
        MessageSystemException.ThrowIfNull(messenger, nameof(messenger));

        _messenger = messenger;
    }

    public MessageReceptionResult ReceiveMessage(Message message)
    {
        MessageSystemException.ThrowIfNull(message, nameof(message));

        _messenger.OutputMessage($"Header: {message.Header}\n{message.Body}");

        return new MessageReceptionSuccess();
    }
}