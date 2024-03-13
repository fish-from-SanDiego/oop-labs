using System;
using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record Frequency : IComparable<Frequency>
{
    private const double KiloMultiplier = 10e3;
    private const double MegaMultiplier = 10e6;
    private const double GigaMultiplier = 10e9;

    public Frequency(double hertz)
    {
        ComponentAttributeException.ThrowIfNonPositive(hertz, nameof(hertz));

        Hertz = hertz;
    }

    public double Hertz { get; }
    public double KiloHertz => Hertz / KiloMultiplier;
    public double MegaHertz => Hertz / MegaMultiplier;
    public double GigaHertz => Hertz / GigaMultiplier;

    public static bool operator <(Frequency? left, Frequency? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    public static bool operator <=(Frequency? left, Frequency right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(Frequency? left, Frequency right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    public static bool operator >=(Frequency? left, Frequency? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }

    public static Frequency FromKiloHertz(double kiloHertz) =>
        new Frequency(kiloHertz * KiloMultiplier);

    public static Frequency FromMegaHertz(double megaHertz) =>
        new Frequency(megaHertz * MegaMultiplier);

    public static Frequency FromGigaHertz(double gigaHertz) =>
        new Frequency(gigaHertz * GigaMultiplier);

    public int CompareTo(Frequency? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        return Hertz.CompareTo(other.Hertz);
    }
}