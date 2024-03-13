namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public record SystemTypeBasedStrategy<TStrategy>(FileSystemType SystemType, TStrategy Strategy);