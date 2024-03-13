using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities;

public class ParsingStrategy : IParsingStrategy
{
    private readonly string[] _commandTokens;
    private readonly int _mainArgumentsNumber;

    public ParsingStrategy(IReadOnlyCollection<string> commandTokens, int mainArgumentsNumber)
    {
        ParserException.ThrowIfNull(commandTokens, nameof(commandTokens));

        _mainArgumentsNumber = mainArgumentsNumber;
        _commandTokens = commandTokens.ToArray();
    }

    public ParseResult Execute(IReadOnlyCollection<string> tokens)
    {
        ParserException.ThrowIfNull(tokens, nameof(tokens));

        if (!_commandTokens.SequenceEqual(tokens.Take(_commandTokens.Length)))
            return new ParseWrongCommand();
        var mainArguments = tokens
            .Skip(_commandTokens.Length)
            .Take(_mainArgumentsNumber)
            .Select(arg => arg.Trim('\"'))
            .ToList();
        if ((tokens.Count - (_commandTokens.Length + mainArguments.Count)) % 2 != 0)
        {
            return new ParseError("Flags count error");
        }

        var flags = tokens
            .Skip(_commandTokens.Length + mainArguments.Count)
            .Select((token, index) => new { token, index })
            .GroupBy(x => x.index / 2)
            .Select(group => new CommandFlag(group.First().token, new[] { group.Last().token }))
            .ToList();

        return new ParseSuccess(mainArguments, flags);
    }
}