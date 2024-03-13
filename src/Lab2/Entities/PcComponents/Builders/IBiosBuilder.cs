using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.BiosTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface IBiosBuilder : IPcComponentBuilder<Bios>
{
    IBiosBuilder WithName(string name);
    IBiosBuilder WithBiosType(BiosType type);
    IBiosBuilder WithVersion(TechnologyVersion version);
    IBiosBuilder AddProcessor(CentralProcessor processor);
    IBiosBuilder ClearProcessors();
    IBiosBuilder WithProcessors(IEnumerable<CentralProcessor> processors);
}