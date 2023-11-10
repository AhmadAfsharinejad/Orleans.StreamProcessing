using Mediator;
using Workflow.Application.Coordinator.Interfaces;

namespace Workflow.Application.Coordinator.Commands.AddPlugin;

public sealed class AddPluginHandler : IRequestHandler<AddPluginConfig, Unit>
{
    private readonly IWorkflowCoordinator _workflowCoordinator;

    public AddPluginHandler(IWorkflowCoordinator workflowCoordinator)
    {
        _workflowCoordinator = workflowCoordinator ?? throw new ArgumentNullException(nameof(workflowCoordinator));
    }
    
    public ValueTask<Unit> Handle(AddPluginConfig request, CancellationToken cancellationToken)
    {
        var designer = _workflowCoordinator.GetDesigner(request.WorkflowId);
        
        return Unit.ValueTask;
    }
}