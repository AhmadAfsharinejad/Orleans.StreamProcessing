﻿using Mediator;
using Workflow.Application.Coordinator.Interfaces;

namespace Workflow.Application.Coordinator.Commands.CreateWorkflow;

public sealed class CreateWorkflowHandler : IRequestHandler<CreateWorkflowCommandConfig, Unit>
{
    private readonly IWorkflowCoordinator _workflowCoordinator;

    public CreateWorkflowHandler(IWorkflowCoordinator workflowCoordinator)
    {
        _workflowCoordinator = workflowCoordinator ?? throw new ArgumentNullException(nameof(workflowCoordinator));
    }
    
    public ValueTask<Unit> Handle(CreateWorkflowCommandConfig request, CancellationToken cancellationToken)
    {
        _workflowCoordinator.Create(request.Id);
        
        return Unit.ValueTask;
    }
}