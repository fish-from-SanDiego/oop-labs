using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

public class VaklasWithPhotonicDeflectors : StandardShip
{
    public VaklasWithPhotonicDeflectors()
        : base(
            new ImpulseWithAdditionalJumpEngine(
                new ClassEImpulseEngine(),
                new ClassGammaJumpEngine()),
            new CompulsoryProtectionWithPhotonicDeflectors(
                new CompulsoryProtectionWithClassOneDeflectors(new ClassTwoShipHull())))
    {
    }

    public override string ToString() => "Photonic deflectors modified Vaklas";
}