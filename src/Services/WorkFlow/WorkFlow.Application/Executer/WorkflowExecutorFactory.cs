using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Executer.Interfaces;

namespace Workflow.Application.Executer;

internal sealed class WorkflowExecutorFactory : IWorkflowExecutorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public WorkflowExecutorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
    
    public IWorkflowExecutor Create()
    {
        return _serviceProvider.GetRequiredService<IWorkflowExecutor>();
    }
}