using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Di;
using Workflow.Application.Executer.Interfaces;

namespace Workflow.Application.Executer;

internal sealed class WorkflowExecutorDiAdder : IServiceAdder
{
    public void AddService(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IWorkflowExecutorFactory, WorkflowExecutorFactory>();
    }
}