using Mediator;
using Workflow.Domain;

namespace Workflow.Application.DesignCoordinator.Commands.RemovePlugin;

public record struct RemovePluginCommandConfig(WorkflowId WorkflowId, PluginId PluginId) : IRequest;
