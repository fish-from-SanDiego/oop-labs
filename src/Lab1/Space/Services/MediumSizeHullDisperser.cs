namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

public class MediumSizeHullDisperser : BasedOnSizeDisperser
{
    private const double FirstDamageBoundCoefficient = 0.8d;
    private const double SecondDamageBoundCoefficient = 1d;

    public MediumSizeHullDisperser()
        : base(FirstDamageBoundCoefficient, SecondDamageBoundCoefficient)
    {
    }
}