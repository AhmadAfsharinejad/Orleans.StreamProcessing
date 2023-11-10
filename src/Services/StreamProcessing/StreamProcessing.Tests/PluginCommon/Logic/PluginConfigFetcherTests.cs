using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Orleans;
using StreamProcessing.Common;
using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.PluginCommon.Logic;
using StreamProcessing.Scenario.Interfaces;
using StreamProcessing.Tests.PluginCommon.Logic.Mock;
using Workflow.Domain;
using Xunit;

namespace StreamProcessing.Tests.PluginCommon.Logic;

public class PluginConfigFetcherTests
{
    private readonly IPluginConfigFetcher<MockStreamPluginConfig> _sut;
    private readonly IGrainFactory _grainFactory;

    public PluginConfigFetcherTests()
    {
        _grainFactory = Substitute.For<IGrainFactory>();
        _sut = new PluginConfigFetcher<MockStreamPluginConfig>(_grainFactory);
    }

    [Fact]
    public void GetConfig_ShouldCallGetGrainOneTime_WhenAll()
    {
        //Arrange
        var workflowId = new WorkflowId(Guid.NewGuid());
        var pluginId = new PluginId(Guid.NewGuid());
        
        var scenarioGrain = Substitute.For<IScenarioGrain>();
        _grainFactory.GetGrain<IScenarioGrain>(workflowId).Returns(scenarioGrain);
        scenarioGrain.GetPluginConfig(pluginId).Returns(new ImmutableWrapper<IPluginConfig>(new MockStreamPluginConfig()));
        
        //Act
        _sut.GetConfig(workflowId, pluginId);
        _sut.GetConfig(workflowId, pluginId);

        //Assert
        _grainFactory.Received(1).GetGrain<IScenarioGrain>(workflowId);
        _grainFactory.ReceivedWithAnyArgs(1).GetGrain<IScenarioGrain>(workflowId);
    }
    
    [Fact]
    public void GetConfig_ShouldCallGetPluginConfigOneTime_WhenAll()
    {
        //Arrange
        var workflowId = new WorkflowId(Guid.NewGuid());
        var pluginId = new PluginId(Guid.NewGuid());

        var scenarioGrain = Substitute.For<IScenarioGrain>();
        _grainFactory.GetGrain<IScenarioGrain>(workflowId).Returns(scenarioGrain);
        scenarioGrain.GetPluginConfig(pluginId).Returns(new ImmutableWrapper<IPluginConfig>(new MockStreamPluginConfig()));
        
        //Act
        _sut.GetConfig(workflowId, pluginId);
        _sut.GetConfig(workflowId, pluginId);

        //Assert
        scenarioGrain.Received(1).GetPluginConfig(pluginId);
        scenarioGrain.ReceivedWithAnyArgs(1).GetPluginConfig(default);
    }
    
    [Fact]
    public async Task GetConfig_ShouldThrowException_WhenConfigTypeIsNotSame()
    {
        //Arrange
        var workflowId = new WorkflowId(Guid.NewGuid());
        var pluginId = new PluginId(Guid.NewGuid());

        var scenarioGrain = Substitute.For<IScenarioGrain>();
        _grainFactory.GetGrain<IScenarioGrain>(workflowId).Returns(scenarioGrain);
        scenarioGrain.GetPluginConfig(pluginId).Returns(new ImmutableWrapper<IPluginConfig>(new OtherMockStreamPluginConfig()));
        
        //Act
        var act = async () => await _sut.GetConfig(workflowId, pluginId);

        //Assert
        await act.Should().ThrowAsync<InvalidCastException>();
    }
}