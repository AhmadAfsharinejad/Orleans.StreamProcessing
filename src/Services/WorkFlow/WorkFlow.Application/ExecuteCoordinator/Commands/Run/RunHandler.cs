using Mediator;
using Workflow.Application.DesignCoordinator.Interfaces;
using Workflow.Application.ExecuteCoordinator.Interfaces;
using Workflow.Domain;

namespace Workflow.Application.ExecuteCoordinator.Commands.Run;

public sealed class RunHandler : IRequestHandler<RunCommandConfig>
{//todo: scenrio persistance is not implemented for now
    private readonly IWorkflowExecuteCoordinator _workflowExecuteCoordinator;
    private readonly IWorkflowDesignCoordinator _workflowDesignCoordinator;

    public RunHandler(IWorkflowExecuteCoordinator workflowExecuteCoordinator,
        IWorkflowDesignCoordinator workflowDesignCoordinator)
    {
        _workflowExecuteCoordinator = workflowExecuteCoordinator ?? throw new ArgumentNullException(nameof(workflowExecuteCoordinator));
        _workflowDesignCoordinator = workflowDesignCoordinator ?? throw new ArgumentNullException(nameof(workflowDesignCoordinator));
    }
    
    public ValueTask<Unit> Handle(RunCommandConfig request, CancellationToken cancellationToken)
    {
        var pluginAndLinks = _workflowDesignCoordinator.GetDesigner(request.Id).GetPluginAndLinks();
        
        _workflowExecuteCoordinator.Create(request.Id);
        var executor = _workflowExecuteCoordinator.GetExecutor(request.Id);

        executor.Run(new WorkflowDesign(request.Id, pluginAndLinks));
        
        return Unit.ValueTask;
    }
}