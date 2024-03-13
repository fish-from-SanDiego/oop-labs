using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

public sealed class ExcursionShuttle : Ship
{
    public ExcursionShuttle()
        : base(new ClassCImpulseEngine(), new ClassOneShipHull())
    {
    }
}