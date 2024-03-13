using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.ProtectionResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public class ProtectionWithAntiNitrinoEmitter : ShipProtectionDecorator
{
    public ProtectionWithAntiNitrinoEmitter(IShipProtection protection)
        : base(protection)
    {
    }

    public override ProtectionResult ProtectFromObstacle(IObstacleCluster obstacleCluster)
    {
        return obstacleCluster is SpaceWhaleCluster
            ? new ProtectionSuccess()
            : base.ProtectFromObstacle(obstacleCluster);
    }
}