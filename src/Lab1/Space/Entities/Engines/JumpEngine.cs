using System;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.EngineRunResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.FuelTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;

public class JumpEngine : ISuitableForHighDensityNebulaEngine
{
    private readonly GravitonMatterConsumptionLaw _fuelConsumptionLaw;
    private readonly double _unitsPerHourVelocity;

    public JumpEngine(double maxDistance, GravitonMatterConsumptionLaw fuelConsumptionLaw, double unitsPerHourVelocity)
    {
        EngineException.ThrowIfNonPositive(maxDistance, nameof(maxDistance));
        EngineException.ThrowIfNonPositive(unitsPerHourVelocity, nameof(maxDistance));
        Fuel = new GravitonMatterFuel();
        MaxDistance = maxDistance;
        _fuelConsumptionLaw = fuelConsumptionLaw;
        _unitsPerHourVelocity = unitsPerHourVelocity;
    }

    public FuelType Fuel { get; }
    public double MaxDistance { get; }

    public EngineRunResult TryCrossSpaceSection(SpaceSection spaceSection)
    {
        SpaceSectionException.ThrowIfNull(spaceSection, nameof(spaceSection));
        if (!spaceSection.Environment.SuitsEngine(this)) return new EngineRunFailed();
        return TryCrossSuitableSpaceSection(spaceSection);
    }

    private EngineRunResult TryCrossSuitableSpaceSection(SpaceSection spaceSection)
    {
        SpaceSectionException.ThrowIfNull(spaceSection, nameof(spaceSection));
        if (spaceSection.Distance > MaxDistance)
        {
            return new EngineRunFailed();
        }

        double travelTimeInHours = spaceSection.Distance / _unitsPerHourVelocity;
        double gravitonMatterSpent = _fuelConsumptionLaw(spaceSection.Distance);
        EngineException.ThrowIfNonPositive(gravitonMatterSpent, nameof(gravitonMatterSpent));

        return new EngineRunSuccess(Fuel, gravitonMatterSpent, TimeSpan.FromHours(travelTimeInHours));
    }
}