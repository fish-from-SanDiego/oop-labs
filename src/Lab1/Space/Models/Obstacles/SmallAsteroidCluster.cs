using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;

public class SmallAsteroidCluster : IRegularSpaceObstacleCluster, IDamagingObstacleCluster
{
    public SmallAsteroidCluster(int obstaclesCount)
    {
        ObstacleException.ThrowIfNegative(obstaclesCount, nameof(obstaclesCount));
        ObstaclesCount = obstaclesCount;
    }

    public double SingleObstacleDamage => 15d;
    public double TotalDamage => SingleObstacleDamage * ObstaclesCount;
    public int ObstaclesCount { get; }
}