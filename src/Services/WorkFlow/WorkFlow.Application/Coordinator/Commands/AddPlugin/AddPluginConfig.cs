using Mediator;
using Workflow.Application.Designer.Domain;

namespace Workflow.Application.Coordinator.Commands.AddPlugin;

public sealed record AddPluginConfig(WorkflowId WorkflowId, PluginId PluginIdId) : IRequest;