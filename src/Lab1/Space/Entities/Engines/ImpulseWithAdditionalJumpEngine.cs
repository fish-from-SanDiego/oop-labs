using System;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.EngineRunResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Engines;

public class ImpulseWithAdditionalJumpEngine : EngineDecorator
{
    private readonly JumpEngine _additionalEngine;

    public ImpulseWithAdditionalJumpEngine(ImpulseEngine mainEngine, JumpEngine additionalEngine)
        : base(mainEngine)
    {
        _additionalEngine = additionalEngine ?? throw new EngineException(
            "Wrapped engine should be not null",
            new ArgumentNullException(nameof(additionalEngine)));
    }

    public override EngineRunResult TryCrossSpaceSection(SpaceSection spaceSection)
    {
        if (base.TryCrossSpaceSection(spaceSection) is EngineRunSuccess engineRunSuccess)
            return engineRunSuccess;
        return _additionalEngine.TryCrossSpaceSection(spaceSection);
    }
}