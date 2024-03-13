using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.EngineRunResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.ShipRunResults;

public sealed record ShipRunSuccess(EngineRunSuccess EngineRunInfo) : ShipRunResult;