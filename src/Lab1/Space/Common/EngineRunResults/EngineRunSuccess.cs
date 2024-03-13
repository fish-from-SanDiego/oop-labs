using System;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.FuelTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.EngineRunResults;

public record EngineRunSuccess(FuelType Fuel, double FuelAmount, TimeSpan TravelTime) : EngineRunResult;