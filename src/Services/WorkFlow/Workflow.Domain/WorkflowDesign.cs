﻿namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public sealed record WorkflowDesign
{
    public WorkflowDesign(WorkflowId Id, PluginAndLinks PluginAndLinks)
    {
        this.Id = Id;
        this.PluginAndLinks = PluginAndLinks;
    }
    
    [Id(0)]
    public WorkflowId Id { get; }
    
    [Id(1)]
    public PluginAndLinks PluginAndLinks { get; }
}