using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.EngineRunResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.ProtectionResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.ShipRunResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Protection;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

public class Ship
{
    private readonly IEngine _engine;
    private readonly IShipProtection _protection;

    public Ship(IEngine engine, IShipProtection protection)
    {
        ShipException.ThrowIfNull(engine, nameof(engine));
        ShipException.ThrowIfNull(protection, nameof(protection));
        _engine = engine;
        _protection = protection;
    }

    public bool HasAdditionalProtection => _protection.HasAdditionalProtection;

    public bool IsDestroyed => _protection.IsDestroyed;

    public ShipRunResult TryCrossSpaceSection(SpaceSection spaceSection)
    {
        ShipException.ThrowIfNull(spaceSection, nameof(spaceSection));
        EngineRunResult engineRunResult = _engine.TryCrossSpaceSection(spaceSection);
        if (engineRunResult is not EngineRunSuccess engineRunSuccess)
        {
            return new ShipLostInSpace();
        }

        foreach (IObstacleCluster environmentObstacle in spaceSection.Environment.Obstacles)
        {
            ProtectionResult protectionResult = _protection.ProtectFromObstacle(environmentObstacle);
            if (protectionResult is ProtectionUseless) return new ShipCrewDead();
            if (protectionResult is ProtectionDestroyed) return new ShipDestroyed();
            if (protectionResult is not ProtectionSuccess protectionSuccess)
            {
                throw new ShipException("Unexpected result when facing an obstacle");
            }
        }

        return new ShipRunSuccess(engineRunSuccess);
    }
}