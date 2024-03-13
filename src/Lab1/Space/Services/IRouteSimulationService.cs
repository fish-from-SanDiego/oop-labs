using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Financial.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.RouteSimulationResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

public interface IRouteSimulationService
{
    RouteSimulationResult Run(Ship ship, Route route, MiningGuildFuelStockExchange stockExchange);
    Ship? ChooseOptimalShip(IReadOnlyCollection<Ship> ships, Route route, MiningGuildFuelStockExchange stockExchange);
}