using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab1.Financial.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Financial.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.RouteSimulationResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.ShipRunResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

public class RouteSimulationService : IRouteSimulationService
{
    public RouteSimulationResult Run(Ship ship, Route route, MiningGuildFuelStockExchange stockExchange)
    {
        RouteSimulationException.ThrowIfNull(stockExchange, nameof(stockExchange));
        RouteSimulationException.ThrowIfNull(ship, nameof(ship));
        RouteSimulationException.ThrowIfNull(route, nameof(route));

        TimeSpan totalTravelTime = TimeSpan.Zero;
        var totalMoneySpent = new MiningGuildCredits(0);

        foreach (SpaceSection spaceSection in route.SpaceSections)
        {
            RouteSimulationException.ThrowIfNull(spaceSection);
            ShipRunResult shipRunResult = ship.TryCrossSpaceSection(spaceSection);

            if (shipRunResult is ShipDestroyed) return new RouteSimulationDestroyed();
            if (shipRunResult is ShipLostInSpace) return new RouteSimulationLostInSpace();
            if (shipRunResult is ShipCrewDead) return new RouteSimulationCrewDead();
            if (shipRunResult is not ShipRunSuccess shipRunSuccess)
                throw new RouteSimulationException("Unexpected ship run result");

            totalMoneySpent +=
                new MiningGuildCredits(
                    stockExchange.GetProductPrice(shipRunSuccess.EngineRunInfo.Fuel).Amount *
                    shipRunSuccess.EngineRunInfo.FuelAmount);

            totalTravelTime += shipRunSuccess.EngineRunInfo.TravelTime;
        }

        return new RouteSimulationSuccess(totalMoneySpent, totalTravelTime, ship);
    }

    public Ship? ChooseOptimalShip(
        IReadOnlyCollection<Ship> ships,
        Route route,
        MiningGuildFuelStockExchange stockExchange)
    {
        RouteSimulationException.ThrowIfNull(stockExchange, nameof(stockExchange));
        RouteSimulationException.ThrowIfNull(ships, nameof(ships));
        RouteSimulationException.ThrowIfNull(route, nameof(route));

        Ship? optimalShip = ships
            .Select(ship => Run(ship, route, stockExchange))
            .Select(result => result as RouteSimulationSuccess)
            .Where(result => result is not null)
            .OrderBy(result => result?.SpentMoney.Amount)
            .ThenBy(result => result?.TravelTime)
            .FirstOrDefault()
            ?.ShipAfterRoute;

        return optimalShip;
    }
}