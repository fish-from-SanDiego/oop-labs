using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

public class Stella : StandardShip
{
    public Stella()
        : base(
            new ImpulseWithAdditionalJumpEngine(
                new ClassCImpulseEngine(),
                new ClassOmegaJumpEngine()),
            new CompulsoryProtectionWithClassOneDeflectors(new ClassOneShipHull()))
    {
    }

    public override string ToString() => "Stella";
}