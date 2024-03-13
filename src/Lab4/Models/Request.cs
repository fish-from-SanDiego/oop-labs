using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public record Request(IReadOnlyCollection<string> Values);