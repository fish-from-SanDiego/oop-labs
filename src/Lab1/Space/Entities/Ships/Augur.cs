using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

public class Augur : StandardShip
{
    public Augur()
        : base(
            new ImpulseWithAdditionalJumpEngine(
                new ClassEImpulseEngine(),
                new ClassAlphaJumpEngine()),
            new CompulsoryProtectionWithClassThreeDeflectors(new ClassThreeShipHull()))
    {
    }

    public override string ToString() => "Augur";
}