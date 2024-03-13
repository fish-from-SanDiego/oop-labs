using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class PcComponentRepository : IPcComponentRepository
{
    private readonly List<MotherBoard> _motherBoards;
    private readonly List<CentralProcessor> _centralProcessors;
    private readonly List<CpuCoolingSystem> _cpuCoolingSystems;
    private readonly List<PcCase> _pcCases;
    private readonly List<PowerSupply> _powerSupplies;
    private readonly List<WiFiAdapter> _wiFiAdapters;
    private readonly List<RandomAccessMemory> _ramComponents;
    private readonly List<GraphicsCard> _graphicsCards;
    private readonly List<HardDrive> _hardDrives;
    private readonly List<SolidStateDrive> _solidStateDrives;
    private readonly List<Bios> _biosList;

    public PcComponentRepository()
    {
        _motherBoards = new List<MotherBoard>();
        _centralProcessors = new List<CentralProcessor>();
        _cpuCoolingSystems = new List<CpuCoolingSystem>();
        _pcCases = new List<PcCase>();
        _powerSupplies = new List<PowerSupply>();
        _wiFiAdapters = new List<WiFiAdapter>();
        _ramComponents = new List<RandomAccessMemory>();
        _graphicsCards = new List<GraphicsCard>();
        _hardDrives = new List<HardDrive>();
        _solidStateDrives = new List<SolidStateDrive>();
        _biosList = new List<Bios>();
    }

    public IReadOnlyCollection<MotherBoard> MotherBoards => _motherBoards;
    public IReadOnlyCollection<CentralProcessor> CentralProcessors => _centralProcessors;
    public IReadOnlyCollection<CpuCoolingSystem> CpuCoolingSystems => _cpuCoolingSystems;
    public IReadOnlyCollection<PcCase> PcCases => _pcCases;
    public IReadOnlyCollection<PowerSupply> PowerSupplies => _powerSupplies;
    public IReadOnlyCollection<WiFiAdapter> WiFiAdapters => _wiFiAdapters;
    public IReadOnlyCollection<RandomAccessMemory> RamComponents => _ramComponents;
    public IReadOnlyCollection<GraphicsCard> GraphicsCards => _graphicsCards;
    public IReadOnlyCollection<HardDrive> HardDrives => _hardDrives;
    public IReadOnlyCollection<SolidStateDrive> SolidStateDrives => _solidStateDrives;
    public IReadOnlyCollection<Bios> BiosCollection => _biosList;

    public void RegisterMotherBoard(MotherBoard motherBoard)
    {
        _motherBoards.Add(motherBoard);
    }

    public void RegisterBios(Bios bios)
    {
        _biosList.Add(bios);
    }

    public void RegisterCentralProcessor(CentralProcessor centralProcessor)
    {
        _centralProcessors.Add(centralProcessor);
    }

    public void RegisterCpuCoolingSystem(CpuCoolingSystem cpuCoolingSystem)
    {
        _cpuCoolingSystems.Add(cpuCoolingSystem);
    }

    public void RegisterGraphicsCard(GraphicsCard graphicsCard)
    {
        _graphicsCards.Add(graphicsCard);
    }

    public void RegisterHardDrive(HardDrive drive)
    {
        _hardDrives.Add(drive);
    }

    public void RegisterSolidStateDrive(SolidStateDrive drive)
    {
        _solidStateDrives.Add(drive);
    }

    public void RegisterPcCase(PcCase pcCase)
    {
        _pcCases.Add(pcCase);
    }

    public void RegisterPowerSupply(PowerSupply powerSupply)
    {
        _powerSupplies.Add(powerSupply);
    }

    public void RegisterRandomAccessMemory(RandomAccessMemory randomAccessMemory)
    {
        _ramComponents.Add(randomAccessMemory);
    }

    public void RegisterWiFiAdapter(WiFiAdapter wiFiAdapter)
    {
        _wiFiAdapters.Add(wiFiAdapter);
    }
}