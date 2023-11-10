using Workflow.Application.Executer.Interfaces;
using Workflow.Domain;

namespace Workflow.Application.ExecuteCoordinator.Interfaces;

public interface IWorkflowExecuteCoordinator
{
    void Create(WorkflowId id);
    IWorkflowExecutor GetExecutor(WorkflowId id);
}