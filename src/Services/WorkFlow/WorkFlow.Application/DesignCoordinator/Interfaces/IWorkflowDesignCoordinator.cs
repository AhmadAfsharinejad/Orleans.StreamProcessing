using Workflow.Application.Designer.Interfaces;
using Workflow.Domain;

namespace Workflow.Application.DesignCoordinator.Interfaces;

public interface IWorkflowDesignCoordinator
{
    void Create(WorkflowId id);
    IWorkflowDesigner GetDesigner(WorkflowId id);
}