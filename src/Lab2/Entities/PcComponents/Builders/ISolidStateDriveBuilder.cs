using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface ISolidStateDriveBuilder : IPcComponentBuilder<SolidStateDrive>
{
    ISolidStateDriveBuilder WithName(string name);
    ISolidStateDriveBuilder WithCapacity(MemoryCapacity capacity);
    ISolidStateDriveBuilder WithMemoryReadMaxSpeed(MemoryTransferSpeed speed);
    ISolidStateDriveBuilder WithMemoryWriteMaxSpeed(MemoryTransferSpeed speed);
    ISolidStateDriveBuilder WithPowerConsumption(Power powerConsumption);
    ISolidStateDriveBuilder WithConnection(ComponentConnection connection);
}