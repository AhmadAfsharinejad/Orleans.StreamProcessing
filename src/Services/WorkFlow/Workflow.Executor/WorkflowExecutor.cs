using Workflow.Domain;
using Workflow.Infrastructure.Executer.Interfaces;

namespace Workflow.Executor;

//TODO Handle Parallelism -- mabey using actor?
internal sealed class WorkflowExecutor : IWorkflowExecutor
{
    public void Run(WorkflowDesign workflowDesign)
    {
        throw new NotImplementedException();
    }

    public void Stop(WorkflowId workflowId)
    {
        throw new NotImplementedException();
    }
}