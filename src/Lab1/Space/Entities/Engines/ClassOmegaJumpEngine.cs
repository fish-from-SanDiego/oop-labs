using System;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;

public class ClassOmegaJumpEngine : JumpEngine
{
    private const double OmegaMaxDistance = 600d;
    private const double OmegaUnitsPerHourVelocity = 100d;

    private static readonly GravitonMatterConsumptionLaw FuelConsumptionLaw =
        (double distance) => (distance * Math.Log(distance)) + 10d;

    public ClassOmegaJumpEngine()
        : base(OmegaMaxDistance, FuelConsumptionLaw, OmegaUnitsPerHourVelocity)
    {
    }
}