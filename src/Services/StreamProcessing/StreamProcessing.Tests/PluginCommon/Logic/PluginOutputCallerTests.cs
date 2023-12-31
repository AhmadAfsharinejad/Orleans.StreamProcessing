﻿using System;
using System.Collections.Generic;
using NSubstitute;
using Orleans;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.PluginCommon.Logic;
using StreamProcessing.WorkFlow.Interfaces;
using Workflow.Domain;
using Xunit;

namespace StreamProcessing.Tests.PluginCommon.Logic;

public class PluginOutputCallerTests
{
    private readonly IPluginOutputCaller _sut;
    private readonly IGrainFactory _grainFactory;
    private readonly IPluginGrainFactory _pluginGrainFactory;

    public PluginOutputCallerTests()
    {
        _grainFactory = Substitute.For<IGrainFactory>();
        _pluginGrainFactory = Substitute.For<IPluginGrainFactory>();
        _sut = new PluginOutputCaller(_grainFactory, _pluginGrainFactory);
    }

    [Fact]
    public void CallOutputsWithSingleRecordArgument_ShouldCallGetGrainOneTime_WhenAll()
    {
        //Arrange
        var pluginContext = new PluginExecutionContext
        {
            PluginId = new PluginId(Guid.NewGuid()),
            WorkFlowId = new WorkflowId(Guid.NewGuid())
        };
        var record = new PluginRecord();

        //Act
        _sut.CallOutputs(pluginContext, record, default!);
        _sut.CallOutputs(pluginContext, record, default!);

        //Assert
        _grainFactory.Received(1).GetGrain<IWorkflowGrain>(pluginContext.WorkFlowId);
        _grainFactory.ReceivedWithAnyArgs(1).GetGrain<IWorkflowGrain>(pluginContext.WorkFlowId);
    }

    [Fact]
    public void CallOutputsWithListRecordArgument_ShouldCallGetGrainOneTime_WhenAll()
    {
        //Arrange
        var pluginContext = new PluginExecutionContext
        {
            PluginId = new PluginId(Guid.NewGuid()),
            WorkFlowId = new WorkflowId(Guid.NewGuid())
        };
        var records = new List<PluginRecord>();

        //Act
        _sut.CallOutputs(pluginContext, records, default!);

        //Assert
        _grainFactory.Received(1).GetGrain<IWorkflowGrain>(pluginContext.WorkFlowId);
        _grainFactory.ReceivedWithAnyArgs(1).GetGrain<IWorkflowGrain>(pluginContext.WorkFlowId);
    }

    [Fact]
    public void CallOutputsWithSingleRecordArgument_ShouldDontCallGetOrCreatePlugin_WhenThereIsNoOutput()
    {
        //Arrange
        var pluginContext = new PluginExecutionContext
        {
            PluginId = new PluginId(Guid.NewGuid()),
            WorkFlowId = new WorkflowId(Guid.NewGuid())
        };
        var record = new PluginRecord();

        //Act
        _sut.CallOutputs(pluginContext, record, default!);
        _sut.CallOutputs(pluginContext, record, default!);

        //Assert
        _pluginGrainFactory.DidNotReceiveWithAnyArgs().GetOrCreatePlugin(default, default);
    }

    [Fact]
    public void CallOutputsWithListRecordArgument_ShouldDontCallGetOrCreatePlugin_WhenThereIsNoOutput()
    {
        //Arrange
        var pluginContext = new PluginExecutionContext
        {
            PluginId = new PluginId(Guid.NewGuid()),
            WorkFlowId = new WorkflowId(Guid.NewGuid())
        };
        var records = new List<PluginRecord>();

        //Act
        _sut.CallOutputs(pluginContext, records, default!);

        //Assert
        _pluginGrainFactory.DidNotReceiveWithAnyArgs().GetOrCreatePlugin(default, default);
    }

    [Fact]
    public void CallOutputsWithSingleRecordArgument_ShouldCallComputeForEachOutput_WhenThereAreOutput()
    {
        //Arrange
        var pluginContext = new PluginExecutionContext
        {
            PluginId = new PluginId(Guid.NewGuid()),
            WorkFlowId = new WorkflowId(Guid.NewGuid())
        };
        var record = new PluginRecord();

        var workflowGrain = Substitute.For<IWorkflowGrain>();
        _grainFactory.GetGrain<IWorkflowGrain>(pluginContext.WorkFlowId).Returns(workflowGrain);

        var pluginTypes = new List<PluginTypeWithId>
        {
            new( new PluginId(Guid.NewGuid()), new PluginTypeId()),
            new(new PluginId(Guid.NewGuid()), new PluginTypeId())
        };
        workflowGrain.GetOutputTypes(pluginContext.PluginId).Returns(pluginTypes);

        var pluginGrains = new List<IPluginGrain>();
        foreach (var pluginType in pluginTypes)
        {
            var pluginGrain = Substitute.For<IPluginGrain>();
            _pluginGrainFactory.GetOrCreatePlugin(pluginType.PluginTypeId, pluginType.PluginId).Returns(pluginGrain);
            pluginGrains.Add(pluginGrain);
        }

        //Act
        _sut.CallOutputs(pluginContext, record, default!);

        //Assert
        int index = 0;
        foreach (var pluginGrain in pluginGrains)
        {
            var outPluginContext = pluginContext with { PluginId = pluginTypes[index++].PluginId };

            pluginGrain.Received(1).Compute(outPluginContext, record, Arg.Any<GrainCancellationToken>());
        }
    }

    [Fact]
    public void CallOutputsWithListRecordArgument_ShouldCallComputeForEachOutput_WhenThereAreOutput()
    {
        //Arrange
        var pluginContext = new PluginExecutionContext
        {
            PluginId = new PluginId(Guid.NewGuid()),
            WorkFlowId = new WorkflowId(Guid.NewGuid())
        };
        var records = new List<PluginRecord>();

        var workflowGrain = Substitute.For<IWorkflowGrain>();
        _grainFactory.GetGrain<IWorkflowGrain>(pluginContext.WorkFlowId).Returns(workflowGrain);

        var pluginTypes = new List<PluginTypeWithId>
        {
            new(new PluginId(Guid.NewGuid()), new PluginTypeId()),
            new(new PluginId(Guid.NewGuid()), new PluginTypeId())
        };
        workflowGrain.GetOutputTypes(pluginContext.PluginId).Returns(pluginTypes);

        var pluginGrains = new List<IPluginGrain>();
        foreach (var pluginType in pluginTypes)
        {
            var pluginGrain = Substitute.For<IPluginGrain>();
            _pluginGrainFactory.GetOrCreatePlugin(pluginType.PluginTypeId, pluginType.PluginId).Returns(pluginGrain);
            pluginGrains.Add(pluginGrain);
        }
        
        var outputRecords = new PluginRecords { Records = records };

        //Act
        _sut.CallOutputs(pluginContext, records, default!);

        //Assert
        int index = 0;
        foreach (var pluginGrain in pluginGrains)
        {
            var outPluginContext = pluginContext with { PluginId = pluginTypes[index++].PluginId };

            pluginGrain.Received(1).Compute(outPluginContext, outputRecords, Arg.Any<GrainCancellationToken>());
        }
    }
}