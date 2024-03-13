using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Common;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Entities;

public class MessageRecipientGroup : ICompoundMessageRecipient
{
    private readonly IReadOnlyCollection<IMessageRecipient> _groupMembers;

    private MessageRecipientGroup(IEnumerable<IMessageRecipient> groupMembers)
    {
        _groupMembers = groupMembers.ToImmutableList();
    }

    public MessageReceptionResult ReceiveMessage(Message message)
    {
        foreach (IMessageRecipient recipient in _groupMembers)
        {
            recipient.ReceiveMessage(message);
        }

        return new MessageReceptionSuccess();
    }

    public Builder Direct(Builder recipientGroupBuilder)
    {
        MessageSystemException.ThrowIfNull(recipientGroupBuilder, nameof(recipientGroupBuilder));

        return recipientGroupBuilder.WithMembers(_groupMembers);
    }

    public class Builder
    {
        private List<IMessageRecipient> _groupMembers;

        public Builder()
        {
            _groupMembers = new List<IMessageRecipient>();
        }

        public Builder AddMember(IMessageRecipient messageRecipient)
        {
            _groupMembers.Add(messageRecipient);

            return this;
        }

        public Builder ClearMembers()
        {
            _groupMembers.Clear();
            return this;
        }

        public Builder WithMembers(IEnumerable<IMessageRecipient> groupMembers)
        {
            MessageSystemException.ThrowIfNull(groupMembers, nameof(groupMembers));
            _groupMembers = groupMembers.ToList();

            return this;
        }

        public MessageRecipientGroup Build() => new MessageRecipientGroup(_groupMembers);
    }
}