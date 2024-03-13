using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Itmo.ObjectOrientedProgramming.Lab1.Financial.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Financial.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Common.RouteSimulationResults;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Entities.Ships;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.FuelTypes;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.Obstacles;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Services;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

[SuppressMessage(
    "StyleCop.CSharp.OrderingRules",
    "SA1204:Static elements should appear before instance elements",
    Justification = "TestCases")]
public class RouteSimulationTests
{
    private readonly IRouteSimulationService _sut;
    private readonly MiningGuildFuelStockExchange _stockExchange;

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public RouteSimulationTests()
    {
        _sut = new RouteSimulationService();
        _stockExchange = new MiningGuildFuelStockExchange();
        _stockExchange.AddRate(new ActivePlasmaFuel(), new MiningGuildCredits(100d));
        _stockExchange.AddRate(new GravitonMatterFuel(), new MiningGuildCredits(450d));
    }

    [Theory]
    [MemberData(nameof(GenerateAugurAndShuttleWithResult))]
    public void ShipsWithoutJumpEngineOrWithSmallJumpDistanceShouldLostInSpace(
        Ship ship,
        Type expectedResultType)
    {
        // Arrange
        var route = new Route(
            new SpaceSection(150d, new HighDensityNebula()),
            new SpaceSection(480d, new HighDensityNebula()),
            new SpaceSection(75d, new HighDensityNebula()));

        // Act
        RouteSimulationResult result = _sut.Run(ship, route, _stockExchange);

        // Assert
        Assert.IsType(expectedResultType, result);
    }

    [Theory]
    [MemberData(nameof(GenerateVaklasAndVaklasWithPhotonicDeflectorsResult))]
    public void PhotonicDeflectorsShouldDefendFromAntimatterFlash(
        Ship ship,
        Type expectedResultType)
    {
        // Arrange
        var route = new Route(
            new SpaceSection(310d, new HighDensityNebula(new AntimatterFlashCluster(1))),
            new SpaceSection(980d, new HighDensityNebula(new AntimatterFlashCluster(2))),
            new SpaceSection(75d, new HighDensityNebula()));

        // Act
        RouteSimulationResult result = _sut.Run(ship, route, _stockExchange);

        // Assert
        Assert.IsType(expectedResultType, result);
    }

    [Theory]
    [MemberData(nameof(GenerateProtectFromSpaceWhaleResult))]
    public void DeflectorsAndAntiNitrinoEmittersShouldProtectFromSpaceWhales(
        Ship ship,
        Type expectedResultType,
        bool hasAdditionalProtection,
        bool shipDestroyed)
    {
        if (ship is null) throw new ArgumentNullException(nameof(ship));

        // Arrange
        var route = new Route(
            new SpaceSection(230d, new NitrinoParticleNebula(new SpaceWhaleCluster(1))));

        // Act
        RouteSimulationResult result = _sut.Run(ship, route, _stockExchange);

        // Assert
        Assert.IsType(expectedResultType, result);
        Assert.Equal(hasAdditionalProtection, ship.HasAdditionalProtection);
        Assert.Equal(shipDestroyed, ship.IsDestroyed);
    }

    [Fact]
    public void ConstantVelocityImpulseEngineShouldBeCheaperThanExponential()
    {
        // Arrange
        var route = new Route(
            new SpaceSection(50d, new RegularSpace()),
            new SpaceSection(70d, new RegularSpace()),
            new SpaceSection(110d, new RegularSpace()));
        var ships = new Ship[] { new ExcursionShuttle(), new Vaklas() };

        // Act
        Ship? optimalShip = _sut.ChooseOptimalShip(ships, route, _stockExchange);

        // Assert
        Assert.NotNull(optimalShip);
        Assert.IsType<ExcursionShuttle>(optimalShip);
    }

    [Fact]
    public void BetweenAlphaAndOmegaJumpEngineOnMediumDistanceShouldBeChosenOmega()
    {
        // Arrange
        var route = new Route(
            new SpaceSection(120d, new HighDensityNebula()),
            new SpaceSection(300d, new HighDensityNebula()),
            new SpaceSection(587d, new HighDensityNebula()),
            new SpaceSection(413d, new HighDensityNebula()));
        var ships = new Ship[] { new Augur(), new Stella() };

        // Act
        Ship? optimalShip = _sut.ChooseOptimalShip(ships, route, _stockExchange);

        // Assert
        Assert.NotNull(optimalShip);
        Assert.IsType<Stella>(optimalShip);
    }

