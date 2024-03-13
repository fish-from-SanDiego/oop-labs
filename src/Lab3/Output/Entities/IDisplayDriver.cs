using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Output.Entities;

public interface IDisplayDriver
{
    Color DefaultColour { get; set; }
    IDisplayDriver Clear();
    IDisplayDriver AddText(string text);
    IDisplayDriver AddText(string text, Color textColor);
    void DisplayText();
    void ClearOutput();
}