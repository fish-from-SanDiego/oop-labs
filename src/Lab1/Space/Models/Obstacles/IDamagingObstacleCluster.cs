namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;

public interface IDamagingObstacleCluster : IObstacleCluster
{
    double SingleObstacleDamage { get; }
    double TotalDamage { get; }
}