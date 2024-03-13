using System;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.EngineRunResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;

public abstract class EngineDecorator : IEngine
{
    private readonly IEngine _engine;

    protected EngineDecorator(IEngine engine)
    {
        _engine = engine ?? throw new EngineException(
            "Wrapped engine should be not null",
            new ArgumentNullException(nameof(engine)));
    }

    public virtual EngineRunResult TryCrossSpaceSection(SpaceSection spaceSection)
    {
        return _engine.TryCrossSpaceSection(spaceSection);
    }
}