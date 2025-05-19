using app_service.Models;
using Microsoft.AspNetCore.Mvc;
using ModelServiceConnector;

namespace app_service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncorrectPredictionController : ControllerBase
{
    private ILogger<IncorrectPredictionController> _logger;
    public IncorrectPredictionController(ILogger<IncorrectPredictionController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Post a review of an incorrect prediction
    /// </summary>
    /// <response code="200">Response successfully submitted</response>
    [HttpPost]
    [ProducesResponseType(typeof(PredictionResponse), StatusCodes.Status200OK)]
    public IActionResult Post([FromBody] IncorrectPredictionInput input)
    {
        return Ok();
    }
}