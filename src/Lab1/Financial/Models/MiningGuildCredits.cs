using System;
using Itmo.ObjectOrientedProgramming.Lab1.Financial.Common;

namespace Itmo.ObjectOrientedProgramming.Lab1.Financial.Models;

public class MiningGuildCredits : IPaymentEquivalent
{
    public MiningGuildCredits(double amount)
    {
        if (amount < 0)
        {
            throw new MoneyException(
                "Validation error",
                new ArgumentException("Value should be non-negative", nameof(amount)));
        }

        Amount = amount;
    }

    public double Amount { get; }

    public static MiningGuildCredits operator +(
        MiningGuildCredits left,
        MiningGuildCredits right)
    {
        if (left is null) throw new MoneyException("Left argument in addition should be not null");
        if (right is null) throw new MoneyException("Right argument in addition should be not null");
        return new MiningGuildCredits(left.Amount + right.Amount);
    }

    public MiningGuildCredits Add(MiningGuildCredits right)
    {
        return this + right;
    }
}