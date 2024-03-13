using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.BiosTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public class BiosBuilder : IBiosBuilder
{
    private string? _name;
    private BiosType? _type;
    private TechnologyVersion? _version;
    private HashSet<CentralProcessor>? _processors;

    public Bios Build() => new Bios(
        _name ?? throw new BuilderException(new InvalidOperationException(nameof(_name))),
        _type ?? throw new BuilderException(new InvalidOperationException(nameof(_type))),
        _version ?? throw new BuilderException(new InvalidOperationException(nameof(_version))),
        _processors ?? throw new BuilderException(new InvalidOperationException(nameof(_version))));

    public IBiosBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public IBiosBuilder WithBiosType(BiosType type)
    {
        _type = type;
        return this;
    }

    public IBiosBuilder WithVersion(TechnologyVersion version)
    {
        _version = version;
        return this;
    }

    public IBiosBuilder AddProcessor(CentralProcessor processor)
    {
        _processors ??= new HashSet<CentralProcessor>();
        _processors.Add(processor);
        return this;
    }

    public IBiosBuilder ClearProcessors()
    {
        _processors ??= new HashSet<CentralProcessor>();
        _processors.Clear();
        return this;
    }

    public IBiosBuilder WithProcessors(IEnumerable<CentralProcessor> processors)
    {
        _processors = processors.ToHashSet();
        return this;
    }
}