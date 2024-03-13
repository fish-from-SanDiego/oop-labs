using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.EngineRunResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;

public interface IEngine
{
    EngineRunResult TryCrossSpaceSection(SpaceSection spaceSection);
}