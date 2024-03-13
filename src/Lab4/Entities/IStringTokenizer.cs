using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities;

public interface IStringTokenizer
{
    IReadOnlyCollection<string> TokenizeLine(string line);
}