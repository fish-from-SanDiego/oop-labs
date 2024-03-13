using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class PcOrderBuilder : IPcOrderBuilder
{
    private MotherBoard? _motherBoard;
    private CentralProcessor? _centralProcessor;
    private CpuCoolingSystem? _cpuCoolingSystem;
    private PcCase? _pcCase;
    private PowerSupply? _powerSupply;
    private WiFiAdapter? _wiFiAdapter;
    private List<RandomAccessMemory>? _ramComponents;
    private List<GraphicsCard>? _graphicsCards;
    private List<HardDrive>? _hardDrives;
    private List<SolidStateDrive>? _solidStateDrives;

    public PcOrderBuilder()
    {
        _graphicsCards = new List<GraphicsCard>();
        _hardDrives = new List<HardDrive>();
        _solidStateDrives = new List<SolidStateDrive>();
    }

    public IPcOrder Build()
    {
        if (_ramComponents is null || _ramComponents.Count == 0)
            throw new BuilderException(new InvalidOperationException(nameof(_ramComponents)));
        if (_centralProcessor is null)
            throw new BuilderException(new InvalidOperationException(nameof(_centralProcessor)));
        if (_graphicsCards is null)
            throw new BuilderException(new InvalidOperationException(nameof(_graphicsCards)));
        if (_hardDrives is null)
            throw new BuilderException(new InvalidOperationException(nameof(_hardDrives)));
        if (_solidStateDrives is null)
            throw new BuilderException(new InvalidOperationException(nameof(_solidStateDrives)));
        if (!_centralProcessor.HasIntegratedVideoCore && _graphicsCards.Count == 0)
            throw new BuilderException(new InvalidOperationException(nameof(_graphicsCards)));
        if (_solidStateDrives.Count == 0 && _hardDrives.Count == 0)
            throw new BuilderException(new InvalidOperationException(nameof(_hardDrives)));
        return new PcOrder(
            _motherBoard ?? throw new BuilderException(new InvalidOperationException(nameof(_motherBoard))),
            _centralProcessor,
            _cpuCoolingSystem ?? throw new BuilderException(new InvalidOperationException(nameof(_cpuCoolingSystem))),
            _pcCase ?? throw new BuilderException(new InvalidOperationException(nameof(_pcCase))),
            _powerSupply ?? throw new BuilderException(new InvalidOperationException(nameof(_powerSupply))),
            _ramComponents,
            _graphicsCards,
            _hardDrives,
            _solidStateDrives,
            _wiFiAdapter);
    }

    public IPcOrderBuilder WithMotherBoard(MotherBoard motherBoard)
    {
        _motherBoard = motherBoard;
        return this;
    }

    public IPcOrderBuilder WithCentralProcessor(CentralProcessor centralProcessor)
    {
        _centralProcessor = centralProcessor;
        return this;
    }

    public IPcOrderBuilder WithCpuCoolingSystem(CpuCoolingSystem cpuCoolingSystem)
    {
        _cpuCoolingSystem = cpuCoolingSystem;
        return this;
    }

    public IPcOrderBuilder WithPcCase(PcCase pcCase)
    {
        _pcCase = pcCase;
        return this;
    }

    public IPcOrderBuilder WithPowerSupply(PowerSupply powerSupply)
    {
        _powerSupply = powerSupply;
        return this;
    }

    public IPcOrderBuilder WithWiFiAdapter(WiFiAdapter? wiFiAdapter)
    {
        _wiFiAdapter = wiFiAdapter;
        return this;
    }

    public IPcOrderBuilder WithoutWiFiAdapter()
    {
        return WithWiFiAdapter(null);
    }

    public IPcOrderBuilder WithGraphicsCards(IEnumerable<GraphicsCard> graphicsCards)
    {
        _graphicsCards = graphicsCards.ToList();
        return this;
    }

    public IPcOrderBuilder AddGraphicsCard(GraphicsCard graphicsCard)
    {
        _graphicsCards ??= new List<GraphicsCard>();
        _graphicsCards.Add(graphicsCard);
        return this;
    }

    public IPcOrderBuilder ClearGraphicsCards()
    {
        _graphicsCards ??= new List<GraphicsCard>();
        _graphicsCards.Clear();
        return this;
    }

    public IPcOrderBuilder WithHardDrives(IEnumerable<HardDrive> hardDrives)
    {
        _hardDrives = hardDrives.ToList();
        return this;
    }

    public IPcOrderBuilder AddHardDrive(HardDrive hardDrive)
    {
        _hardDrives ??= new List<HardDrive>();
        _hardDrives.Add(hardDrive);
        return this;
    }

    public IPcOrderBuilder ClearHardDrives()
    {
        _hardDrives ??= new List<HardDrive>();
        _hardDrives.Clear();
        return this;
    }

    public IPcOrderBuilder WithRamComponents(IEnumerable<RandomAccessMemory> ramComponents)
    {
        _ramComponents = ramComponents.ToList();
        return this;
    }

    public IPcOrderBuilder AddRamComponent(RandomAccessMemory randomAccessMemory)
    {
        _ramComponents ??= new List<RandomAccessMemory>();
        _ramComponents.Add(randomAccessMemory);
        return this;
    }

    public IPcOrderBuilder ClearRamComponents()
    {
        _ramComponents ??= new List<RandomAccessMemory>();
        _ramComponents.Clear();
        return this;
    }

    public IPcOrderBuilder WithSolidStateDrives(IEnumerable<SolidStateDrive> solidStateDrives)
    {
        _solidStateDrives = solidStateDrives.ToList();
        return this;
    }

    public IPcOrderBuilder AddSolidStateDrive(SolidStateDrive solidStateDrive)
    {
        _solidStateDrives ??= new List<SolidStateDrive>();
        _solidStateDrives.Add(solidStateDrive);
        return this;
    }

    public IPcOrderBuilder ClearSolidStateDrives()
    {
        _solidStateDrives ??= new List<SolidStateDrive>();
        _solidStateDrives.Clear();
        return this;
    }
}