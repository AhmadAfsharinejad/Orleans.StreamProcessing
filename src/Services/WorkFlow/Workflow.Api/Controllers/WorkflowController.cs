using Mediator;
using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Coordinator.Commands.CreateWorkflow;
using Workflow.Application.Designer.Domain;

namespace Workflow.Api.Controllers;

[ApiController]
[Route("[controller]/[Action]")]
public class WorkflowController : ControllerBase
{
    private readonly ILogger<WorkflowController> _logger;
    private readonly IMediator _mediator;

    public WorkflowController(ILogger<WorkflowController> logger,
        IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task CreateWorkflow(WorkflowId id)
    {
        await _mediator.Send(new CreateWorkflowConfig(id));
    }
}