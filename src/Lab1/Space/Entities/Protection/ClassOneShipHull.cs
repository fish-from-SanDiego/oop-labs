using Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

public class ClassOneShipHull : ShipHull, ISuitableForSmallSizeDeflectors
{
    private const double InitialHitPoints = 20d;

    public ClassOneShipHull()
        : base(InitialHitPoints, new SmallSizeHullDisperser())
    {
    }
}