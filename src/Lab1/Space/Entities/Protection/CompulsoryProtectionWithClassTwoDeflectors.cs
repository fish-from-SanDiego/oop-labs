using Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public class CompulsoryProtectionWithClassTwoDeflectors : CompulsoryProtectionWithDeflectors
{
    private const double InitialHitPoints = 90d;

    public CompulsoryProtectionWithClassTwoDeflectors(ISuitableForMediumSizeDeflectors protection)
        : base(protection, InitialHitPoints, new MediumSizeDeflectorDisperser())
    {
    }
}