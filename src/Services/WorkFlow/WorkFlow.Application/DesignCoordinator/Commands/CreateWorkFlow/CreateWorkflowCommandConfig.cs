using Mediator;
using Workflow.Domain;

namespace Workflow.Application.DesignCoordinator.Commands.CreateWorkFlow;

public record struct CreateWorkflowCommandConfig(WorkflowId Id) : IRequest;
