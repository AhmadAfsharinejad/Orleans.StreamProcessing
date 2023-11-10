using Workflow.Domain;

namespace Workflow.Application.Coordinator.Interfaces;

public interface IWorkflowCoordinator
{
    void Create(WorkflowId id);
}