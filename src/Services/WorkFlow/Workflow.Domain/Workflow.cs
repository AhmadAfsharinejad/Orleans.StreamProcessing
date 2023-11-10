namespace Workflow.Domain;

public sealed record Workflow(WorkflowId Id, PluginAndLinks PluginAndLinks);
