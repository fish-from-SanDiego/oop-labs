using System;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.EngineRunResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.FuelTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;

public class ClassEImpulseEngine : ImpulseEngine, ISuitableForRegularSpaceEngine,
    ISuitableForNitrinoParticleNebulaEngine
{
    private const double PlasmaConsumptionWhenStarting = 30d;
    private const double PlasmaConsumptionPerTravelUnit = 15d;
    private const double VelocityExponentBase = 25d;
    private const double VelocityCoefficient = 1.3d;
    private const double MaxUnitsPerHourVelocity = 150d;
    private const double StartingTimeInMinutes = 3.5d;

    public ClassEImpulseEngine()
        : base(new ActivePlasmaFuel())
    {
    }

    protected override EngineRunResult TryCrossSuitableSpaceSection(SpaceSection spaceSection)
    {
        SpaceSectionException.ThrowIfNull(spaceSection, nameof(spaceSection));
        return RunDistance(spaceSection.Distance);
    }

    protected override EngineStartInfo Start()
    {
        return new EngineStartInfo(
            PlasmaConsumptionWhenStarting,
            TimeSpan.FromMinutes(StartingTimeInMinutes));
    }

    private static double DistanceRunByTime(double timeInHours) => VelocityCoefficient /
        Math.Log(VelocityExponentBase) * (Math.Pow(VelocityExponentBase, timeInHours) - 1d);

    private static double TimeToAccelerateToMaxVelocity() =>
        Math.Log(MaxUnitsPerHourVelocity / VelocityCoefficient, VelocityExponentBase);

    private static double TimeToRunDistance(double distance) =>
        Math.Log((distance * Math.Log(VelocityExponentBase) / VelocityCoefficient) + 1d, VelocityExponentBase);

    private EngineRunSuccess RunDistance(double distance)
    {
        double engineRunTime = TimeToRunDistance(distance);
        double timeToAccelerate = TimeToAccelerateToMaxVelocity();
        if (engineRunTime > timeToAccelerate)
        {
            double distanceTillMaxVelocity = DistanceRunByTime(timeToAccelerate);
            engineRunTime = timeToAccelerate + ((distance - distanceTillMaxVelocity) / MaxUnitsPerHourVelocity);
        }

        double plasmaSpent = distance / PlasmaConsumptionPerTravelUnit;
        return new EngineRunSuccess(Fuel, plasmaSpent, TimeSpan.FromHours(engineRunTime));
    }
}