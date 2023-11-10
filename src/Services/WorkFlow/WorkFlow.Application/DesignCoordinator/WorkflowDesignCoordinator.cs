using Workflow.Application.DesignCoordinator.Interfaces;
using Workflow.Application.Designer;
using Workflow.Application.Designer.Domain;
using Workflow.Application.Designer.Interfaces;

// ReSharper disable InconsistentlySynchronizedField

namespace Workflow.Application.DesignCoordinator;

internal sealed class WorkflowDesignCoordinator : IWorkflowDesignCoordinator
{
    private readonly Dictionary<WorkflowId, WorkflowDesigner> _designers;

    public WorkflowDesignCoordinator()
    {
        _designers = new();
    }
    
    public void Create(WorkflowId id)
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