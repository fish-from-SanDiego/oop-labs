using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

public sealed class Vaklas : StandardShip
{
    public Vaklas()
        : base(
            new ImpulseWithAdditionalJumpEngine(
                new ClassEImpulseEngine(),
                new ClassGammaJumpEngine()),
            new CompulsoryProtectionWithClassOneDeflectors(new ClassTwoShipHull()))
    {
    }

    public override string ToString() => "Vaklas";
}