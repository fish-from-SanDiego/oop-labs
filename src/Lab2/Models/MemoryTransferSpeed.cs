namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record MemoryTransferSpeed
{
    public MemoryTransferSpeed(double megabytesPerSecond)
    {
        MegabytesPerSecond = megabytesPerSecond;
    }

    public double MegabytesPerSecond { get; }
}