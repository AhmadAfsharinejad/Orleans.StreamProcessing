using Workflow.Domain;

namespace Workflow.Application.Executer.Interfaces;

public interface IWorkflowExecutor
{
    void Run(WorkflowDesign workflowDesign);
    void Stop(WorkflowId workflowId);
}