using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;

public class ConsoleTextWriter : ITextWriter
{
    public void Output(string? text)
    {
        Console.WriteLine(text);
        Console.WriteLine();
    }
}