using Workflow.Domain;
using Workflow.Infrastructure.Executer.Interfaces;

namespace Workflow.FunctionalTests.Mock;

internal sealed class MockWorkflowExecutor : IWorkflowExecutor
{
    public void Run(WorkflowDesign workflowDesign)
    {
        //Nothing TODO
    }

    public void Stop(WorkflowId workflowId)

    {
        //Nothing TODO
    }
}