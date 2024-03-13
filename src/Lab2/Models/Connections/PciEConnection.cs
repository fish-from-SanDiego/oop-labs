using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;

public record PciEConnection : ComponentConnection
{
    public PciEConnection(int lanesNumber)
    {
        ComponentAttributeException.ThrowIfNonPositive(lanesNumber, nameof(lanesNumber));

        LanesNumber = lanesNumber;
    }

    public int LanesNumber { get; }

    public static PciEConnection PciEx1 => new PciEConnection(1);
    public static PciEConnection PciEx4 => new PciEConnection(4);
    public static PciEConnection PciEx8 => new PciEConnection(8);
    public static PciEConnection PciEx16 => new PciEConnection(16);
}