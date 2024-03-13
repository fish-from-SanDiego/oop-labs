using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.ProtectionResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public abstract class ShipProtectionDecorator : IShipProtection
{
    private readonly IShipProtection _protection;

    protected ShipProtectionDecorator(IShipProtection protection)
    {
        ProtectionException.ThrowIfNull(protection, nameof(protection));
        _protection = protection;
    }

    public virtual bool IsDestroyed => _protection.IsDestroyed;
    public virtual bool HasAdditionalProtection => _protection.HasAdditionalProtection;
    public virtual double HitPoints => _protection.HitPoints;

    public virtual ProtectionResult ProtectFromObstacle(IObstacleCluster obstacleCluster)
    {
        return _protection.ProtectFromObstacle(obstacleCluster);
    }

    public virtual ProtectionResult TakeDamageExcess(DamageExcess damageExcess)
    {
        return _protection.TakeDamageExcess(damageExcess);
    }
}