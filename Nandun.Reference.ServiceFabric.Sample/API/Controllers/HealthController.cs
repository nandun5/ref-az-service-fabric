using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Nandun.Reference.ServiceFabric.Sample.API.Controllers;

/// <summary>
/// Provides a collection of endpoints to perform health checks on dependencies that requires for Transaction Sample API.
/// </summary>
[ApiController]
[Produces("application/json")]
[Route("Reference/Sample-api/v1/[controller]")]
public class HealthController : ControllerBase
{
    private readonly HealthCheckService _health;

    public HealthController(HealthCheckService health)
        => _health = health;

    [HttpGet]
    public async Task<IActionResult> GetHealthAsync()
    {
        return Ok();
    }
}

