using Itmo.ObjectOrientedProgramming.Lab1.Space.Common;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;

public class ClassGammaJumpEngine : JumpEngine
{
    private const double GammaMaxDistance = 1000d;
    private const double GammaUnitsPerHourVelocity = 200d;

    private static readonly GravitonMatterConsumptionLaw FuelConsumptionLaw =
        (double distance) => (2.5 * distance * distance) + 20d;

    public ClassGammaJumpEngine()
        : base(GammaMaxDistance, FuelConsumptionLaw, GammaUnitsPerHourVelocity)
    {
    }
}