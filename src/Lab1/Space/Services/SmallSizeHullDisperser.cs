namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

public class SmallSizeHullDisperser : BasedOnSizeDisperser
{
    private const double FirstDamageBoundCoefficient = 1d;
    private const double SecondDamageBoundCoefficient = 1d;

    public SmallSizeHullDisperser()
        : base(FirstDamageBoundCoefficient, SecondDamageBoundCoefficient)
    {
    }
}