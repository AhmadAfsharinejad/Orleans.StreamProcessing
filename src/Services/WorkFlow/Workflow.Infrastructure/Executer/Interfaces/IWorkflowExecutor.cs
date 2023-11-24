using Workflow.Domain;

namespace Workflow.Infrastructure.Executer.Interfaces;

public interface IWorkflowExecutor
{
    Task Run(WorkflowDesign workflowDesign);
    Task Stop(WorkflowId workflowId);
}