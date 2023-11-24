using Workflow.Domain;
using Workflow.Infrastructure.Executer.Interfaces;

namespace Workflow.Application.ExecuteCoordinator.Interfaces;

public interface IWorkflowExecuteCoordinator
{
    void Create(WorkflowId id);
    IWorkflowExecutor GetExecutor(WorkflowId id);
}