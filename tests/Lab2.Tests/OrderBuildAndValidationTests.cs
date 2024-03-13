using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Common;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.PcComponents.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.BiosTypes;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Connections;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Ddr;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Dimensions;
using Itmo.ObjectOrientedProgramming.Lab2.Models.FormFactors;
using Itmo.ObjectOrientedProgramming.Lab2.Services;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

[SuppressMessage(
    "StyleCop.CSharp.OrderingRules",
    "SA1204:Static elements should appear before instance elements",
    Justification = "TestCases")]
public class OrderBuildAndValidationTests
{
    private static readonly IPcOrderValidationService _sut;
    private static readonly IPcComponentRepository _repository;
    private static readonly ICentralProcessorBuilder _cpuBuilder;
    private static readonly IBiosBuilder _biosBuilder;
    private static readonly IMotherBoardBuilder _motherBoardBuilder;
    private static readonly IRandomAccessMemoryBuilder _memoryBuilder;
    private static readonly IGraphicsCardBuilder _graphicsCardBuilder;
    private static readonly ISolidStateDriveBuilder _solidStateDriveBuilder;
    private static readonly ICpuCoolingSystemBuilder _cpuCoolingSystemBuilder;
    private static readonly IPowerSupplyBuilder _powerSupplyBuilder;
    private static readonly IPcCaseBuilder _pcCaseBuilder;

#pragma warning disable CA1810
    static OrderBuildAndValidationTests()
#pragma warning restore CA1810
    {
        _cpuBuilder = new CentralProcessorBuilder();
        _biosBuilder = new BiosBuilder();
        _motherBoardBuilder = new MotherBoardBuilder();
        _memoryBuilder = new RandomAccessMemoryBuilder();
        _graphicsCardBuilder = new GraphicsCardBuilder();
        _solidStateDriveBuilder = new SolidStateDriveBuilder();
        _cpuCoolingSystemBuilder = new CpuCoolingSystemBuilder();
        _powerSupplyBuilder = new PowerSupplyBuilder();
        _pcCaseBuilder = new PcCaseBuilder();
        _sut = new PcOrderValidationService();
        _repository = new PcComponentRepository();

        _repository.RegisterCentralProcessor(_cpuBuilder
            .WithName("Intel Core i5-11400F")
            .WithCores(new CpuCores(6, Frequency.FromGigaHertz(2.6)))
            .WithSocket(new CpuSocket("LGA 1200"))
            .WithoutIntegratedVideoCore()
            .WithMaxSupportedRamFrequency(Frequency.FromMegaHertz(3200))
            .WithThermalDesignPower(new Power(65))
            .WithPowerConsumption(new Power(65))
            .Build());

        _repository.RegisterBios(_biosBuilder
            .WithName("Bios 1")
            .WithVersion(new TechnologyVersion("1.0"))
            .WithBiosType(new UefiBios())
            .WithProcessors(_repository.CentralProcessors)
            .Build());

        _repository.RegisterMotherBoard(_motherBoardBuilder
            .WithName("Biostar H510MHP 2.0")
            .WithSocket(new CpuSocket("LGA 1200"))
            .WithDdrStandard(new Ddr4())
            .WithFormFactor(new MicroAtxFactor())
            .WithSupportedMemoryFormFactor(new DimmFactor())
            .SetPciEInfo(PciEConnection.PciEx16, new Slots(1))
            .SetPciEInfo(PciEConnection.PciEx1, new Slots(2))
            .WithSataPorts(new Slots(4))
            .AddJedecFrequency(Frequency.FromMegaHertz(3200))
            .AddJedecFrequency(Frequency.FromMegaHertz(2666))
            .WithRamSlots(new Slots(2))
            .WithBios(_repository.BiosCollection.First())
            .Build());

        _repository.RegisterRandomAccessMemory(_memoryBuilder
            .WithName("DEXP4GD4UD26")
            .WithFormFactor(new DimmFactor())
            .WithDdrStandard(new Ddr4())
            .SetJedecFrequencyInfo(Frequency.FromMegaHertz(2666), new Voltage(1.2))
            .WithMemoryCapacity(new MemoryCapacity(4))
            .WithPowerConsumption(new Power(10))
            .Build());

        _repository.RegisterGraphicsCard(_graphicsCardBuilder
            .WithName("KFA2 GeForce 210")
            .WithChipFrequency(Frequency.FromMegaHertz(520))
            .WithConnection(PciEConnection.PciEx16)
            .WithPowerConsumption(new Power(120))
            .WithVideoMemoryCapacity(new MemoryCapacity(1))
            .WithSize(new TwoDimensions(70, 168))
            .Build());

        _repository.RegisterSolidStateDrive(_solidStateDriveBuilder
            .WithName("AGI AI138")
            .WithMemoryReadMaxSpeed(new MemoryTransferSpeed(510))
            .WithMemoryWriteMaxSpeed(new MemoryTransferSpeed(500))
            .WithCapacity(new MemoryCapacity(120))
            .WithConnection(new SataConnection())
            .WithPowerConsumption(new Power(20))
            .Build());

        _repository.RegisterCpuCoolingSystem(_cpuCoolingSystemBuilder
            .WithName("ID-COOLING DK-01")
            .WithMaxHeatDissipation(new Power(95))
            .AddSupportedSocket(new CpuSocket("LGA 1200"))
            .AddSupportedSocket(new CpuSocket("LGA 1156"))
            .AddSupportedSocket(new CpuSocket("AM2"))
            .WithSize(new TwoDimensions(52, 110))
            .Build());

        _repository.RegisterPowerSupply(_powerSupplyBuilder
            .WithName("ExeGate AAA400")
            .WithPeakLoad(new Power(400))
            .Build());

        _repository.RegisterPcCase(_pcCaseBuilder
            .WithName("DEXP DC-201M")
            .WithMotherBoardFormFactors(new MotherBoardFormFactor[] { new MicroAtxFactor(), new MiniItxFactor() })
            .WithCoolingSystemMaxSize(new OneDimension(142))
            .WithGraphicsCardMaxSize(new TwoDimensions(150, 300))
            .WithSize(new ThreeDimensions(360, 175, 360))
            .Build());

        _repository.RegisterPowerSupply(_repository.PowerSupplies.First().Direct(new PowerSupplyBuilder())
            .WithPeakLoad(new Power(210))
            .WithName("ExeGate AAA400 for poor people")
            .Build());

        _repository.RegisterCpuCoolingSystem(_repository.CpuCoolingSystems.First().Direct(new CpuCoolingSystemBuilder())
            .WithMaxHeatDissipation(new Power(60))
            .WithName("ID-COOLING DK-01 (actually AEROCOOL)")
            .Build());

        _repository.RegisterGraphicsCard(_repository.GraphicsCards.First().Direct(new GraphicsCardBuilder())
            .WithSize(new TwoDimensions(2000, 500))
            .WithName("KFA2 RTX 5090")
            .Build());

        _repository.RegisterCentralProcessor(_repository.CentralProcessors.First().Direct(new CentralProcessorBuilder())
            .WithName("Weak Intel Core i5-11400F")
            .WithMaxSupportedRamFrequency(new Frequency(1000))
            .Build());

        _repository.RegisterBios(_biosBuilder
            .WithName("Good Bios (supports weak processors)")
            .WithVersion(new TechnologyVersion("0.1"))
            .WithBiosType(new AmiBios())
            .ClearProcessors()
            .AddProcessor(_repository.CentralProcessors.First(i => i.Name == "Weak Intel Core i5-11400F"))
            .Build());
    }

