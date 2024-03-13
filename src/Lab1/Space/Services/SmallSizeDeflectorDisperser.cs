namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

public class SmallSizeDeflectorDisperser : BasedOnSizeDisperser
{
    private const double FirstDamageBoundCoefficient = 1d;
    private const double SecondDamageBoundCoefficient = 1d;

    public SmallSizeDeflectorDisperser()
        : base(FirstDamageBoundCoefficient, SecondDamageBoundCoefficient)
    {
    }
}