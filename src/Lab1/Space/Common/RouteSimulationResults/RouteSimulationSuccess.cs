using System;
using Itmo.ObjectOrientedProgramming.Lab1.Financial.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.RouteSimulationResults;

public sealed record RouteSimulationSuccess
    (MiningGuildCredits SpentMoney, TimeSpan TravelTime, Ship ShipAfterRoute) : RouteSimulationResult;