using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

public sealed class Meridian : StandardShip
{
    public Meridian()
        : base(
            new ClassEImpulseEngine(),
            new ProtectionWithAntiNitrinoEmitter(
                new CompulsoryProtectionWithClassTwoDeflectors(new ClassTwoShipHull())))
    {
    }

    public override string ToString() => "Meridian";
}