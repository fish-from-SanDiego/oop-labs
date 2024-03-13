using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface IWiFiAdapterBuilder : IPcComponentBuilder<WiFiAdapter>
{
    IWiFiAdapterBuilder WithName(string name);
    IWiFiAdapterBuilder WithWiFiVersion(TechnologyVersion version);
    IWiFiAdapterBuilder WithConnection(PciEConnection connection);
    IWiFiAdapterBuilder WithPowerConsumption(Power powerConsumption);
    IWiFiAdapterBuilder WithIntegratedBluetoothModule();
    IWiFiAdapterBuilder WithoutIntegratedBluetoothModule();
}