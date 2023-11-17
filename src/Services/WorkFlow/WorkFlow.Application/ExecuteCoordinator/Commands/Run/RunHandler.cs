using Mediator;
using Workflow.Application.ExecuteCoordinator.Interfaces;

namespace Workflow.Application.ExecuteCoordinator.Commands.Run;

public sealed class RunHandler : IRequestHandler<RunCommandConfig>
{
    private readonly IWorkflowExecuteCoordinator _workflowExecuteCoordinator;

    public RunHandler(IWorkflowExecuteCoordinator workflowExecuteCoordinator)
    {
        _workflowExecuteCoordinator = workflowExecuteCoordinator ?? throw new ArgumentNullException(nameof(workflowExecuteCoordinator));
    }
    
    public ValueTask<Unit> Handle(RunCommandConfig request, CancellationToken cancellationToken)
    {
        _workflowExecuteCoordinator.Create(request.Id);
        var executor = _workflowExecuteCoordinator.GetExecutor(request.Id);
        executor.Run();
        
        return Unit.ValueTask;
    }
}