namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

public class LargeSizeDeflectorDisperser : BasedOnSizeDisperser
{
    private const double FirstDamageBoundCoefficient = 0.4d;
    private const double SecondDamageBoundCoefficient = 0.8d;

    public LargeSizeDeflectorDisperser()
        : base(FirstDamageBoundCoefficient, SecondDamageBoundCoefficient)
    {
    }
}