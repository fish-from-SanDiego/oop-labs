using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities;

public class StringTokenizer : IStringTokenizer
{
    private readonly char _quoteSymbol;
    private readonly char[] _separators;

    public StringTokenizer(char quoteSymbol, IEnumerable<char> separators)
    {
        _quoteSymbol = quoteSymbol;
        _separators = separators.Distinct().ToArray();
    }

    public IReadOnlyCollection<string> TokenizeLine(string line)
    {
        if (line.Count(c => c.Equals(_quoteSymbol)) % 2 != 0)
        {
            throw new ParserException(
                "line not valid",
                new ArgumentException("line quotes number should not be dual", nameof(line)));
        }

        ParserException.ThrowIfNull(line, nameof(line));
        return line.Split(_quoteSymbol)
            .Select((phrase, index) => index % 2 == 0
                ? phrase.Split(_separators, StringSplitOptions.RemoveEmptyEntries)
                : new[] { $"\"{phrase}\"" })
            .SelectMany(word => word).ToList();
    }
}