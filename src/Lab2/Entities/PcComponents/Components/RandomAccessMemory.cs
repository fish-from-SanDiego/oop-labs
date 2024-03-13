using System.Collections.Generic;
using System.Collections.Immutable;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Ddr;
using Itmo.ObjectOrientedProgramming.Lab2.Models.FormFactors;
using Itmo.ObjectOrientedProgramming.Lab2.Models.MemoryProfiles;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public class RandomAccessMemory : IPcComponent<RandomAccessMemory>, IPcComponent, IRandomAccessMemoryBuilderDirector
{
    internal RandomAccessMemory(
        string name,
        MemoryCapacity memoryCapacity,
        IReadOnlyDictionary<Frequency, Voltage> jedecFrequenciesInfo,
        IEnumerable<OverclockedMemoryProfile> overclockedMemoryProfiles,
        RandomAccessMemoryFormFactor formFactor,
        DdrStandard ddrStandard,
        Power powerConsumption)
    {
        Name = name;
        MemoryCapacity = memoryCapacity;
        FormFactor = formFactor;
        DdrStandard = ddrStandard;
        PowerConsumption = powerConsumption;
        OverclockedMemoryProfiles = overclockedMemoryProfiles.ToImmutableHashSet();
        JedecFrequenciesInfo = jedecFrequenciesInfo.ToImmutableDictionary();
    }

    public string Name { get; }
    public MemoryCapacity MemoryCapacity { get; }
    public IReadOnlyDictionary<Frequency, Voltage> JedecFrequenciesInfo { get; }
    public IReadOnlySet<OverclockedMemoryProfile> OverclockedMemoryProfiles { get; }
    public RandomAccessMemoryFormFactor FormFactor { get; }
    public DdrStandard DdrStandard { get; }
    public Power PowerConsumption { get; }

    public virtual RandomAccessMemory Clone() => new RandomAccessMemory(
        Name,
        MemoryCapacity,
        JedecFrequenciesInfo,
        OverclockedMemoryProfiles,
        FormFactor,
        DdrStandard,
        PowerConsumption);

    public IRandomAccessMemoryBuilder Direct(IRandomAccessMemoryBuilder builder)
    {
        BuilderException.ThrowIfNull(builder);

        return builder.WithName(Name).WithMemoryCapacity(MemoryCapacity).WithJedecFrequenciesInfo(JedecFrequenciesInfo)
            .WithOverclockedMemoryProfiles(OverclockedMemoryProfiles).WithFormFactor(FormFactor)
            .WithDdrStandard(DdrStandard).WithPowerConsumption(PowerConsumption);
    }
}