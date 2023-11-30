using Mediator;
using Workflow.Application.DesignCoordinator.Interfaces;

namespace Workflow.Application.DesignCoordinator.Commands.AddPlugin;

public sealed class AddPluginHandler : IRequestHandler<AddPluginCommandConfig, Unit>
{
    private readonly IWorkflowDesignCoordinator _workflowDesignCoordinator;

    public AddPluginHandler(IWorkflowDesignCoordinator workflowDesignCoordinator)
    {
        _workflowDesignCoordinator = workflowDesignCoordinator ?? throw new ArgumentNullException(nameof(workflowDesignCoordinator));
    }
    
    public ValueTask<Unit> Handle(AddPluginCommandConfig request, CancellationToken cancellationToken)
    {
        var designer = _workflowDesignCoordinator.GetDesigner(request.WorkflowId);
        designer.AddPlugin(request.PluginTypeId, request.PluginId);
        
        return Unit.ValueTask;
    }
}