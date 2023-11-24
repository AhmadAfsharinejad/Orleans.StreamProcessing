using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Di;
using Workflow.Application.ExecuteCoordinator.Interfaces;

namespace Workflow.Application.ExecuteCoordinator;

internal sealed class WorkflowExecuteCoordinatorDiAdder : IServiceAdder
{
    public void AddService(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IWorkflowExecuteCoordinator, WorkflowExecuteCoordinator>();
    }
}