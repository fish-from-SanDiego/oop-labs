using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public class CpuCoolingSystemBuilder : ICpuCoolingSystemBuilder
{
    private string? _name;
    private TwoDimensions? _size;
    private HashSet<CpuSocket>? _sockets;
    private Power? _heatDissipation;

    public CpuCoolingSystem Build()
    {
        if (_sockets is null || _sockets.Count == 0)
            throw new BuilderException(new InvalidOperationException(nameof(_sockets)));
        return new CpuCoolingSystem(
            _name ?? throw new BuilderException(new InvalidOperationException(nameof(_name))),
            _size ?? throw new BuilderException(new InvalidOperationException(nameof(_size))),
            _sockets,
            _heatDissipation ?? throw new BuilderException(new InvalidOperationException(nameof(_heatDissipation))));
    }

    public ICpuCoolingSystemBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ICpuCoolingSystemBuilder WithSize(TwoDimensions size)
    {
        _size = size;
        return this;
    }

    public ICpuCoolingSystemBuilder WithSupportedSockets(IEnumerable<CpuSocket> sockets)
    {
        _sockets = sockets.ToHashSet();
        return this;
    }

    public ICpuCoolingSystemBuilder AddSupportedSocket(CpuSocket socket)
    {
        _sockets ??= new HashSet<CpuSocket>();
        _sockets.Add(socket);
        return this;
    }

    public ICpuCoolingSystemBuilder ClearSupportedSockets()
    {
        _sockets ??= new HashSet<CpuSocket>();
        _sockets.Clear();
        return this;
    }

    public ICpuCoolingSystemBuilder WithMaxHeatDissipation(Power heatDissipation)
    {
        _heatDissipation = heatDissipation;
        return this;
    }
}