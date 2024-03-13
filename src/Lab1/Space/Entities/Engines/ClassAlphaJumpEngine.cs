using Itmo.ObjectOrientedProgramming.Lab1.Space.Common;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;

public class ClassAlphaJumpEngine : JumpEngine
{
    private const double AlphaMaxDistance = 200d;
    private const double AlphaUnitsPerHourVelocity = 50d;
    private static readonly GravitonMatterConsumptionLaw FuelConsumptionLaw = (double distance) => distance * 1.5d;

    public ClassAlphaJumpEngine()
        : base(AlphaMaxDistance, FuelConsumptionLaw, AlphaUnitsPerHourVelocity)
    {
    }
}