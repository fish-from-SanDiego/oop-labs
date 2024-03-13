using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

public class MeridianWithPhotonicDeflectors : StandardShip
{
    public MeridianWithPhotonicDeflectors()
        : base(
            new ClassEImpulseEngine(),
            new ProtectionWithAntiNitrinoEmitter(
                new CompulsoryProtectionWithPhotonicDeflectors(
                    new CompulsoryProtectionWithClassTwoDeflectors(new ClassTwoShipHull()))))
    {
    }

    public override string ToString() => "Photonic deflectors modified Meridian";
}