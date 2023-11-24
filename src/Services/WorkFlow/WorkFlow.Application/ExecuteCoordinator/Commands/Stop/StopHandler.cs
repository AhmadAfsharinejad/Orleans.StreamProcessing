using Mediator;
using Workflow.Application.ExecuteCoordinator.Commands.Run;
using Workflow.Application.ExecuteCoordinator.Interfaces;

namespace Workflow.Application.ExecuteCoordinator.Commands.Stop;

public sealed class StopHandler : IRequestHandler<StopCommandConfig>
{
    private readonly IWorkflowExecuteCoordinator _workflowExecuteCoordinator;

    public StopHandler(IWorkflowExecuteCoordinator workflowExecuteCoordinator)
    {
        _workflowExecuteCoordinator = workflowExecuteCoordinator ?? throw new ArgumentNullException(nameof(workflowExecuteCoordinator));
    }
    
    public ValueTask<Unit> Handle(StopCommandConfig request, CancellationToken cancellationToken)
    {
        var executor = _workflowExecuteCoordinator.GetExecutor(request.Id);
        executor.Stop(request.Id);
        
        return Unit.ValueTask;
    }
}