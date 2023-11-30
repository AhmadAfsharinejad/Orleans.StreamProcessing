using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.DesignCoordinator.Interfaces;
using Workflow.Application.Di;

namespace Workflow.Application.DesignCoordinator;

internal sealed class WorkflowDesignCoordinatorDiAdder : IServiceAdder
{
    public void AddService(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IWorkflowDesignCoordinator, WorkflowDesignCoordinator>();
    }
}