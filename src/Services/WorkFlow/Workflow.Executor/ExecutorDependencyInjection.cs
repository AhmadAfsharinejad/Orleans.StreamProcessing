using Microsoft.Extensions.DependencyInjection;
using Workflow.Infrastructure.Executer.Interfaces;

namespace Workflow.Executor;

public static class ExecutorDependencyInjection
{
    public static IServiceCollection AddExecutionServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IWorkflowExecutor, WorkflowExecutor>();
        return serviceCollection;
    }
}