    [Fact]
    public void ShouldValidateValidPcBuildOrder()
    {
        // Arrange
        IPcOrder order = StandardPc();

        // Act
        PcOrderValidationResult result = _sut.Validate(order);

        // Assert
        Assert.IsType<PcOrderValidationSuccess>(result);
    }

    [Fact]
    public void ShouldMakeNotesAboutPowerConsumption()
    {
        // Arrange
        IPcOrderBuilder builder = StandardPc().Direct(new PcOrderBuilder());
        IPcOrder order = builder.WithPowerSupply(_repository.PowerSupplies.First(i => i.Name == "ExeGate AAA400 for poor people"))
            .Build();

        // Act
        PcOrderValidationResult result = _sut.Validate(order);

        // Assert
        Assert.IsType<PcOrderImportantNote>(result);
    }

    [Fact]
    public void ShouldCancelWarrantyWhenTdpIsHigherThanHeatDissipation()
    {
        // Arrange
        IPcOrderBuilder builder = StandardPc().Direct(new PcOrderBuilder());
        IPcOrder order = builder
            .WithCpuCoolingSystem(_repository.CpuCoolingSystems.First(i => i.Name == "ID-COOLING DK-01 (actually AEROCOOL)"))
            .Build();

        // Act
        PcOrderValidationResult result = _sut.Validate(order);

        // Assert
        Assert.IsType<PcOrderWarrantyCancellation>(result);
    }

