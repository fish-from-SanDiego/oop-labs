using System.Drawing;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab3.Output.Entities;

public abstract class StringBuilderBasedDriver : IDisplayDriver
{
    private readonly StringBuilder _textBuilder;

    protected StringBuilderBasedDriver(Color defaultColour)
    {
        DefaultColour = defaultColour;
        _textBuilder = new StringBuilder();
    }

    protected StringBuilderBasedDriver()
        : this(Color.Empty)
    {
    }

    public Color DefaultColour { get; set; }

    public IDisplayDriver Clear()
    {
        _textBuilder.Clear();

        return this;
    }

    public IDisplayDriver AddText(string text)
    {
        return AddText(text, DefaultColour);
    }

    public IDisplayDriver AddText(string text, Color textColor)
    {
        if (textColor.Equals(Color.Empty))
        {
            _textBuilder.Append(text);
        }

        _textBuilder.Append(Crayon.Output.Rgb(textColor.R, textColor.G, textColor.B).Text(text));

        return this;
    }

    public void DisplayText()
    {
        DisplayString(_textBuilder.ToString());
    }

    public abstract void ClearOutput();

    protected abstract void DisplayString(string str);
}