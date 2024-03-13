using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Ddr;
using Itmo.ObjectOrientedProgramming.Lab2.Models.FormFactors;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public class MotherBoardBuilder : IMotherBoardBuilder
{
    private string? _name;
    private CpuSocket? _socket;
    private Dictionary<PciEConnection, Slots>? _pciEInfo;
    private Slots? _sataPorts;
    private HashSet<Frequency>? _jedecFrequencies;
    private HashSet<Frequency>? _overclockedFrequencies;
    private DdrStandard? _ddrStandard;
    private Slots? _ramSlots;
    private MotherBoardFormFactor? _formFactor;
    private RandomAccessMemoryFormFactor? _supportedMemoryFormFactor;
    private Bios? _bios;
    private WiFiAdapter? _wiFiAdapter;

    public MotherBoardBuilder()
    {
        _overclockedFrequencies = new HashSet<Frequency>();
    }

    public MotherBoard Build()
    {
        if (_pciEInfo is null || _pciEInfo.Count == 0)
            throw new BuilderException(new InvalidOperationException(nameof(_pciEInfo)));
        if (_jedecFrequencies is null || _jedecFrequencies.Count == 0)
            throw new BuilderException(new InvalidOperationException(nameof(_jedecFrequencies)));
        if (_overclockedFrequencies is null)
            throw new BuilderException(new InvalidOperationException(nameof(_overclockedFrequencies)));
        if (_supportedMemoryFormFactor is null)
            throw new BuilderException(new InvalidOperationException(nameof(_supportedMemoryFormFactor)));
        return new MotherBoard(
            _name ?? throw new BuilderException(new InvalidOperationException(nameof(_name))),
            _socket ?? throw new BuilderException(new InvalidOperationException(nameof(_socket))),
            _pciEInfo,
            _sataPorts ?? throw new BuilderException(new InvalidOperationException(nameof(_sataPorts))),
            _jedecFrequencies,
            _overclockedFrequencies,
            _ddrStandard ?? throw new BuilderException(new InvalidOperationException(nameof(_ddrStandard))),
            _ramSlots ?? throw new BuilderException(new InvalidOperationException(nameof(_ramSlots))),
            _formFactor ?? throw new BuilderException(new InvalidOperationException(nameof(_formFactor))),
            _supportedMemoryFormFactor,
            _bios ?? throw new BuilderException(new InvalidOperationException(nameof(_bios))),
            _wiFiAdapter);
    }

    public IMotherBoardBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public IMotherBoardBuilder WithSocket(CpuSocket socket)
    {
        _socket = socket;
        return this;
    }

    public IMotherBoardBuilder WithPciEInfo(IReadOnlyDictionary<PciEConnection, Slots> info)
    {
        _pciEInfo = info.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        return this;
    }

    public IMotherBoardBuilder SetPciEInfo(PciEConnection connection, Slots slots)
    {
        _pciEInfo ??= new Dictionary<PciEConnection, Slots>();
        _pciEInfo[connection] = slots;
        return this;
    }

    public IMotherBoardBuilder ClearPciEInfo()
    {
        _pciEInfo ??= new Dictionary<PciEConnection, Slots>();
        _pciEInfo.Clear();
        return this;
    }

    public IMotherBoardBuilder WithSataPorts(Slots sataPorts)
    {
        _sataPorts = sataPorts;
        return this;
    }

    public IMotherBoardBuilder WithJedecFrequencies(IEnumerable<Frequency> frequencies)
    {
        _jedecFrequencies = frequencies.ToHashSet();
        return this;
    }

    public IMotherBoardBuilder AddJedecFrequency(Frequency frequency)
    {
        _jedecFrequencies ??= new HashSet<Frequency>();
        _jedecFrequencies.Add(frequency);
        return this;
    }

    public IMotherBoardBuilder ClearJedecFrequencies()
    {
        _jedecFrequencies ??= new HashSet<Frequency>();
        _jedecFrequencies.Clear();
        return this;
    }

    public IMotherBoardBuilder WithOverclockedFrequencies(IEnumerable<Frequency> frequencies)
    {
        _overclockedFrequencies = frequencies.ToHashSet();
        return this;
    }

    public IMotherBoardBuilder AddOverclockedFrequency(Frequency frequency)
    {
        _overclockedFrequencies ??= new HashSet<Frequency>();
        _overclockedFrequencies.Add(frequency);
        return this;
    }

    public IMotherBoardBuilder ClearOverclockedFrequencies()
    {
        _overclockedFrequencies ??= new HashSet<Frequency>();
        _overclockedFrequencies.Clear();
        return this;
    }

    public IMotherBoardBuilder WithDdrStandard(DdrStandard standard)
    {
        _ddrStandard = standard;
        return this;
    }

    public IMotherBoardBuilder WithRamSlots(Slots ramSlots)
    {
        _ramSlots = ramSlots;
        return this;
    }

    public IMotherBoardBuilder WithFormFactor(MotherBoardFormFactor formFactor)
    {
        _formFactor = formFactor;
        return this;
    }

    public IMotherBoardBuilder WithSupportedMemoryFormFactor(RandomAccessMemoryFormFactor formFactor)
    {
        _supportedMemoryFormFactor = formFactor;
        return this;
    }

    public IMotherBoardBuilder WithBios(Bios bios)
    {
        _bios = bios;
        return this;
    }

    public IMotherBoardBuilder WithWiFiAdapter(WiFiAdapter? wiFiAdapter)
    {
        _wiFiAdapter = wiFiAdapter;
        return this;
    }

    public IMotherBoardBuilder WithoutWiFiAdapter()
    {
        return WithWiFiAdapter(null);
    }
}