using System.Collections.Generic;
using System.Collections.Immutable;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Environments;

public class RegularSpace : ISpaceEnvironment
{
    public RegularSpace(IEnumerable<IRegularSpaceObstacleCluster> obstacles)
    {
        SpaceEnvironmentException.ThrowIfNull(obstacles, nameof(obstacles));
        Obstacles = obstacles.ToImmutableArray();
    }

    public RegularSpace(params IRegularSpaceObstacleCluster[] obstacles)
        : this(obstacles as IEnumerable<IRegularSpaceObstacleCluster>)
    {
    }

    public IReadOnlyCollection<IObstacleCluster> Obstacles { get; }

    public bool SuitsEngine(IEngine engine)
    {
        return engine is ISuitableForRegularSpaceEngine;
    }
}