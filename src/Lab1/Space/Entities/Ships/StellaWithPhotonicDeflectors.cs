using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

public class StellaWithPhotonicDeflectors : StandardShip
{
    public StellaWithPhotonicDeflectors()
        : base(
            new ImpulseWithAdditionalJumpEngine(
                new ClassCImpulseEngine(),
                new ClassOmegaJumpEngine()),
            new CompulsoryProtectionWithPhotonicDeflectors(
                new CompulsoryProtectionWithClassOneDeflectors(new ClassOneShipHull())))
    {
    }

    public override string ToString() => "Photonic deflectors modified Stella";
}