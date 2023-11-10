using Workflow.Application.Designer.Domain;
using Workflow.Application.Designer.Interfaces;

namespace Workflow.Application.DesignCoordinator.Interfaces;

public interface IWorkflowDesignCoordinator
{
    void Create(WorkflowId id);
    IWorkflowDesigner GetDesigner(WorkflowId id);
}