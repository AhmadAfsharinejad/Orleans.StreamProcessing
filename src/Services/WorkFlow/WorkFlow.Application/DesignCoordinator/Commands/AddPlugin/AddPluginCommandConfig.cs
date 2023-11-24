using Mediator;
using Workflow.Domain;

namespace Workflow.Application.DesignCoordinator.Commands.AddPlugin;

public record struct AddPluginCommandConfig(WorkflowId WorkflowId, PluginTypeId PluginTypeId, PluginId PluginId) : IRequest;