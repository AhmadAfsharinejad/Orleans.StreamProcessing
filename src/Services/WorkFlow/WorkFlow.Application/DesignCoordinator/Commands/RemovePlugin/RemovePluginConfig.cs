using Mediator;
using Workflow.Application.Designer.Domain;

namespace Workflow.Application.DesignCoordinator.Commands.RemovePlugin;

public sealed record RemovePluginConfig(WorkflowId WorkflowId, PluginId PluginId) : IRequest;
