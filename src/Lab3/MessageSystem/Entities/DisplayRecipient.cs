using System.Drawing;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Common;
using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;
using Itmo.ObjectOrientedProgramming.Lab3.Output.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Entities;

public class DisplayRecipient : ITerminalMessageRecipient
{
    private readonly IDisplay _display;

    public DisplayRecipient(IDisplay display, Color headerColor, Color bodyColor)
    {
        MessageSystemException.ThrowIfNull(display, nameof(display));

        HeaderColor = headerColor;
        BodyColor = bodyColor;
        _display = display;
    }

    public DisplayRecipient(IDisplay display)
        : this(display, Color.Empty, Color.Empty)
    {
    }

    public Color HeaderColor { get; }
    public Color BodyColor { get; }

    public MessageReceptionResult ReceiveMessage(Message message)
    {
        MessageSystemException.ThrowIfNull(message, nameof(message));

        _display.ClearTextBuffer();
        _display.AddBufferText($"Header: {message.Header}\n", HeaderColor);
        _display.AddBufferText($"{message.Body}", BodyColor);
        _display.DisplayText();

        return new MessageReceptionSuccess();
    }
}