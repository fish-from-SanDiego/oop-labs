using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Ddr;
using Itmo.ObjectOrientedProgramming.Lab2.Models.FormFactors;
using Itmo.ObjectOrientedProgramming.Lab2.Models.MemoryProfiles;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface IRandomAccessMemoryBuilder : IPcComponentBuilder<RandomAccessMemory>
{
    IRandomAccessMemoryBuilder WithName(string name);
    IRandomAccessMemoryBuilder WithMemoryCapacity(MemoryCapacity capacity);
    IRandomAccessMemoryBuilder WithJedecFrequenciesInfo(IReadOnlyDictionary<Frequency, Voltage> info);
    IRandomAccessMemoryBuilder SetJedecFrequencyInfo(Frequency frequency, Voltage voltage);
    IRandomAccessMemoryBuilder ClearJedecFrequencies();
    IRandomAccessMemoryBuilder WithOverclockedMemoryProfiles(IEnumerable<OverclockedMemoryProfile> profiles);
    IRandomAccessMemoryBuilder AddOverclockedMemoryProfile(OverclockedMemoryProfile profile);
    IRandomAccessMemoryBuilder ClearOverclockedMemoryProfiles();
    IRandomAccessMemoryBuilder WithFormFactor(RandomAccessMemoryFormFactor formFactor);
    IRandomAccessMemoryBuilder WithDdrStandard(DdrStandard standard);
    IRandomAccessMemoryBuilder WithPowerConsumption(Power powerConsumption);
}