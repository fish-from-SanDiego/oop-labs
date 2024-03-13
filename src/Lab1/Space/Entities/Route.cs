using System.Collections.Generic;
using System.Collections.Immutable;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities;

public class Route
{
    public Route(IEnumerable<SpaceSection> spaceSections)
    {
        SpaceSections = spaceSections.ToImmutableArray();
    }

    public Route(params SpaceSection[] spaceSections)
        : this(spaceSections as IEnumerable<SpaceSection>)
    {
    }

    public IReadOnlyCollection<SpaceSection> SpaceSections { get; }
}