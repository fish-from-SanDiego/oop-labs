using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Output.Entities;

public interface IDisplay
{
    Color DefaultColor { get; set; }
    IDisplay ClearTextBuffer();
    IDisplay AddBufferText(string text);
    IDisplay AddBufferText(string text, Color textColor);
    void DisplayText();
}