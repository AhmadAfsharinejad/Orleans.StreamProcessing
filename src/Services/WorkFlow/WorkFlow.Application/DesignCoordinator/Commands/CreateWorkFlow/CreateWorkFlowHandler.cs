using Mediator;
using Workflow.Application.DesignCoordinator.Interfaces;

namespace Workflow.Application.DesignCoordinator.Commands.CreateWorkFlow;

public sealed class CreateWorkflowHandler : IRequestHandler<CreateWorkflowCommandConfig, Unit>
{
    private readonly IWorkflowDesignCoordinator _coordinator;

    public CreateWorkflowHandler(IWorkflowDesignCoordinator coordinator)
    {
        _coordinator = coordinator ?? throw new ArgumentNullException(nameof(coordinator));
    }
    
    public ValueTask<Unit> Handle(CreateWorkflowCommandConfig request, CancellationToken cancellationToken)
    {
        _coordinator.Create(request.Id);
        
        return Unit.ValueTask;
    }
}