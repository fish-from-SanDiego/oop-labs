namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

public class MediumSizeDeflectorDisperser : BasedOnSizeDisperser
{
    private const double FirstDamageBoundCoefficient = 0.6d;
    private const double SecondDamageBoundCoefficient = 1d;

    public MediumSizeDeflectorDisperser()
        : base(FirstDamageBoundCoefficient, SecondDamageBoundCoefficient)
    {
    }
}