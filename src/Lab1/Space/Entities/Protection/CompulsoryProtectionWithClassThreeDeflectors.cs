using Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public class CompulsoryProtectionWithClassThreeDeflectors : CompulsoryProtectionWithDeflectors
{
    private const double InitialHitPoints = 240d;

    public CompulsoryProtectionWithClassThreeDeflectors(ISuitableForLargeSizeDeflectors protection)
        : base(protection, InitialHitPoints, new LargeSizeDeflectorDisperser())
    {
    }
}