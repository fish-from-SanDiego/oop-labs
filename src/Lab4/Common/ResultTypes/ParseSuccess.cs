using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;

public record ParseSuccess(IReadOnlyCollection<string> MainArguments, IReadOnlyCollection<CommandFlag> Flags)
    : ParseResult;