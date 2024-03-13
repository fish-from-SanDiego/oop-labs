using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Common;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Entities;

public class UserRecipient : ITerminalMessageRecipient
{
    private readonly List<MessageWithStatus> _messages;

    public UserRecipient()
    {
        _messages = new List<MessageWithStatus>();
    }

    public int KeptMessagesCount => _messages.Count;

    public Message GetMessageByPosition(int positionInHistory)
    {
        CheckPosition(positionInHistory);

        return _messages[positionInHistory].Message;
    }

    public UserMessageStatus GetMessageStatusByPosition(int positionInHistory)
    {
        CheckPosition(positionInHistory);

        return _messages[positionInHistory].Status;
    }

    public void SetMessageReadByPosition(int positionInHistory)
    {
        CheckPosition(positionInHistory);
        if (_messages[positionInHistory].Status is UserMessageRead)
        {
            throw new MessageSystemException(
                "message status error",
                new InvalidOperationException("attempt to set already read message read"));
        }

        _messages[positionInHistory] = new MessageWithStatus(_messages[positionInHistory].Message, new UserMessageRead());
    }

    public MessageReceptionResult ReceiveMessage(Message message)
    {
        _messages.Add(new MessageWithStatus(message, new UserMessageUnread()));

        return new MessageReceptionSuccess();
    }

    private void CheckPosition(int positionInHistory)
    {
        if (positionInHistory < 0 || positionInHistory >= _messages.Count)
        {
            throw new MessageSystemException(
                "position should be valid",
                new ArgumentException("value should be in valid interval", nameof(positionInHistory)));
        }
    }

    private record MessageWithStatus(Message Message, UserMessageStatus Status);
}