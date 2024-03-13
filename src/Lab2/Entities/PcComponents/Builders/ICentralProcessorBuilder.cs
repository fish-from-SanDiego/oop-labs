using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface ICentralProcessorBuilder : IPcComponentBuilder<CentralProcessor>
{
    ICentralProcessorBuilder WithName(string name);
    ICentralProcessorBuilder WithCores(CpuCores cores);
    ICentralProcessorBuilder WithSocket(CpuSocket socket);
    ICentralProcessorBuilder WithIntegratedVideoCore();
    ICentralProcessorBuilder WithoutIntegratedVideoCore();
    ICentralProcessorBuilder WithMaxSupportedRamFrequency(Frequency frequency);
    ICentralProcessorBuilder WithThermalDesignPower(Power thermalDesignPower);
    ICentralProcessorBuilder WithPowerConsumption(Power powerConsumption);
}