using System;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public class WiFiAdapterBuilder : IWiFiAdapterBuilder
{
    private string? _name;
    private TechnologyVersion? _wiFiVersion;
    private Power? _powerConsumption;
    private PciEConnection? _connection;
    private bool _hasIntegratedBluetoothModule;

    public WiFiAdapterBuilder()
    {
        _hasIntegratedBluetoothModule = false;
    }

    public WiFiAdapter Build() => new WiFiAdapter(
        _name ?? throw new BuilderException(new InvalidOperationException(nameof(_name))),
        _wiFiVersion ?? throw new BuilderException(new InvalidOperationException(nameof(_wiFiVersion))),
        _connection ?? throw new BuilderException(new InvalidOperationException(nameof(_connection))),
        _powerConsumption ?? throw new BuilderException(new InvalidOperationException(nameof(_powerConsumption))),
        _hasIntegratedBluetoothModule);

    public IWiFiAdapterBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public IWiFiAdapterBuilder WithWiFiVersion(TechnologyVersion version)
    {
        _wiFiVersion = version;
        return this;
    }

    public IWiFiAdapterBuilder WithConnection(PciEConnection connection)
    {
        _connection = connection;
        return this;
    }

    public IWiFiAdapterBuilder WithPowerConsumption(Power powerConsumption)
    {
        _powerConsumption = powerConsumption;
        return this;
    }

    public IWiFiAdapterBuilder WithIntegratedBluetoothModule()
    {
        _hasIntegratedBluetoothModule = true;
        return this;
    }

    public IWiFiAdapterBuilder WithoutIntegratedBluetoothModule()
    {
        _hasIntegratedBluetoothModule = false;
        return this;
    }
}