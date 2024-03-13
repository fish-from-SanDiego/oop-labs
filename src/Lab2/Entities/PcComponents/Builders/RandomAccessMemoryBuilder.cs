using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Ddr;
using Itmo.ObjectOrientedProgramming.Lab2.Models.FormFactors;
using Itmo.ObjectOrientedProgramming.Lab2.Models.MemoryProfiles;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public class RandomAccessMemoryBuilder : IRandomAccessMemoryBuilder
{
    private string? _name;
    private MemoryCapacity? _capacity;
    private Dictionary<Frequency, Voltage>? _frequenciesInfo;
    private HashSet<OverclockedMemoryProfile>? _profiles;
    private RandomAccessMemoryFormFactor? _formFactor;
    private DdrStandard? _standard;
    private Power? _powerConsumption;

    public RandomAccessMemoryBuilder()
    {
        _profiles = new HashSet<OverclockedMemoryProfile>();
    }

    public RandomAccessMemory Build()
    {
        if (_frequenciesInfo is null || _frequenciesInfo.Count == 0)
            throw new BuilderException(new InvalidOperationException(nameof(_frequenciesInfo)));
        return new RandomAccessMemory(
            _name ?? throw new BuilderException(new InvalidOperationException(nameof(_name))),
            _capacity ?? throw new BuilderException(new InvalidOperationException(nameof(_capacity))),
            _frequenciesInfo,
            _profiles ?? throw new BuilderException(new InvalidOperationException(nameof(_frequenciesInfo))),
            _formFactor ?? throw new BuilderException(new InvalidOperationException(nameof(_formFactor))),
            _standard ?? throw new BuilderException(new InvalidOperationException(nameof(_standard))),
            _powerConsumption ?? throw new BuilderException(new InvalidOperationException(nameof(_powerConsumption))));
    }

    public IRandomAccessMemoryBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public IRandomAccessMemoryBuilder WithMemoryCapacity(MemoryCapacity capacity)
    {
        _capacity = capacity;
        return this;
    }

    public IRandomAccessMemoryBuilder WithJedecFrequenciesInfo(IReadOnlyDictionary<Frequency, Voltage> info)
    {
        _frequenciesInfo = info.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        return this;
    }

    public IRandomAccessMemoryBuilder SetJedecFrequencyInfo(Frequency frequency, Voltage voltage)
    {
        _frequenciesInfo ??= new Dictionary<Frequency, Voltage>();
        _frequenciesInfo[frequency] = voltage;
        return this;
    }

    public IRandomAccessMemoryBuilder ClearJedecFrequencies()
    {
        _frequenciesInfo ??= new Dictionary<Frequency, Voltage>();
        _frequenciesInfo.Clear();
        return this;
    }

    public IRandomAccessMemoryBuilder WithOverclockedMemoryProfiles(IEnumerable<OverclockedMemoryProfile> profiles)
    {
        _profiles = profiles.ToHashSet();
        return this;
    }

    public IRandomAccessMemoryBuilder AddOverclockedMemoryProfile(OverclockedMemoryProfile profile)
    {
        _profiles ??= new HashSet<OverclockedMemoryProfile>();
        _profiles.Add(profile);
        return this;
    }

    public IRandomAccessMemoryBuilder ClearOverclockedMemoryProfiles()
    {
        _profiles ??= new HashSet<OverclockedMemoryProfile>();
        _profiles.Clear();
        return this;
    }

    public IRandomAccessMemoryBuilder WithFormFactor(RandomAccessMemoryFormFactor formFactor)
    {
        _formFactor = formFactor;
        return this;
    }

    public IRandomAccessMemoryBuilder WithDdrStandard(DdrStandard standard)
    {
        _standard = standard;
        return this;
    }

    public IRandomAccessMemoryBuilder WithPowerConsumption(Power powerConsumption)
    {
        _powerConsumption = powerConsumption;
        return this;
    }
}