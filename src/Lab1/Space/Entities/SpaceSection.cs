using System;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Entities;

public class SpaceSection
{
    public SpaceSection(double distance, ISpaceEnvironment environment)
    {
        SpaceEnvironmentException.ThrowIfNull(environment);
        if (distance < 0d)
        {
            throw new SpaceSectionException(
                "Value validation failed",
                new ArgumentException("Value should be positive", nameof(distance)));
        }

        Environment = environment;
        Distance = distance;
    }

    public double Distance { get; }
    public ISpaceEnvironment Environment { get; }
}