using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.ProtectionResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public interface IShipProtection
{
    bool IsDestroyed { get; }
    bool HasAdditionalProtection { get; }
    double HitPoints { get; }
    ProtectionResult ProtectFromObstacle(IObstacleCluster obstacleCluster);
    ProtectionResult TakeDamageExcess(DamageExcess damageExcess);
}