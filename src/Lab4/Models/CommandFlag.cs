using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public record CommandFlag(string Name, IReadOnlyCollection<string> Value);