using Workflow.Application.Executer.Interfaces;
using Workflow.Domain;

namespace Workflow.Application.Executer;

//TODO
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