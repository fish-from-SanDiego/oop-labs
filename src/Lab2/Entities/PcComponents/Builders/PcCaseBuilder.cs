using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;
using Itmo.ObjectOrientedProgramming.Lab2.Models.FormFactors;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public class PcCaseBuilder : IPcCaseBuilder
{
    private string? _name;
    private TwoDimensions? _graphicsCardMaxSize;
    private OneDimension? _coolingSystemMaxSize;
    private HashSet<MotherBoardFormFactor>? _supportedMotherBoardFormFactors;
    private ThreeDimensions? _size;

    public PcCase Build()
    {
        if (_graphicsCardMaxSize is null)
            throw new BuilderException(new InvalidOperationException(nameof(_graphicsCardMaxSize)));
        if (_coolingSystemMaxSize is null)
            throw new BuilderException(new InvalidOperationException(nameof(_coolingSystemMaxSize)));
        if (_supportedMotherBoardFormFactors is null || _supportedMotherBoardFormFactors.Count == 0)
            throw new BuilderException(new InvalidOperationException(nameof(_supportedMotherBoardFormFactors)));

        return new PcCase(
            _name ?? throw new BuilderException(new InvalidOperationException(nameof(_name))),
            _graphicsCardMaxSize,
            _coolingSystemMaxSize,
            _supportedMotherBoardFormFactors,
            _size ?? throw new BuilderException(new InvalidOperationException(nameof(_size))));
    }

    public IPcCaseBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public IPcCaseBuilder WithGraphicsCardMaxSize(TwoDimensions size)
    {
        _graphicsCardMaxSize = size;
        return this;
    }

    public IPcCaseBuilder WithCoolingSystemMaxSize(OneDimension size)
    {
        _coolingSystemMaxSize = size;
        return this;
    }

    public IPcCaseBuilder AddMotherBoardFormFactor(MotherBoardFormFactor formFactor)
    {
        _supportedMotherBoardFormFactors ??= new HashSet<MotherBoardFormFactor>();
        _supportedMotherBoardFormFactors.Add(formFactor);
        return this;
    }

    public IPcCaseBuilder ClearMotherBoardFormFactors()
    {
        _supportedMotherBoardFormFactors ??= new HashSet<MotherBoardFormFactor>();
        _supportedMotherBoardFormFactors.Clear();
        return this;
    }

    public IPcCaseBuilder WithMotherBoardFormFactors(IEnumerable<MotherBoardFormFactor> formFactors)
    {
        _supportedMotherBoardFormFactors ??= new HashSet<MotherBoardFormFactor>();
        _supportedMotherBoardFormFactors = formFactors.ToHashSet();
        return this;
    }

    public IPcCaseBuilder WithSize(ThreeDimensions size)
    {
        _size = size;
        return this;
    }
}