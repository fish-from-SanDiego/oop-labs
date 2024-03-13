using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;
using Itmo.ObjectOrientedProgramming.Lab2.Models.FormFactors;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface IPcCaseBuilder : IPcComponentBuilder<PcCase>
{
    IPcCaseBuilder WithName(string name);
    IPcCaseBuilder WithGraphicsCardMaxSize(TwoDimensions size);
    IPcCaseBuilder WithCoolingSystemMaxSize(OneDimension size);
    IPcCaseBuilder AddMotherBoardFormFactor(MotherBoardFormFactor formFactor);
    IPcCaseBuilder ClearMotherBoardFormFactors();
    IPcCaseBuilder WithMotherBoardFormFactors(IEnumerable<MotherBoardFormFactor> formFactors);
    IPcCaseBuilder WithSize(ThreeDimensions size);
}