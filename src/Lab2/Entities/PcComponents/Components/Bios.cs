using System.Collections.Generic;
using System.Collections.Immutable;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.BiosTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public class Bios : IPcComponent<Bios>, IPcComponent, IBiosBuilderDirector
{
    internal Bios(
        string name,
        BiosType type,
        TechnologyVersion version,
        IEnumerable<CentralProcessor> supportedProcessors)
    {
        Name = name;
        Type = type;
        Version = version;
        SupportedProcessors = supportedProcessors.ToImmutableHashSet();
    }

    public string Name { get; }
    public BiosType Type { get; }
    public TechnologyVersion Version { get; }
    public IReadOnlySet<CentralProcessor> SupportedProcessors { get; }

    public virtual Bios Clone() => new Bios(Name, Type, Version, SupportedProcessors);

    public IBiosBuilder Direct(IBiosBuilder builder)
    {
        BuilderException.ThrowIfNull(builder);

        return builder.WithName(Name).WithBiosType(Type).WithVersion(Version).WithProcessors(SupportedProcessors);
    }
}