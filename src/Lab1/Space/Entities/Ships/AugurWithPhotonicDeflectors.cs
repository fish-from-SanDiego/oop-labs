using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

public class AugurWithPhotonicDeflectors : StandardShip
{
    public AugurWithPhotonicDeflectors()
        : base(
            new ImpulseWithAdditionalJumpEngine(
                new ClassEImpulseEngine(),
                new ClassAlphaJumpEngine()),
            new CompulsoryProtectionWithPhotonicDeflectors(
                new CompulsoryProtectionWithClassThreeDeflectors(new ClassThreeShipHull())))
    {
    }

    public override string ToString() => "Photonic deflectors modified Augur";
}