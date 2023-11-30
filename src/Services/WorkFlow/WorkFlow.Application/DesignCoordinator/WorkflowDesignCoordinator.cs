using Workflow.Application.DesignCoordinator.Interfaces;
using Workflow.Application.Designer.Interfaces;
using Workflow.Domain;

// ReSharper disable InconsistentlySynchronizedField

namespace Workflow.Application.DesignCoordinator;

internal sealed class WorkflowDesignCoordinator : IWorkflowDesignCoordinator
{
    private readonly IWorkflowDesignerFactory _workflowDesignerFactory;
    private readonly Dictionary<WorkflowId, IWorkflowDesigner> _designers;

    public WorkflowDesignCoordinator(IWorkflowDesignerFactory workflowDesignerFactory)
    {
        _workflowDesignerFactory = workflowDesignerFactory ?? throw new ArgumentNullException(nameof(workflowDesignerFactory));
        _designers = new();
    }
    
    public void Create(WorkflowId id)
    {
        lock (_designers)
        {
            _designers.Add(id, _workflowDesignerFactory.Create());
        }
    }

    public IWorkflowDesigner GetDesigner(WorkflowId id)
    {
        return _designers[id];
    }
}