using Workflow.Domain;

namespace Workflow.Infrastructure.Executer.Interfaces;

public interface IWorkflowExecutor
{
    void Run(WorkflowDesign workflowDesign);
    void Stop(WorkflowId workflowId);
}