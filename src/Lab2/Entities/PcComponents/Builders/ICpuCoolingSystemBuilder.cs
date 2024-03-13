using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Components;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;

public interface ICpuCoolingSystemBuilder : IPcComponentBuilder<CpuCoolingSystem>
{
    ICpuCoolingSystemBuilder WithName(string name);
    ICpuCoolingSystemBuilder WithSize(TwoDimensions size);
    ICpuCoolingSystemBuilder WithSupportedSockets(IEnumerable<CpuSocket> sockets);
    ICpuCoolingSystemBuilder AddSupportedSocket(CpuSocket socket);
    ICpuCoolingSystemBuilder ClearSupportedSockets();
    ICpuCoolingSystemBuilder WithMaxHeatDissipation(Power heatDissipation);
}