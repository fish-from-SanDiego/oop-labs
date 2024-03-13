using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;

public class AntimatterFlashCluster : IHighDensityNebulaObstacleCluster, IIgnoringRegularProtectionObstacleCluster
{
    public AntimatterFlashCluster(int obstaclesCount)
    {
        ObstacleException.ThrowIfNegative(obstaclesCount, nameof(obstaclesCount));
        ObstaclesCount = obstaclesCount;
    }

    public int ObstaclesCount { get; }
}