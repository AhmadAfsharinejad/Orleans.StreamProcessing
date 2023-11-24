using Mediator;
using Workflow.Domain;

namespace Workflow.Application.ExecuteCoordinator.Commands.Stop;

public record struct StopCommandConfig(WorkflowId Id) : IRequest;
