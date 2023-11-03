using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Orleans;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Silo;
using StreamProcessing.Silo.Interfaces;
using Xunit;

namespace StreamProcessing.Tests.Silo;

public class EverySiloCallerTests
{
    private readonly IEverySiloCaller _sut;
    private readonly IGrainFactory _grainFactory;

    public EverySiloCallerTests()
    {
        _grainFactory = Substitute.For<IGrainFactory>();
        _sut = new EverySiloCaller(_grainFactory);
    }

    [Fact]
    public async Task Start_ShouldCallEveryLoadGrain_WhenAll()
    {
        // Arrange
        var startingPluginType = typeof(EverySiloCallerTests);
        var pluginExecutionContext = new PluginExecutionContext();
        var localGrainCoordinator = Substitute.For<ILocalGrainCoordinator>();
        _grainFactory.GetGrain<ILocalGrainCoordinator>(SiloConsts.CoordinatorGrainId).Returns(localGrainCoordinator);

        var localGrainIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        localGrainCoordinator.GetAllLocalSiloGrainIds().Returns(localGrainIds);

        var localSiloGrains = new List<ILocalSiloGrain>();
        foreach (var localGrainId in localGrainIds)
        {
            var localSiloGrain = Substitute.For<ILocalSiloGrain>();
            _grainFactory.GetGrain<ILocalSiloGrain>(localGrainId).Returns(localSiloGrain);
            localSiloGrains.Add(localSiloGrain);
        }

        // Act
        await _sut.Start(startingPluginType, pluginExecutionContext, default!);

        // Assert
        _grainFactory.ReceivedWithAnyArgs(localSiloGrains.Count).GetGrain<ILocalSiloGrain>(Guid.Empty);

        foreach (var localSiloGrain in localSiloGrains)
        {
            await localSiloGrain.Received(1).StartPlugin(startingPluginType, pluginExecutionContext, Arg.Any<GrainCancellationToken>());
        }
    }
}