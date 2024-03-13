using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface IGraphicsCardBuilder : IPcComponentBuilder<GraphicsCard>
{
    IGraphicsCardBuilder WithName(string name);
    IGraphicsCardBuilder WithSize(TwoDimensions size);
    IGraphicsCardBuilder WithVideoMemoryCapacity(MemoryCapacity capacity);
    IGraphicsCardBuilder WithConnection(PciEConnection connection);
    IGraphicsCardBuilder WithChipFrequency(Frequency chipFrequency);
    IGraphicsCardBuilder WithPowerConsumption(Power powerConsumption);
}