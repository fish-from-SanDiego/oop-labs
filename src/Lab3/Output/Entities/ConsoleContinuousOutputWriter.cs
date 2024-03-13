using System;

namespace Itmo.ObjectOrientedProgramming.Lab3.Output.Entities;

public class ConsoleContinuousOutputWriter : IContinuousOutputWriter
{
    public void Write(string text)
    {
        Console.WriteLine(text);
    }
}