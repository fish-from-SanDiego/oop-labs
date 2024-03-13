using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;

public record TwoDimensions
{
    public TwoDimensions(double heightMm, double widthMm)
    {
        ComponentAttributeException.ThrowIfNonPositive(heightMm, nameof(heightMm));
        ComponentAttributeException.ThrowIfNonPositive(widthMm, nameof(widthMm));

        HeightMm = heightMm;
        WidthMm = widthMm;
    }

    public double HeightMm { get; }
    public double WidthMm { get; }
}