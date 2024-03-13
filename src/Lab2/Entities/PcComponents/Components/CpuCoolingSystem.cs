using System.Collections.Generic;
using System.Collections.Immutable;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public class CpuCoolingSystem : IPcComponent<CpuCoolingSystem>, IPcComponent, ICpuCoolingSystemBuilderDirector
{
    internal CpuCoolingSystem(
        string name,
        TwoDimensions size,
        IEnumerable<CpuSocket> supportedCpuSockets,
        Power maxHeatDissipation)
    {
        Name = name;
        Size = size;
        SupportedCpuSockets = supportedCpuSockets.ToImmutableHashSet();
        MaxHeatDissipation = maxHeatDissipation;
    }

    public string Name { get; }
    public TwoDimensions Size { get; }
    public IReadOnlySet<CpuSocket> SupportedCpuSockets { get; }
    public Power MaxHeatDissipation { get; }

    public virtual CpuCoolingSystem Clone() =>
        new CpuCoolingSystem(Name, Size, SupportedCpuSockets, MaxHeatDissipation);

    public ICpuCoolingSystemBuilder Direct(ICpuCoolingSystemBuilder builder)
    {
        BuilderException.ThrowIfNull(builder);

        return builder.WithName(Name).WithSize(Size).WithSupportedSockets(SupportedCpuSockets)
            .WithMaxHeatDissipation(MaxHeatDissipation);
    }
}