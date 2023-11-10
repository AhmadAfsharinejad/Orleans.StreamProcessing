using Mediator;
using Workflow.Domain;

namespace Workflow.Application.ExecuteCoordinator.Commands.Run;

public record struct RunCommandConfig(WorkflowId Id) : IRequest;
