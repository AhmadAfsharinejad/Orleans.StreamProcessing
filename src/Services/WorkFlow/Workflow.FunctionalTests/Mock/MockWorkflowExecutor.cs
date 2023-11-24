using Workflow.Domain;
using Workflow.Infrastructure.Executer.Interfaces;

namespace Workflow.FunctionalTests.Mock;

internal sealed class MockWorkflowExecutor : IWorkflowExecutor
{
    public async Task Run(WorkflowDesign workflowDesign)
    {
        await Task.CompletedTask;
    }

    public async Task Stop(WorkflowId workflowId)
    {
        await Task.CompletedTask;
    }
}