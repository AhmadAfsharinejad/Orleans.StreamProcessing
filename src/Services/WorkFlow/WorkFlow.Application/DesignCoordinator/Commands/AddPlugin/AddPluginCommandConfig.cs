using Mediator;
using Workflow.Application.Designer.Domain;

namespace Workflow.Application.DesignCoordinator.Commands.AddPlugin;

public sealed record AddPluginCommandConfig(WorkflowId WorkflowId, PluginTypeId PluginTypeId, PluginId PluginId) : IRequest;