using System;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;

public class CentralProcessor : IPcComponent<CentralProcessor>, IPcComponent, ICentralProcessorBuilderDirector,
    IEquatable<CentralProcessor>
{
    internal CentralProcessor(
        string name,
        CpuCores cores,
        CpuSocket socket,
        bool hasIntegratedVideoCore,
        Frequency maxSupportedRamFrequency,
        Power thermalDesignPower,
        Power powerConsumption)
    {
        Name = name;
        Cores = cores;
        Socket = socket;
        HasIntegratedVideoCore = hasIntegratedVideoCore;
        ThermalDesignPower = thermalDesignPower;
        PowerConsumption = powerConsumption;
        MaxSupportedRamFrequency = maxSupportedRamFrequency;
    }

    public string Name { get; }
    public CpuCores Cores { get; }
    public CpuSocket Socket { get; }
    public bool HasIntegratedVideoCore { get; }
    public Frequency MaxSupportedRamFrequency { get; }
    public Power ThermalDesignPower { get; }
    public Power PowerConsumption { get; }

    public virtual CentralProcessor Clone() => new CentralProcessor(
        Name,
        Cores,
        Socket,
        HasIntegratedVideoCore,
        MaxSupportedRamFrequency,
        ThermalDesignPower,
        PowerConsumption);

    public bool Equals(CentralProcessor? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Cores.Equals(other.Cores) && Socket.Equals(other.Socket) &&
               HasIntegratedVideoCore == other.HasIntegratedVideoCore &&
               MaxSupportedRamFrequency.Equals(other.MaxSupportedRamFrequency) &&
               ThermalDesignPower.Equals(other.ThermalDesignPower) && PowerConsumption.Equals(other.PowerConsumption);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((CentralProcessor)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            Name,
            Cores,
            Socket,
            HasIntegratedVideoCore,
            MaxSupportedRamFrequency,
            ThermalDesignPower,
            PowerConsumption);
    }

    public ICentralProcessorBuilder Direct(ICentralProcessorBuilder builder)
    {
        BuilderException.ThrowIfNull(builder);
        if (HasIntegratedVideoCore) builder.WithIntegratedVideoCore();
        else builder.WithoutIntegratedVideoCore();

        return builder.WithName(Name).WithCores(Cores).WithSocket(Socket)
            .WithMaxSupportedRamFrequency(MaxSupportedRamFrequency).WithThermalDesignPower(ThermalDesignPower)
            .WithPowerConsumption(PowerConsumption);
    }
}