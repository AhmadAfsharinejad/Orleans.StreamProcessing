using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Orleans;
using StreamProcessing.Common;
using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.PluginCommon.Logic;
using StreamProcessing.Tests.PluginCommon.Logic.Mock;
using StreamProcessing.WorkFlow.Interfaces;
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
        
        var workflowGrain = Substitute.For<IWorkflowGrain>();
        _grainFactory.GetGrain<IWorkflowGrain>(workflowId).Returns(workflowGrain);
        workflowGrain.GetPluginConfig(pluginId).Returns(new ImmutableWrapper<IPluginConfig>(new MockStreamPluginConfig()));
        
        //Act
        _sut.GetConfig(workflowId, pluginId);
        _sut.GetConfig(workflowId, pluginId);

        //Assert
        _grainFactory.Received(1).GetGrain<IWorkflowGrain>(workflowId);
        _grainFactory.ReceivedWithAnyArgs(1).GetGrain<IWorkflowGrain>(workflowId);
    }
    
    [Fact]
    public void GetConfig_ShouldCallGetPluginConfigOneTime_WhenAll()
    {
        //Arrange
        var workflowId = new WorkflowId(Guid.NewGuid());
        var pluginId = new PluginId(Guid.NewGuid());

        var workflowGrain = Substitute.For<IWorkflowGrain>();
        _grainFactory.GetGrain<IWorkflowGrain>(workflowId).Returns(workflowGrain);
        workflowGrain.GetPluginConfig(pluginId).Returns(new ImmutableWrapper<IPluginConfig>(new MockStreamPluginConfig()));
        
        //Act
        _sut.GetConfig(workflowId, pluginId);
        _sut.GetConfig(workflowId, pluginId);

        //Assert
        workflowGrain.Received(1).GetPluginConfig(pluginId);
        workflowGrain.ReceivedWithAnyArgs(1).GetPluginConfig(default);
    }
    
    [Fact]
    public async Task GetConfig_ShouldThrowException_WhenConfigTypeIsNotSame()
    {
        //Arrange
        var workflowId = new WorkflowId(Guid.NewGuid());
        var pluginId = new PluginId(Guid.NewGuid());

        var workflowGrain = Substitute.For<IWorkflowGrain>();
        _grainFactory.GetGrain<IWorkflowGrain>(workflowId).Returns(workflowGrain);
        workflowGrain.GetPluginConfig(pluginId).Returns(new ImmutableWrapper<IPluginConfig>(new OtherMockStreamPluginConfig()));
        
        //Act
        var act = async () => await _sut.GetConfig(workflowId, pluginId);

        //Assert
        await act.Should().ThrowAsync<InvalidCastException>();
    }
}