using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public class GraphicsCard : IPcComponent<GraphicsCard>, IPcComponent, IGraphicsCardBuilderDirector
{
    public GraphicsCard(
        string name,
        TwoDimensions size,
        MemoryCapacity videoMemoryCapacity,
        PciEConnection connection,
        Frequency chipFrequency,
        Power powerConsumption)
    {
        Name = name;
        Size = size;
        VideoMemoryCapacity = videoMemoryCapacity;
        Connection = connection;
        ChipFrequency = chipFrequency;
        PowerConsumption = powerConsumption;
    }

    public string Name { get; }
    public TwoDimensions Size { get; }
    public MemoryCapacity VideoMemoryCapacity { get; }
    public PciEConnection Connection { get; }
    public Frequency ChipFrequency { get; }
    public Power PowerConsumption { get; }

    public virtual GraphicsCard Clone() =>
        new GraphicsCard(Name, Size, VideoMemoryCapacity, Connection, ChipFrequency, PowerConsumption);

    public IGraphicsCardBuilder Direct(IGraphicsCardBuilder builder)
    {
        BuilderException.ThrowIfNull(builder);

        return builder.WithName(Name).WithSize(Size).WithVideoMemoryCapacity(VideoMemoryCapacity)
            .WithConnection(Connection).WithChipFrequency(ChipFrequency).WithPowerConsumption(PowerConsumption);
    }
}