using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;

public record ThreeDimensions
{
    public ThreeDimensions(double heightMm, double widthMm, double lengthMm)
    {
        ComponentAttributeException.ThrowIfNonPositive(heightMm, nameof(heightMm));
        ComponentAttributeException.ThrowIfNonPositive(widthMm, nameof(widthMm));
        ComponentAttributeException.ThrowIfNonPositive(lengthMm, nameof(lengthMm));

        HeightMm = heightMm;
        WidthMm = widthMm;
        LengthMm = lengthMm;
    }

    public double HeightMm { get; }
    public double WidthMm { get; }
    public double LengthMm { get; }
}