    [Theory]
    [MemberData(nameof(GenerateValidationIncompatibleBuild))]
    public void ShouldNotPassWhenBuildIsIncompatible(IPcOrderBuilder builder, Type expectedResultType)
    {
        // Arrange
        IPcOrder? order = builder?.Build();

        // Act
        PcOrderValidationResult result = _sut.Validate(order ?? throw new InvalidOperationException());

        // Assert
        Assert.IsType(expectedResultType, result);
    }

    public static IEnumerable<object[]> GenerateValidationIncompatibleBuild()
    {
        // Test case 1
        yield return new object[]
        {
            StandardPc().Direct(new PcOrderBuilder()).ClearGraphicsCards()
                .AddGraphicsCard(_repository.GraphicsCards.First(i => i.Name == "KFA2 RTX 5090")),
            typeof(PcOrderIncompatibleBuild),
        };

        // Test case 2
        yield return new object[]
        {
            StandardPc().Direct(new PcOrderBuilder())
                .AddSolidStateDrive(_repository.SolidStateDrives.First())
                .AddSolidStateDrive(_repository.SolidStateDrives.First())
                .AddSolidStateDrive(_repository.SolidStateDrives.First())
                .AddSolidStateDrive(_repository.SolidStateDrives.First())
                .AddSolidStateDrive(_repository.SolidStateDrives.First())
                .AddSolidStateDrive(_repository.SolidStateDrives.First()),
            typeof(PcOrderIncompatibleBuild),
        };

        // Test case 3
        yield return new object[]
        {
            StandardPc().Direct(new PcOrderBuilder())
                .WithCentralProcessor(_repository.CentralProcessors.First(i => i.Name == "Weak Intel Core i5-11400F")),
            typeof(PcOrderIncompatibleBuild),
        };

        // Test case 4
        yield return new object[]
        {
            StandardPc().Direct(new PcOrderBuilder())
                .WithMotherBoard(_repository.MotherBoards.First().Direct(new MotherBoardBuilder())
                    .WithBios(_repository.BiosCollection.First(i => i.Name == "Good Bios (supports weak processors)")).Build()),
            typeof(PcOrderIncompatibleBuild),
        };
    }

    private static IPcOrder StandardPc()
    {
        return new PcOrderBuilder()
            .WithCentralProcessor(_repository.CentralProcessors.First(i => i.Name == "Intel Core i5-11400F").Clone())
            .AddGraphicsCard(_repository.GraphicsCards.First(i => i.Name == "KFA2 GeForce 210").Clone())
            .WithMotherBoard(_repository.MotherBoards.First(i => i.Name == "Biostar H510MHP 2.0").Clone())
            .WithPowerSupply(_repository.PowerSupplies.First(i => i.Name == "ExeGate AAA400").Clone())
            .AddSolidStateDrive(_repository.SolidStateDrives.First(i => i.Name == "AGI AI138").Clone())
            .AddRamComponent(_repository.RamComponents.First(i => i.Name == "DEXP4GD4UD26").Clone())
            .WithPcCase(_repository.PcCases.First(i => i.Name == "DEXP DC-201M").Clone())
            .WithCpuCoolingSystem(_repository.CpuCoolingSystems.First(i => i.Name == "ID-COOLING DK-01").Clone()).Build();
    }
}