using System;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.EngineRunResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.FuelTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;

public class ClassCImpulseEngine : ImpulseEngine, ISuitableForRegularSpaceEngine
{
    private const double PlasmaConsumptionWhenStarting = 10d;
    private const double PlasmaConsumptionPerTravelUnit = 10d;
    private const double UnitsPerHourVelocity = 30d;
    private const double StartingTimeInMinutes = 3d;

    public ClassCImpulseEngine()
        : base(new ActivePlasmaFuel())
    {
    }

    protected override EngineRunResult TryCrossSuitableSpaceSection(SpaceSection spaceSection)
    {
        SpaceSectionException.ThrowIfNull(spaceSection, nameof(spaceSection));

        double plasmaSpent = spaceSection.Distance / PlasmaConsumptionPerTravelUnit;
        double travelTimeInHours = spaceSection.Distance / UnitsPerHourVelocity;
        return new EngineRunSuccess(Fuel, plasmaSpent, TimeSpan.FromHours(travelTimeInHours));
    }

    protected override EngineStartInfo Start()
    {
        return new EngineStartInfo(
            PlasmaConsumptionWhenStarting,
            TimeSpan.FromMinutes(StartingTimeInMinutes));
    }
}