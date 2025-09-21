using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TicketsApiController : ControllerBase
{
    [HttpGet]
    public IActionResult GetByProject(int projectId)
    {
        return Ok(new { Id = 1, Title = "Sample Ticket" });
    }
}
