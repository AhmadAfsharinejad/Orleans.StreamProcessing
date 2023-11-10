using Workflow.Application.ExecuteCoordinator.Interfaces;
using Workflow.Application.Executer.Interfaces;
using Workflow.Domain;

// ReSharper disable InconsistentlySynchronizedField

namespace Workflow.Application.ExecuteCoordinator;

internal sealed class WorkflowExecuteCoordinator : IWorkflowExecuteCoordinator
{
    private readonly IWorkflowExecutorFactory _workflowExecutorFactory;
    private readonly Dictionary<WorkflowId, IWorkflowExecutor> _executors;

    public WorkflowExecuteCoordinator(IWorkflowExecutorFactory workflowExecutorFactory)
    {
        _workflowExecutorFactory = workflowExecutorFactory ?? throw new ArgumentNullException(nameof(workflowExecutorFactory));
        _executors = new();
    }

    public void Create(WorkflowId id)
    {
        lock (_executors)
        {
            _executors.Add(id, _workflowExecutorFactory.Create());
        }
    }

    public IWorkflowExecutor GetExecutor(WorkflowId id)
    {
        return _executors[id];
    }
}