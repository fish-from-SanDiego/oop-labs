using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public interface IPcOrderBuilder
{
    IPcOrder Build();
    IPcOrderBuilder WithMotherBoard(MotherBoard motherBoard);
    IPcOrderBuilder WithCentralProcessor(CentralProcessor centralProcessor);
    IPcOrderBuilder WithCpuCoolingSystem(CpuCoolingSystem cpuCoolingSystem);
    IPcOrderBuilder WithPcCase(PcCase pcCase);
    IPcOrderBuilder WithPowerSupply(PowerSupply powerSupply);
    IPcOrderBuilder WithWiFiAdapter(WiFiAdapter? wiFiAdapter);
    IPcOrderBuilder WithoutWiFiAdapter();
    IPcOrderBuilder WithGraphicsCards(IEnumerable<GraphicsCard> graphicsCards);
    IPcOrderBuilder AddGraphicsCard(GraphicsCard graphicsCard);
    IPcOrderBuilder ClearGraphicsCards();
    IPcOrderBuilder WithHardDrives(IEnumerable<HardDrive> hardDrives);
    IPcOrderBuilder AddHardDrive(HardDrive hardDrive);
    IPcOrderBuilder ClearHardDrives();
    IPcOrderBuilder WithRamComponents(IEnumerable<RandomAccessMemory> ramComponents);
    IPcOrderBuilder AddRamComponent(RandomAccessMemory randomAccessMemory);
    IPcOrderBuilder ClearRamComponents();
    IPcOrderBuilder WithSolidStateDrives(IEnumerable<SolidStateDrive> solidStateDrives);
    IPcOrderBuilder AddSolidStateDrive(SolidStateDrive solidStateDrive);
    IPcOrderBuilder ClearSolidStateDrives();
}