using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities;

public interface IParsingStrategy
{
    ParseResult Execute(IReadOnlyCollection<string> tokens);
}