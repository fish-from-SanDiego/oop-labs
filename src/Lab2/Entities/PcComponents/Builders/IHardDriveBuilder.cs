using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface IHardDriveBuilder : IPcComponentBuilder<HardDrive>
{
    IHardDriveBuilder WithName(string name);
    IHardDriveBuilder WithCapacity(MemoryCapacity capacity);
    IHardDriveBuilder WithSpindleSpeed(RotationalSpeed spindleSpeed);
    IHardDriveBuilder WithPowerConsumption(Power powerConsumption);
}