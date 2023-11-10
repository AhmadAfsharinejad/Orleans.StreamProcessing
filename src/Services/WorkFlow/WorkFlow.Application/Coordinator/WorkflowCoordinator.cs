using Workflow.Application.Coordinator.Interfaces;
using Workflow.Application.DesignCoordinator.Interfaces;
using Workflow.Application.Designer.Domain;

namespace Workflow.Application.Coordinator;

internal sealed class WorkflowCoordinator : IWorkflowCoordinator
{
    private readonly IWorkflowDesignCoordinator _workflowDesignCoordinator;

    public WorkflowCoordinator(IWorkflowDesignCoordinator workflowDesignCoordinator)
    {
        _workflowDesignCoordinator = workflowDesignCoordinator ?? throw new ArgumentNullException(nameof(workflowDesignCoordinator));
    }
    
    public void Create(WorkflowId id)
    {
        _workflowDesignCoordinator.Create(id);
    }
}