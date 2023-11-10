using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Coordinator.Interfaces;
using Workflow.Application.Di;

namespace Workflow.Application.Coordinator;

internal sealed class WorkflowCoordinatorDiAdder : IServiceAdder
{
    public void AddService(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IWorkflowCoordinator, WorkflowCoordinator>();
    }
}