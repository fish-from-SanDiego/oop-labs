using System;
using Itmo.ObjectOrientedProgramming.Lab2.Common;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record CpuSocket
{
    public CpuSocket(string name)
    {
        ComponentAttributeException.ThrowIfNull(name, nameof(name));
        if (name.Length == 0)
        {
            throw new ComponentAttributeException(
                "Value validation error",
                new ArgumentException("Name should be non-empty", nameof(name)));
        }

        Name = name;
    }

    public string Name { get; }
}