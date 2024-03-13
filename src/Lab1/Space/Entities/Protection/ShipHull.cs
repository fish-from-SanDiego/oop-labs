using System;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.ProtectionResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public abstract class ShipHull : ICompulsoryShipProtection
{
    private readonly BasedOnSizeDisperser _disperser;

    protected ShipHull(double hitPoints, BasedOnSizeDisperser disperser)
    {
        if (hitPoints <= 0)
        {
            throw new ProtectionException(
                "Invalid initial hit points",
                new ArgumentException("Value should be positive", nameof(hitPoints)));
        }

        ProtectionException.ThrowIfNull(disperser, nameof(disperser));
        HitPoints = hitPoints;
        _disperser = disperser;
    }

    public bool IsDestroyed => HitPoints <= 0d;
    public bool HasAdditionalProtection => false;
    public double HitPoints { get; private set; }

    public ProtectionResult ProtectFromObstacle(IObstacleCluster obstacleCluster)
    {
        ProtectionException.ThrowIfNull(obstacleCluster, nameof(obstacleCluster));
        if (obstacleCluster is not IDamagingObstacleCluster damagingObstacleCluster) return new ProtectionUseless();

        return TakeDamage(damagingObstacleCluster.TotalDamage, damagingObstacleCluster.SingleObstacleDamage);
    }

    public ProtectionResult TakeDamageExcess(DamageExcess damageExcess)
    {
        ProtectionException.ThrowIfNull(damageExcess, nameof(damageExcess));

        return TakeDamage(damageExcess.RemainingDamage, damageExcess.SingleObstacleDamage);
    }

    private ProtectionResult TakeDamage(double totalDamage, double oneTimeDamage)
    {
        double disperseCoefficient = _disperser.DisperseCoefficient(oneTimeDamage);
        HitPoints -= totalDamage * disperseCoefficient;
        if (HitPoints > 0d)
        {
            return new ProtectionSuccess();
        }

        double remainingDamage = -HitPoints / disperseCoefficient;
        HitPoints = 0d;
        return new ProtectionDestroyed(new DamageExcess(remainingDamage, oneTimeDamage));
    }
}