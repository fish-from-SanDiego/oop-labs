using System.Drawing;
using Itmo.ObjectOrientedProgramming.Lab3.Output.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Output.Entities;

public class DriverManagingDisplay : IDisplay
{
    private readonly IDisplayDriver _driver;

    public DriverManagingDisplay(IDisplayDriver driver)
    {
        OutputException.ThrowIfNull(driver, nameof(driver));

        _driver = driver;
    }

    public Color DefaultColor
    {
        get => _driver.DefaultColour;
        set => _driver.DefaultColour = value;
    }

    public IDisplay ClearTextBuffer()
    {
        _driver.Clear();

        return this;
    }

    public IDisplay AddBufferText(string text)
    {
        _driver.AddText(text);

        return this;
    }

    public IDisplay AddBufferText(string text, Color textColor)
    {
        _driver.AddText(text, textColor);

        return this;
    }

    public void DisplayText()
    {
        _driver.ClearOutput();
        _driver.DisplayText();
    }
}