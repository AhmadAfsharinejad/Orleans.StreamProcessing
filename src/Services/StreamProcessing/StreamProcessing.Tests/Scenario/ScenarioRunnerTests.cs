using System;
using NSubstitute;
using Orleans;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.Scenario;
using StreamProcessing.Scenario.Interfaces;
using StreamProcessing.Tests.PluginCommon.Logic.Mock;
using Workflow.Domain;
using Xunit;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace StreamProcessing.Tests.Scenario;

public class ScenarioRunnerTests
{
    private readonly IScenarioRunner _sut;
    private readonly IGrainFactory _grainFactory;
    private readonly IPluginGrainFactory _pluginGrainFactory;

    public ScenarioRunnerTests()
    {
        _grainFactory = Substitute.For<IGrainFactory>();
        _pluginGrainFactory = Substitute.For<IPluginGrainFactory>();
        _sut = new ScenarioRunner(_grainFactory, _pluginGrainFactory);
    }

    [Fact]
    public void Run_ShouldCallStart_WhenPluginIsSource()
    {
        //Arrange
        var source1 = new Plugin(new PluginTypeId("source"), new PluginId(Guid.NewGuid()), new MockStreamPluginConfig());
        var source2 = new Plugin(new PluginTypeId("source2"), new PluginId(Guid.NewGuid()), new MockStreamPluginConfig());
        var middle = new Plugin(new PluginTypeId("middle"), new PluginId(Guid.NewGuid()), new MockStreamPluginConfig());
        var target = new Plugin(new PluginTypeId("target"), new PluginId(Guid.NewGuid()), new MockStreamPluginConfig());

        var config = new WorkflowDesign(
             new WorkflowId(Guid.NewGuid()),
            new PluginAndLinks(new[] { source1, source2, middle, target },
                new[]
                {
                    new Link(new LinkId(), new PluginIdWithPort(source1.Id, new PortId()),
                        new PluginIdWithPort(middle.Id, new PortId())),
                    new Link(new LinkId(), new PluginIdWithPort(middle.Id, new PortId()),
                        new PluginIdWithPort(target.Id, new PortId())),
                    new Link(new LinkId(), new PluginIdWithPort(source2.Id, new PortId()),
                        new PluginIdWithPort(target.Id, new PortId())),
                }));

        var sourcePlugin1 = Substitute.For<ISourcePluginGrain>();
        _pluginGrainFactory.GetOrCreateSourcePlugin(source1.PluginTypeId, source1.Id).Returns(sourcePlugin1);

        var sourcePlugin2 = Substitute.For<ISourcePluginGrain>();
        _pluginGrainFactory.GetOrCreateSourcePlugin(source2.PluginTypeId, source2.Id).Returns(sourcePlugin2);

        //Act
        _sut.Run(config);

        //Assert
        _pluginGrainFactory.ReceivedWithAnyArgs(2).GetOrCreateSourcePlugin(new PluginTypeId(), Guid.Empty);
        sourcePlugin1.Received(1).Start(new PluginExecutionContext(config.Id, source1.Id, null), Arg.Any<GrainCancellationToken>());
        sourcePlugin2.Received(1).Start(new PluginExecutionContext(config.Id, source2.Id, null), Arg.Any<GrainCancellationToken>());
    }
}