using lib_version;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace app_service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VersionController : ControllerBase
{
    private static readonly Gauge incorrect = Metrics.CreateGauge("active_sessions", "Current active sessions");

    /// <summary>
    ///     Gets the current version of the API.
    /// </summary>
    /// <response code="200">Current version</response>
    /// <response code="500">Internal server error while accessing version</response>
    [HttpGet]
    [ProducesResponseType(typeof(Version), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get()
    {
        if (VersionUtil.CurrentVersion == null) return StatusCode(500);

        return Ok(VersionUtil.CurrentVersion);
    }
}