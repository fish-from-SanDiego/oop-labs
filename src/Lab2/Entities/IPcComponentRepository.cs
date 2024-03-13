using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public interface IPcComponentRepository
{
    public IReadOnlyCollection<MotherBoard> MotherBoards { get; }
    public IReadOnlyCollection<CentralProcessor> CentralProcessors { get; }
    public IReadOnlyCollection<CpuCoolingSystem> CpuCoolingSystems { get; }
    public IReadOnlyCollection<PcCase> PcCases { get; }
    public IReadOnlyCollection<PowerSupply> PowerSupplies { get; }
    public IReadOnlyCollection<WiFiAdapter> WiFiAdapters { get; }
    public IReadOnlyCollection<RandomAccessMemory> RamComponents { get; }
    public IReadOnlyCollection<GraphicsCard> GraphicsCards { get; }
    public IReadOnlyCollection<HardDrive> HardDrives { get; }
    public IReadOnlyCollection<SolidStateDrive> SolidStateDrives { get; }

    public IReadOnlyCollection<Bios> BiosCollection { get; }

    void RegisterMotherBoard(MotherBoard motherBoard);
    void RegisterBios(Bios bios);
    void RegisterCentralProcessor(CentralProcessor centralProcessor);
    void RegisterCpuCoolingSystem(CpuCoolingSystem cpuCoolingSystem);
    void RegisterGraphicsCard(GraphicsCard graphicsCard);
    void RegisterHardDrive(HardDrive drive);
    void RegisterSolidStateDrive(SolidStateDrive drive);
    void RegisterPcCase(PcCase pcCase);
    void RegisterPowerSupply(PowerSupply powerSupply);
    void RegisterRandomAccessMemory(RandomAccessMemory randomAccessMemory);
    void RegisterWiFiAdapter(WiFiAdapter wiFiAdapter);
}