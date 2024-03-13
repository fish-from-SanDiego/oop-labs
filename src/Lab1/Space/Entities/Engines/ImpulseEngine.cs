using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.EngineRunResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.FuelTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;

public abstract class ImpulseEngine : IEngine
{
    protected ImpulseEngine(FuelType fuel)
    {
        Fuel = fuel;
    }

    public FuelType Fuel { get; }

    public EngineRunResult TryCrossSpaceSection(SpaceSection spaceSection)
    {
        SpaceSectionException.ThrowIfNull(spaceSection, nameof(spaceSection));
        if (spaceSection.Environment.SuitsEngine(this))
        {
            EngineStartInfo engineStartInfo = Start();
            EngineRunResult engineRunResult = TryCrossSuitableSpaceSection(spaceSection);
            if (engineRunResult is EngineRunSuccess success)
            {
                engineRunResult = new EngineRunSuccess(
                    success.Fuel,
                    success.FuelAmount + engineStartInfo.FuelAmount,
                    success.TravelTime + engineStartInfo.StartTime);
            }

            return engineRunResult;
        }

        return new EngineRunFailed();
    }

    protected abstract EngineRunResult TryCrossSuitableSpaceSection(SpaceSection spaceSection);
    protected abstract EngineStartInfo Start();
}