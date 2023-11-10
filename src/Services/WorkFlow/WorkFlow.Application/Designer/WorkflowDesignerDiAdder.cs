using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Designer.Interfaces;
using Workflow.Application.Di;

namespace Workflow.Application.Designer;

internal sealed class WorkflowDesignerDiAdder : IServiceAdder
{
    public void AddService(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IWorkflowDesigner, WorkflowDesigner>();
    }
}