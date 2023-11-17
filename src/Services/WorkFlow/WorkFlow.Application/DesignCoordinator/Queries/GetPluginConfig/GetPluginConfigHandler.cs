using Mediator;
using Workflow.Application.DesignCoordinator.Interfaces;
using Workflow.Domain;

namespace Workflow.Application.DesignCoordinator.Queries.GetPluginConfig;

public sealed class GetPluginConfigHandler : IRequestHandler<GetPluginConfigCommandConfig, IPluginConfig>
{
    private readonly IWorkflowDesignCoordinator _workflowDesignCoordinator;

    public GetPluginConfigHandler(IWorkflowDesignCoordinator workflowDesignCoordinator)
    {
        _workflowDesignCoordinator = workflowDesignCoordinator ?? throw new ArgumentNullException(nameof(workflowDesignCoordinator));
    }
    
    public ValueTask<IPluginConfig> Handle(GetPluginConfigCommandConfig request, CancellationToken cancellationToken)
    {
        var designer = _workflowDesignCoordinator.GetDesigner(request.WorkflowId);
        var config = designer.GetPluginConfig(request.PluginId);

        return ValueTask.FromResult(config);
    }
}