using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Common;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Entities;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Services;
using Itmo.ObjectOrientedProgramming.Lab3.Output.Entities;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class MessageSystemTests
{
    private const string StepsToBecomeSigmaMale =
        "Step #1. Be Yourself\nStep #2. Live by your own rules\nStep #3. Let go of arrogance\n" +
        "Step #4. Lead without being bossy\nStep #5. Be flexible and adaptable\nStep #6. Practice empathic listening\n" +
        "Step #7. Sharpen your emotional intelligence\nStep #8. Develop self-reliance\n" +
        "Step #9. See solitude as a positive\nStep #10. Ditch competitiveness\nStep #11. Treat others respectfully";

    [Fact]
    public void ShouldSaveMessageInUserAsUnread()
    {
        // Arrange
        var user = new UserRecipient();
        var message = new Message("Header", "Body", new SeverityLevel(10));

        // Act
        user.ReceiveMessage(message);

        // Assert
        Assert.Equal(1, user.KeptMessagesCount);
        Assert.IsType<UserMessageUnread>(user.GetMessageStatusByPosition(0));
    }

    [Fact]
    public void ShouldMarkMessageAsReadWhenCallingMethodOnUnread()
    {
        // Arrange
        var user = new UserRecipient();
        var message = new Message("Killer Fish", "Killer fish from San-Diego", new SeverityLevel(999));
        user.ReceiveMessage(message);

        // Act
        user.SetMessageReadByPosition(0);

        // Assert
        Assert.IsType<UserMessageRead>(user.GetMessageStatusByPosition(0));
    }

    [Fact]
    public void ShouldThrowWhenAttemptingToReadAlreadyReadMessage()
    {
        // Arrange
        var user = new UserRecipient();
        var message = new Message("It's nerdin' time", "🤓🤓🤓🤓🤓🤓🤓🤓", new SeverityLevel(0));
        user.ReceiveMessage(message);

        // Act
        user.SetMessageReadByPosition(0);

        // Assert
        MessageSystemException exception =
            Assert.Throws<MessageSystemException>(() => user.SetMessageReadByPosition(0));
        Assert.Equal("message status error", exception.Message);
        Assert.IsType<InvalidOperationException>(exception.InnerException);
    }

    [Fact]
    public void GroupShouldTransferMessageToItsMembers()
    {
        // Arrange
        var user1 = new UserRecipient();
        var user2 = new UserRecipient();
        MessageRecipientGroup group1 = new MessageRecipientGroup.Builder().AddMember(user1).Build();
        MessageRecipientGroup group2 = new MessageRecipientGroup.Builder().AddMember(user2).AddMember(group1).Build();
        var message1 = new Message("msg1", "a", new SeverityLevel(0));
        var message2 = new Message("msg2", "b", new SeverityLevel(0));

        // Act
        group1.ReceiveMessage(message1);
        group2.ReceiveMessage(message2);

        // Assert
        Assert.Equal(2, user1.KeptMessagesCount);
        Assert.Equal(1, user2.KeptMessagesCount);
    }

    [Fact]
    public void FilteredRecipientShouldPassOnlyAcceptedSeverityLevels()
    {
        // Arrange
        var receivedMessages = new List<Message>();
        var message1 = new Message("msg1", "aaa", new SeverityLevel(1));
        var message2 = new Message("msg2", "bbb", new SeverityLevel(10));
        ITerminalMessageRecipient mockedRecipient = Substitute.For<ITerminalMessageRecipient>();
        mockedRecipient
            .When(r => r.ReceiveMessage(message1))
            .Do(_ => receivedMessages.Add(message1));
        mockedRecipient
            .When(r => r.ReceiveMessage(message2))
            .Do(_ => receivedMessages.Add(message2));

        var filteredRecipient =
            new FilteredMessageRecipient(mockedRecipient, new SeverityLevel(0), new SeverityLevel(1));

        // Act
        filteredRecipient.ReceiveMessage(message1);
        filteredRecipient.ReceiveMessage(message2);

        // Assert
        Assert.Single(receivedMessages);
        Assert.Equal(message1, receivedMessages.First());
    }

    [Fact]
    public void LogShouldBeWrittenCorrectlyWhenLoggingIsOn()
    {
        // Arrange
        var logMessages = new List<LogMessage>();
        ILogger logger = Substitute.For<ILogger>();
        logger
            .When(l => l.Log(Arg.Any<LogMessage>()))
            .Do(info => logMessages.Add(info.Arg<LogMessage>()));
        var sigmaMessage = new Message(
            "11 Steps to Be More of a Sigma Male",
            StepsToBecomeSigmaMale,
            new SeverityLevel(10000));
        var message2 = new Message("msg2", "hmm...", new SeverityLevel(0));
        var recipient =
            new LoggingMessageRecipient(
                new FilteredMessageRecipient(new UserRecipient(), new SeverityLevel(0)),
                logger,
                "Test Recipient");

        // Act
        recipient.ReceiveMessage(sigmaMessage);
        recipient.ReceiveMessage(message2);

        // Assert
        Assert.Equal(2, logMessages.Count);
        Assert.Equal(
            "Test Recipient: message reception failed(message not received due to unaccepted severity level)",
            logMessages[0].Message);
        Assert.Equal("Test Recipient: message received", logMessages[1].Message);
    }

    [Fact]
    public void MessengerShouldWriteCorrectlyToOutput()
    {
        // Arrange
        var messages = new List<string>();
        IContinuousOutputWriter outputWriter = Substitute.For<IContinuousOutputWriter>();
        outputWriter
            .When(o => o.Write(Arg.Any<string>()))
            .Do(info => messages.Add(info.Arg<string>()));
        var recipient = new MessengerRecipient(new Messenger(outputWriter));
        var message1 = new Message("msg1", "a", new SeverityLevel(0));
        var message2 = new Message("msg2", "b", new SeverityLevel(0));

        // Act
        recipient.ReceiveMessage(message1);
        recipient.ReceiveMessage(message2);

        // Assert
        Assert.Equal(2, messages.Count);
        Assert.Equal("Messenger:\nHeader: msg1\na", messages[0]);
        Assert.Equal("Messenger:\nHeader: msg2\nb", messages[1]);
    }

    [Fact]
    public void DisplayShouldWriteCorrectlyToOutput()
    {
        // Arrange
        string displayedString = string.Empty;
        string buffer = string.Empty;
        IDisplay display = Substitute.For<IDisplay>();
        display
            .When(d => d.DisplayText())
            .Do(_ => displayedString = buffer);
        display
            .When(d => d.ClearTextBuffer())
            .Do(_ => buffer = string.Empty);
        display
            .When(d => d.AddBufferText(Arg.Any<string>(), Arg.Any<Color>()))
            .Do(info => buffer +=
                Crayon.Output.Rgb(info.Arg<Color>().R, info.Arg<Color>().G, info.Arg<Color>().B)
                    .Text(info.Arg<string>()));
        var recipient = new DisplayRecipient(display, Color.Cyan, Color.Orange);
        var message = new Message("Amogus", "Aboba", new SeverityLevel(0));

        // Act
        recipient.ReceiveMessage(message);

        // Assert
        Assert.Equal(
            ColoredText("Header: Amogus\n", Color.Cyan) + ColoredText("Aboba", Color.Orange),
            displayedString);
    }

    private static string ColoredText(string text, Color color) =>
        Crayon.Output.Rgb(color.R, color.G, color.B).Text(text);
}