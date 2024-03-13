using Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public class CompulsoryProtectionWithClassOneDeflectors : CompulsoryProtectionWithDeflectors
{
    private const double InitialHitPoints = 30d;

    public CompulsoryProtectionWithClassOneDeflectors(ISuitableForSmallSizeDeflectors protection)
        : base(protection, InitialHitPoints, new SmallSizeDeflectorDisperser())
    {
    }
}