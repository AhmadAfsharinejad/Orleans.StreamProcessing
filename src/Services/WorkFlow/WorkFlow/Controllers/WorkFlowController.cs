using Microsoft.AspNetCore.Mvc;

namespace WorkFlow.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkFlowController : ControllerBase
{
    private readonly ILogger<WorkFlowController> _logger;

    public WorkFlowController(ILogger<WorkFlowController> logger)
    {
        _logger = logger;
    }
    
    
}