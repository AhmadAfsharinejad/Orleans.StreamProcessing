using Mediator;
using Workflow.Application.DesignCoordinator.Interfaces;

namespace Workflow.Application.DesignCoordinator.Commands.RemovePlugin;

public sealed class RemovePluginHandler : IRequestHandler<RemovePluginCommandConfig, Unit>
{
    private readonly IWorkflowDesignCoordinator _workflowDesignCoordinator;

    public RemovePluginHandler(IWorkflowDesignCoordinator workflowDesignCoordinator)
    {
        _workflowDesignCoordinator = workflowDesignCoordinator ?? throw new ArgumentNullException(nameof(workflowDesignCoordinator));
    }
    
    public ValueTask<Unit> Handle(RemovePluginCommandConfig request, CancellationToken cancellationToken)
    {
        var designer = _workflowDesignCoordinator.GetDesigner(request.WorkflowId);
        designer.RemovePlugin(request.PluginId);
        
        return Unit.ValueTask;
    }
}