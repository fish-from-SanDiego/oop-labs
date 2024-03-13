using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Common;

public sealed record PcOrderImportantNote
    (IPcOrder Order, IReadOnlyCollection<string> Messages) : PcOrderValidationResult(Order);