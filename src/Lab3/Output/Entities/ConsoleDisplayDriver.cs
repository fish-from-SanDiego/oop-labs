using System;
using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Output.Entities;

public class ConsoleDisplayDriver : StringBuilderBasedDriver
{
    public ConsoleDisplayDriver(Color defaultColour)
        : base(defaultColour)
    {
    }

    public ConsoleDisplayDriver()
        : this(Color.Empty)
    {
    }

    public override void ClearOutput()
    {
        Console.Clear();
    }

    protected override void DisplayString(string str)
    {
        Console.WriteLine(str);
    }
}