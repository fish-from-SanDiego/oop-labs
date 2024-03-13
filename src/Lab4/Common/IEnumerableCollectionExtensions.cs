using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Common;

public static class IEnumerableCollectionExtensions
{
    public static IReadOnlyDictionary<FileSystemType, TStrategy> ToStrategyDictionary<TStrategy>(
        this IEnumerable<SystemTypeBasedStrategy<TStrategy>> strategies)
        => strategies
            .GroupBy(d => d.SystemType)
            .ToDictionary(g => g.Key, g => g.First().Strategy);

    public static IReadOnlyDictionary<OutputMode, TStrategy> ToStrategyDictionary<TStrategy>(
        this IEnumerable<OutputModeBasedStrategy<TStrategy>> strategies)
        => strategies
            .GroupBy(d => d.OutputMode)
            .ToDictionary(g => g.Key, g => g.First().Strategy);
}