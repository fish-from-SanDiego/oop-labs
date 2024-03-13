using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

public abstract class StandardShip : Ship
{
    protected StandardShip(IEngine engine, IShipProtection protection)
        : base(engine, protection)
    {
    }

    public abstract override string ToString();
}