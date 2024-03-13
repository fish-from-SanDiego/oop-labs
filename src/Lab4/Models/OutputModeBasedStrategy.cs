namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public record OutputModeBasedStrategy<TStrategy>(OutputMode OutputMode, TStrategy Strategy);