namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

public class LargeSizeHullDisperser : BasedOnSizeDisperser
{
    private const double FirstDamageBoundCoefficient = 0.5d;
    private const double SecondDamageBoundCoefficient = 1d;

    public LargeSizeHullDisperser()
        : base(FirstDamageBoundCoefficient, SecondDamageBoundCoefficient)
    {
    }
}