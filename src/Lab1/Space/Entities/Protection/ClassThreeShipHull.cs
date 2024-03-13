using Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public class ClassThreeShipHull : ShipHull, ISuitableForSmallSizeDeflectors, ISuitableForMediumSizeDeflectors,
    ISuitableForLargeSizeDeflectors
{
    private const double InitialHitPoints = 155d;

    public ClassThreeShipHull()
        : base(InitialHitPoints, new LargeSizeHullDisperser())
    {
    }
}