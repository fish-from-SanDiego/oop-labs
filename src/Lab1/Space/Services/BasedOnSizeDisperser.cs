using System;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Services;

public abstract class BasedOnSizeDisperser : IDamageDisperser
{
    private const double FirstDamageBound = 20d;
    private const double SecondDamageBound = 40d;
    private const double OverBoundCoefficient = 1d;

    private readonly double _firstDamageBoundCoefficient;
    private readonly double _secondDamageBoundCoefficient;

    protected BasedOnSizeDisperser(double firstDamageBoundCoefficient, double secondDamageBoundCoefficient)
    {
        if (firstDamageBoundCoefficient > OverBoundCoefficient
            || secondDamageBoundCoefficient > OverBoundCoefficient
            || firstDamageBoundCoefficient < 0d
            || secondDamageBoundCoefficient < 0d)
        {
            throw new DamageDisperserException(
                "Invalid value passed",
                new ArgumentException("Damage coefficients should be in [0;1] interval"));
        }

        _firstDamageBoundCoefficient = firstDamageBoundCoefficient;
        _secondDamageBoundCoefficient = secondDamageBoundCoefficient;
    }

    public double DisperseCoefficient(double oneTimeDamage)
    {
        if (oneTimeDamage <= FirstDamageBound) return _firstDamageBoundCoefficient;
        if (oneTimeDamage <= SecondDamageBound) return _secondDamageBoundCoefficient;
        return OverBoundCoefficient;
    }
}