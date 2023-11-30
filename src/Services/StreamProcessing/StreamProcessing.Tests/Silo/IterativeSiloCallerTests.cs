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

public class IterativeSiloCallerTests
{
    private readonly IIterativeSiloCaller _sut;
    private readonly IGrainFactory _grainFactory;

    public IterativeSiloCallerTests()
    {
        _grainFactory = Substitute.For<IGrainFactory>();
        _sut = new IterativeSiloCaller(_grainFactory);
    }

    [Fact]
    public async Task Start_ShouldCallExpectedLocalGrain_WhenStartCallsMoreThanSiloCounts()
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

        var callCount = 5;

        // Act
        for (int i = 0; i < callCount; i++)
        {
            await _sut.Start(startingPluginType, pluginExecutionContext, default!);
        }
        
        // Assert
        _grainFactory.ReceivedWithAnyArgs(callCount).GetGrain<ILocalSiloGrain>(Guid.Empty);

        var remainder = callCount % localSiloGrains.Count;
        for (int i = 0; i < localSiloGrains.Count; i++)
        {
            int expectedCallCount = callCount / localSiloGrains.Count + (remainder == 0 ? 0 : (i < remainder ? 1 : 0));
            var localSiloGrain = localSiloGrains[i];
            await localSiloGrain.Received(expectedCallCount).StartPlugin(startingPluginType, pluginExecutionContext, Arg.Any<GrainCancellationToken>());
        }
    }
    
    [Fact]
    public async Task Start_ShouldCallExpectedLocalGrain_WhenStartCallsLessThanSiloCounts()
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

        var callCount = 1;

        // Act
        for (int i = 0; i < callCount; i++)
        {
            await _sut.Start(startingPluginType, pluginExecutionContext, default!);
        }

        // Assert
        _grainFactory.ReceivedWithAnyArgs(1).GetGrain<ILocalSiloGrain>(Guid.Empty);

        for (int i = 0; i < callCount; i++)
        {
            var localSiloGrain = localSiloGrains[i];
            await localSiloGrain.Received(1).StartPlugin(startingPluginType, pluginExecutionContext, Arg.Any<GrainCancellationToken>());
        }
        
        for (int i = callCount; i < localSiloGrains.Count; i++)
        {
            var localSiloGrain = localSiloGrains[i];
            await localSiloGrain.DidNotReceiveWithAnyArgs().StartPlugin(default!, default, default!);
        }
    }
}