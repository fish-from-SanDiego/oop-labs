using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Environments;

public interface ISpaceEnvironment
{
    IReadOnlyCollection<IObstacleCluster> Obstacles { get; }
    bool SuitsEngine(IEngine engine);
}