using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.ProtectionResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public class CompulsoryProtectionWithPhotonicDeflectors : ShipProtectionDecorator
{
    private const int InitialDurability = 3;

    public CompulsoryProtectionWithPhotonicDeflectors(CompulsoryProtectionWithDeflectors protection)
        : base(protection)
    {
        Durability = InitialDurability;
    }

    public int Durability { get; private set; }

    public override ProtectionResult ProtectFromObstacle(IObstacleCluster obstacleCluster)
    {
        if (obstacleCluster is not AntimatterFlashCluster antimatterFlashCluster)
            return base.ProtectFromObstacle(obstacleCluster);
        Durability -= antimatterFlashCluster.ObstaclesCount;
        return Durability >= 0
            ? new ProtectionSuccess()
            : base.ProtectFromObstacle(new AntimatterFlashCluster(-Durability));
    }
}