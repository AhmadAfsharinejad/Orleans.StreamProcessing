using Mediator;
using Workflow.Application.DesignCoordinator.Interfaces;

namespace Workflow.Application.DesignCoordinator.Commands.AddLink;

public sealed class AddLinkHandler : IRequestHandler<AddLinkCommandConfig, Unit>
{
    private readonly IWorkflowDesignCoordinator _workflowDesignCoordinator;

    public AddLinkHandler(IWorkflowDesignCoordinator workflowDesignCoordinator)
    {
        _workflowDesignCoordinator = workflowDesignCoordinator ?? throw new ArgumentNullException(nameof(workflowDesignCoordinator));
    }
    
    public ValueTask<Unit> Handle(AddLinkCommandConfig request, CancellationToken cancellationToken)
    {
        var designer = _workflowDesignCoordinator.GetDesigner(request.WorkflowId);
        designer.AddLink(request.config);
        
        return Unit.ValueTask;
    }
}