using Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public class ClassTwoShipHull : ShipHull, ISuitableForSmallSizeDeflectors, ISuitableForMediumSizeDeflectors
{
    private const double InitialHitPoints = 65d;

    public ClassTwoShipHull()
        : base(InitialHitPoints, new MediumSizeHullDisperser())
    {
    }
}