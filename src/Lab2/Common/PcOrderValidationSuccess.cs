using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Common;

public sealed record PcOrderValidationSuccess(IPcOrder Order) : PcOrderValidationResult(Order);