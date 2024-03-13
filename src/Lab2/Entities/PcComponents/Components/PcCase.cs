using System.Collections.Generic;
using System.Collections.Immutable;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;
using Itmo.ObjectOrientedProgramming.Lab2.Models.FormFactors;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public class PcCase : IPcComponent<PcCase>, IPcComponent, IPcCaseBuilderDirector
{
    internal PcCase(
        string name,
        TwoDimensions graphicsCardMaxSize,
        OneDimension coolingSystemMaxSize,
        IEnumerable<MotherBoardFormFactor> supportedMotherBoardFormFactors,
        ThreeDimensions size)
    {
        Name = name;
        GraphicsCardMaxSize = graphicsCardMaxSize;
        CoolingSystemMaxSize = coolingSystemMaxSize;
        SupportedMotherBoardFormFactors = supportedMotherBoardFormFactors.ToImmutableHashSet();
        Size = size;
    }

    public string Name { get; }
    public TwoDimensions GraphicsCardMaxSize { get; }
    public OneDimension CoolingSystemMaxSize { get; }
    public IReadOnlySet<MotherBoardFormFactor> SupportedMotherBoardFormFactors { get; }
    public ThreeDimensions Size { get; }

    public virtual PcCase Clone() =>
        new PcCase(Name, GraphicsCardMaxSize, CoolingSystemMaxSize, SupportedMotherBoardFormFactors, Size);

    public IPcCaseBuilder Direct(IPcCaseBuilder builder)
    {
        BuilderException.ThrowIfNull(builder);

        return builder.WithName(Name).WithGraphicsCardMaxSize(GraphicsCardMaxSize)
            .WithCoolingSystemMaxSize(CoolingSystemMaxSize).WithMotherBoardFormFactors(SupportedMotherBoardFormFactors)
            .WithSize(Size);
    }
}