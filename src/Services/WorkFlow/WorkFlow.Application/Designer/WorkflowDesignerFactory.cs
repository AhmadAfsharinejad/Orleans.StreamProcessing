using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Designer.Interfaces;

namespace Workflow.Application.Designer;

internal sealed class WorkflowDesignerFactory : IWorkflowDesignerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public WorkflowDesignerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
    
    public IWorkflowDesigner Create()
    {
        return _serviceProvider.GetRequiredService<IWorkflowDesigner>();
    }
}