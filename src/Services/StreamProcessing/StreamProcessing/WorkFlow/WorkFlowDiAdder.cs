﻿using Microsoft.Extensions.DependencyInjection;
using StreamProcessing.Di;
using StreamProcessing.WorkFlow.Interfaces;

namespace StreamProcessing.WorkFlow;

internal sealed class WorkFlowDiAdder : IServiceAdder
{
    public void AddService(IServiceCollection collection)
    {
        collection.AddSingleton<IWorkflowRunner, WorkflowRunner>();
    }
}