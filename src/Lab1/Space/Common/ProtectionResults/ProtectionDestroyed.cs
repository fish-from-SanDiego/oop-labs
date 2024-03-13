using Itmo.ObjectOrientedProgramming.Lab1.Space.Models;

namespace Itmo.ObjectOrientedProgramming.Lab1.Space.Common.ProtectionResults;

public sealed record ProtectionDestroyed(DamageExcess ExcessiveDamage) : ProtectionResult;