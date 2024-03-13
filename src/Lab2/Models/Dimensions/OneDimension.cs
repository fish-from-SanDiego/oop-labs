using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;

public record OneDimension
{
    public OneDimension(double heightMm)
    {
        ComponentAttributeException.ThrowIfNonPositive(heightMm);
        HeightMm = heightMm;
    }

    public double HeightMm { get; }
}