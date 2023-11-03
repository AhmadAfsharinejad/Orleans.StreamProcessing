using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Orleans;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Silo;
using StreamProcessing.Silo.Interfaces;
using Xunit;

namespace StreamProcessing.Tests.Silo;

public class EachSiloCallerTests
{
    private readonly IEachSiloCaller _sut;
    private readonly IGrainFactory _grainFactory;

    public EachSiloCallerTests()
    {
        _grainFactory = Substitute.For<IGrainFactory>();
        _sut = new EachSiloCaller(_grainFactory);
    }

    [Fact]
    public async Task Start_ShouldCallEachLoadGrain_WhenAll()
    {
        // Arrange
        var startingPluginType = typeof(EachSiloCallerTests);
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