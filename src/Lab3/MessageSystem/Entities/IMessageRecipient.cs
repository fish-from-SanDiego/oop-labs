using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Entities;

public interface IMessageRecipient
{
    MessageReceptionResult ReceiveMessage(Message message);
}