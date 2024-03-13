using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Common;

public sealed record PcOrderIncompatibleBuild(IPcOrder Order, IReadOnlyCollection<string> Messages) : PcOrderValidationResult(Order);