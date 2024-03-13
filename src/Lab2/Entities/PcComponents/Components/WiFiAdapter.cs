using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public class WiFiAdapter : IPcComponent<WiFiAdapter>, IPcComponent, IWiFiAdapterBuilderDirector
{
    internal WiFiAdapter(
        string name,
        TechnologyVersion wiFiVersion,
        PciEConnection connection,
        Power powerConsumption,
        bool hasIntegratedBluetoothModule)
    {
        Name = name;
        WiFiVersion = wiFiVersion;
        Connection = connection;
        PowerConsumption = powerConsumption;
        HasIntegratedBluetoothModule = hasIntegratedBluetoothModule;
    }

    public string Name { get; }
    public TechnologyVersion WiFiVersion { get; }
    public PciEConnection Connection { get; }
    public Power PowerConsumption { get; }
    public bool HasIntegratedBluetoothModule { get; }

    public virtual WiFiAdapter Clone() =>
        new WiFiAdapter(Name, WiFiVersion, Connection, PowerConsumption, HasIntegratedBluetoothModule);

    public IWiFiAdapterBuilder Direct(IWiFiAdapterBuilder builder)
    {
        IPcComponent<IPcComponent> a = this.Clone();
        BuilderException.ThrowIfNull(builder);
        if (HasIntegratedBluetoothModule) builder.WithIntegratedBluetoothModule();
        else builder.WithoutIntegratedBluetoothModule();

        return builder.WithName(Name).WithWiFiVersion(WiFiVersion).WithConnection(Connection)
            .WithPowerConsumption(PowerConsumption);
    }
}