using Mediator;
using Workflow.Domain;

namespace Workflow.Application.DesignCoordinator.Queries.GetPluginConfig;

public record struct GetPluginConfigCommandConfig(WorkflowId WorkflowId, PluginId PluginId) : IRequest<IPluginConfig>;