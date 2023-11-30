using Mediator;
using Workflow.Domain;

namespace Workflow.Application.DesignCoordinator.Commands.SetPluginConfig;

public record struct SetPluginConfigCommandConfig(WorkflowId WorkflowId, PluginId pluginId, IPluginConfig Config) : IRequest;
