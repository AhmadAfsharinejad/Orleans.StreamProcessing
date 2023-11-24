using Mediator;
using Workflow.Application.DesignCoordinator.Interfaces;

namespace Workflow.Application.DesignCoordinator.Commands.SetPluginConfig;

public sealed class SetPluginConfigHandler : IRequestHandler<SetPluginConfigCommandConfig, Unit>
{
    private readonly IWorkflowDesignCoordinator _workflowDesignCoordinator;

    public SetPluginConfigHandler(IWorkflowDesignCoordinator workflowDesignCoordinator)
    {
        _workflowDesignCoordinator = workflowDesignCoordinator ?? throw new ArgumentNullException(nameof(workflowDesignCoordinator));
    }
    
    public ValueTask<Unit> Handle(SetPluginConfigCommandConfig request, CancellationToken cancellationToken)
    {
        var designer = _workflowDesignCoordinator.GetDesigner(request.WorkflowId);
        designer.SetPluginConfig(request.pluginId, request.Config);
        
        return Unit.ValueTask;
    }
}