    [Fact]
    public void ShipWithExponentialEngineShouldBeChosenForNitrino()
    {
        // Arrange
        var route = new Route(
            new SpaceSection(120d, new NitrinoParticleNebula()),
            new SpaceSection(200d, new NitrinoParticleNebula()),
            new SpaceSection(170d, new NitrinoParticleNebula()),
            new SpaceSection(10d, new NitrinoParticleNebula()));
        var ships = new Ship[] { new ExcursionShuttle(), new Vaklas() };

        // Act
        Ship? optimalShip = _sut.ChooseOptimalShip(ships, route, _stockExchange);

        // Assert
        Assert.NotNull(optimalShip);
        Assert.IsType<Vaklas>(optimalShip);
    }

    [Fact]
    public void NoneShipShouldPassTooManyAsteroids()
    {
        // Arrange
        var route = new Route(
            new SpaceSection(5d, new RegularSpace(new SmallAsteroidCluster(61))));
        var ships = new Ship[] { new ExcursionShuttle(), new Vaklas(), new Augur(), new Meridian(), new Stella() };

        // Act
        Ship? optimalShip = _sut.ChooseOptimalShip(ships, route, _stockExchange);

        // Assert
        Assert.Null(optimalShip);
    }

    [Fact]
    public void NoneShipShouldPassTooManyMeteorites()
    {
        // Arrange
        var route = new Route(
            new SpaceSection(5d, new RegularSpace(new MeteoriteCluster(16))));
        var ships = new Ship[] { new ExcursionShuttle(), new Vaklas(), new Augur(), new Meridian(), new Stella() };

        // Act
        Ship? optimalShip = _sut.ChooseOptimalShip(ships, route, _stockExchange);

        // Assert
        Assert.Null(optimalShip);
    }

    [Fact]
    public void NoneShipShouldPassTooManyAntiMatterFlashes()
    {
        // Arrange
        var route = new Route(
            new SpaceSection(5d, new HighDensityNebula(new AntimatterFlashCluster(4))));
        var ships = new Ship[]
        {
            new ExcursionShuttle(), new Vaklas(), new Augur(), new Meridian(), new Stella(),
            new AugurWithPhotonicDeflectors(), new VaklasWithPhotonicDeflectors(), new MeridianWithPhotonicDeflectors(),
            new StellaWithPhotonicDeflectors(),
        };

        // Act
        Ship? optimalShip = _sut.ChooseOptimalShip(ships, route, _stockExchange);

        // Assert
        Assert.Null(optimalShip);
    }

    [Fact]
    public void AugurWithPhotonicDeflectorsShouldBeChosenForDifficultRoute()
    {
        // Arrange
        var route = new Route(
            new SpaceSection(120d, new RegularSpace(new MeteoriteCluster(1), new SmallAsteroidCluster(3))),
            new SpaceSection(150d, new HighDensityNebula(new AntimatterFlashCluster(2))),
            new SpaceSection(400d, new NitrinoParticleNebula(new SpaceWhaleCluster(1))),
            new SpaceSection(350d, new RegularSpace(new SmallAsteroidCluster(8), new MeteoriteCluster(1))));
        var ships = new Ship[]
        {
            new ExcursionShuttle(), new VaklasWithPhotonicDeflectors(), new Augur(), new Meridian(), new Stella(),
            new AugurWithPhotonicDeflectors(),
        };

        // Act
        Ship? optimalShip = _sut.ChooseOptimalShip(ships, route, _stockExchange);

        // Assert
        Assert.NotNull(optimalShip);
        Assert.IsType<AugurWithPhotonicDeflectors>(optimalShip);
    }

    public static IEnumerable<object[]> GenerateAugurAndShuttleWithResult()
    {
        // Test case 1
        yield return new object[]
        {
            new Augur(),
            typeof(RouteSimulationLostInSpace),
        };

        // Test case 2
        yield return new object[]
        {
            new ExcursionShuttle(),
            typeof(RouteSimulationLostInSpace),
        };
    }

    public static IEnumerable<object[]> GenerateVaklasAndVaklasWithPhotonicDeflectorsResult()
    {
        // Test case 1
        yield return new object[]
        {
            new Vaklas(),
            typeof(RouteSimulationCrewDead),
        };

        // Test case 2
        yield return new object[]
        {
            new VaklasWithPhotonicDeflectors(),
            typeof(RouteSimulationSuccess),
        };
    }

    public static IEnumerable<object[]> GenerateProtectFromSpaceWhaleResult()
    {
        // Test case 1
        yield return new object[]
        {
            new Vaklas(),
            typeof(RouteSimulationDestroyed),
            false,
            true,
        };

        // Test case 2
        yield return new object[]
        {
            new Augur(),
            typeof(RouteSimulationSuccess),
            false,
            false,
        };

        // Test case 3
        yield return new object[]
        {
            new Meridian(),
            typeof(RouteSimulationSuccess),
            true,
            false,
        };
    }
}