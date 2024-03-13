using System;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.ProtectionResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public abstract class CompulsoryProtectionWithDeflectors : ShipProtectionDecorator
{
    private readonly BasedOnSizeDisperser _deflectorsDisperser;

    protected CompulsoryProtectionWithDeflectors(
        ICompulsoryShipProtection protection,
        double deflectorsHitPoints,
        BasedOnSizeDisperser deflectorsDisperser)
        : base(protection)
    {
        ProtectionException.ThrowIfNull(deflectorsDisperser, nameof(deflectorsDisperser));
        if (deflectorsHitPoints <= 0)
        {
            throw new ProtectionException(
                "Invalid initial hit points",
                new ArgumentException("Value should be positive", nameof(deflectorsHitPoints)));
        }

        DeflectorsHitPoints = deflectorsHitPoints;
        _deflectorsDisperser = deflectorsDisperser;
    }

    public double DeflectorsHitPoints { get; private set; }

    public override bool HasAdditionalProtection => DeflectorsHitPoints > 0d;

    public override ProtectionResult ProtectFromObstacle(IObstacleCluster obstacleCluster)
    {
        ProtectionException.ThrowIfNull(obstacleCluster, nameof(obstacleCluster));
        if (obstacleCluster is not IDamagingObstacleCluster damagingObstacleCluster)
            return base.ProtectFromObstacle(obstacleCluster);
        return HandleProtectionResult(TakeDamage(
            damagingObstacleCluster.TotalDamage,
            damagingObstacleCluster.SingleObstacleDamage));
    }

    public override ProtectionResult TakeDamageExcess(DamageExcess damageExcess)
    {
        ProtectionException.ThrowIfNull(damageExcess, nameof(damageExcess));

        return HandleProtectionResult(TakeDamage(damageExcess.RemainingDamage, damageExcess.SingleObstacleDamage));
    }

    private ProtectionResult TakeDamage(double totalDamage, double oneTimeDamage)
    {
        double disperseCoefficient = _deflectorsDisperser.DisperseCoefficient(oneTimeDamage);
        DeflectorsHitPoints -= totalDamage * disperseCoefficient;
        if (DeflectorsHitPoints > 0d)
        {
            return new ProtectionSuccess();
        }

        double remainingDamage = -DeflectorsHitPoints / disperseCoefficient;
        DeflectorsHitPoints = 0d;
        return new ProtectionDestroyed(new DamageExcess(remainingDamage, oneTimeDamage));
    }

    private ProtectionResult HandleProtectionResult(ProtectionResult protectionResult)
    {
        if (protectionResult is ProtectionSuccess)
        {
            return protectionResult;
        }

        if (protectionResult is ProtectionDestroyed protectionDestroyed)
        {
            return base.TakeDamageExcess(protectionDestroyed.ExcessiveDamage);
        }

        throw new ProtectionException("Unexpected result when facing damaging obstacle");
    }
}