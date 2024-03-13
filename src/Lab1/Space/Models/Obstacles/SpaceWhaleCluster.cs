using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;

public class SpaceWhaleCluster : IDamagingObstacleCluster, INitrinoParticleNebulaObstacleCluster
{
    public SpaceWhaleCluster(int obstaclesCount)
    {
        ObstacleException.ThrowIfNegative(obstaclesCount, nameof(obstaclesCount));
        ObstaclesCount = obstaclesCount;
    }

    public double SingleObstacleDamage => 240d;
    public double TotalDamage => SingleObstacleDamage * ObstaclesCount;
    public int ObstaclesCount { get; }
}