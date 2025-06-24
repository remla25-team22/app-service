using lib_version;
using Microsoft.AspNetCore.Mvc;
using ModelServiceConnector;
using Prometheus;

namespace app_service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VersionController(IApiClient apiClient) : ControllerBase
{
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
    /// <summary>
    ///     Gets the current version of the model-service API.
    /// </summary>
    /// <response code="200">Current model-service version</response>
    /// <response code="500">Internal server error while accessing version</response>
    [HttpGet("model-service")]
    [ProducesResponseType(typeof(Version), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get_Model_Version()
    {
        var versionResponse = await apiClient.ModelAsync();
        if (versionResponse?.Version == null) return StatusCode(500);

        return Ok(versionResponse.Version);
    }
}