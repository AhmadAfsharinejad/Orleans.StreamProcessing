using Workflow.Application.Coordinator.Interfaces;
using Workflow.Application.Designer;
using Workflow.Application.Designer.Domain;
using Workflow.Application.Designer.Interfaces;

// ReSharper disable InconsistentlySynchronizedField

namespace Workflow.Application.Coordinator;

internal sealed class WorkflowCoordinator : IWorkflowCoordinator
{
    private readonly Dictionary<WorkflowId, WorkflowDesigner> _designers;

    public WorkflowCoordinator()
    {
        _designers = new();
    }
    
    public void Created(WorkflowId id)
    {
        lock (_designers)
        {
            _designers.Add(id, new WorkflowDesigner());
        }
    }

    public IWorkflowDesigner GetDesigner(WorkflowId id)
    {
        return _designers[id];
    }
}