using System;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public class CentralProcessorBuilder : ICentralProcessorBuilder
{
    private string? _name;
    private CpuCores? _cores;
    private CpuSocket? _socket;
    private bool _hasIntegratedVideoCore;
    private Frequency? _maxSupportedRamFrequency;
    private Power? _thermalDesignPower;
    private Power? _powerConsumption;

    public CentralProcessorBuilder()
    {
        _hasIntegratedVideoCore = false;
    }

    public CentralProcessor Build()
    {
        if (_maxSupportedRamFrequency is null)
            throw new BuilderException(new InvalidOperationException(nameof(_maxSupportedRamFrequency)));
        if (_thermalDesignPower is null)
            throw new BuilderException(new InvalidOperationException(nameof(_thermalDesignPower)));
        return new CentralProcessor(
            _name ?? throw new BuilderException(new InvalidOperationException(nameof(_name))),
            _cores ?? throw new BuilderException(new InvalidOperationException(nameof(_cores))),
            _socket ?? throw new BuilderException(new InvalidOperationException(nameof(_socket))),
            _hasIntegratedVideoCore,
            _maxSupportedRamFrequency,
            _thermalDesignPower,
            _powerConsumption ?? throw new BuilderException(new InvalidOperationException(nameof(_powerConsumption))));
    }

    public ICentralProcessorBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ICentralProcessorBuilder WithCores(CpuCores cores)
    {
        _cores = cores;
        return this;
    }

    public ICentralProcessorBuilder WithSocket(CpuSocket socket)
    {
        _socket = socket;
        return this;
    }

    public ICentralProcessorBuilder WithIntegratedVideoCore()
    {
        _hasIntegratedVideoCore = true;
        return this;
    }

    public ICentralProcessorBuilder WithoutIntegratedVideoCore()
    {
        _hasIntegratedVideoCore = false;
        return this;
    }

    public ICentralProcessorBuilder WithMaxSupportedRamFrequency(Frequency frequency)
    {
        _maxSupportedRamFrequency = frequency;
        return this;
    }

    public ICentralProcessorBuilder WithThermalDesignPower(Power thermalDesignPower)
    {
        _thermalDesignPower = thermalDesignPower;
        return this;
    }

    public ICentralProcessorBuilder WithPowerConsumption(Power powerConsumption)
    {
        _powerConsumption = powerConsumption;
        return this;
    }
}