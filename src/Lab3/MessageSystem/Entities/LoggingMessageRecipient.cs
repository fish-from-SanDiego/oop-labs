using System;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Common;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Services;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Entities;

public class LoggingMessageRecipient : ITerminalMessageRecipient
{
    private readonly ITerminalMessageRecipient _decoratee;
    private readonly ILogger _logger;
    private readonly string _loggingName;

    public LoggingMessageRecipient(ITerminalMessageRecipient messageRecipient, ILogger logger, string loggingName)
    {
        MessageSystemException.ThrowIfNull(messageRecipient, nameof(messageRecipient));
        MessageSystemException.ThrowIfNull(logger, nameof(logger));
        MessageSystemException.ThrowIfNull(loggingName, nameof(loggingName));
        if (string.IsNullOrEmpty(loggingName))
        {
            throw new MessageSystemException(
                "string validation error",
                new ArgumentException("string should be non empty", nameof(loggingName)));
        }

        _decoratee = messageRecipient;
        _logger = logger;
        _loggingName = loggingName;
    }

    public MessageReceptionResult ReceiveMessage(Message message)
    {
        MessageReceptionResult result = _decoratee.ReceiveMessage(message);
        string resultInfo = result switch
        {
            MessageReceptionSuccess _ => "message received",

            MessageReceptionError error => $"message reception failed({error.Description})",

            _ => throw new MessageSystemException(
                "result validation error",
                new InvalidOperationException("unexpected message reception result")),
        };
        _logger.Log(new LogMessage($"{_loggingName}: {resultInfo}", DateTime.Now));

        return result;
    }
}