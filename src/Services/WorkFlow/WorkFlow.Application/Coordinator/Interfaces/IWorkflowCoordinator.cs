using Workflow.Application.Designer.Domain;
using Workflow.Application.Designer.Interfaces;

namespace Workflow.Application.Coordinator.Interfaces;

public interface IWorkflowCoordinator
{
    void Created(WorkflowId id);
    IWorkflowDesigner GetDesigner(WorkflowId id);

}