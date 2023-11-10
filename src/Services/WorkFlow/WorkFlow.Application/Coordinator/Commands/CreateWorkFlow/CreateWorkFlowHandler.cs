using Mediator;
using Workflow.Application.Coordinator.Interfaces;

namespace Workflow.Application.Coordinator.Commands.CreateWorkflow;

public sealed class CreateWorkflowHandler : IRequestHandler<CreateWorkflowConfig, Unit>
{
    private readonly IWorkflowCoordinator _workflowCoordinator;

    public CreateWorkflowHandler(IWorkflowCoordinator workflowCoordinator)
    {
        _workflowCoordinator = workflowCoordinator ?? throw new ArgumentNullException(nameof(workflowCoordinator));
    }
    
    public ValueTask<Unit> Handle(CreateWorkflowConfig request, CancellationToken cancellationToken)
    {
        _workflowCoordinator.Created(request.Id);
        
        return Unit.ValueTask;
    }
}