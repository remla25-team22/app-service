using app_service.Models;
using Microsoft.AspNetCore.Mvc;

namespace app_service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PredictionController : ControllerBase
{
    /// <summary>
    ///     Gets a prediction based on the given input
    /// </summary>
    /// <response code="200">prediction response</response>
    [HttpPost]
    [ProducesResponseType(typeof(PredictionResponse),StatusCodes.Status200OK)]
    public IActionResult Post([FromBody] PredictionInput input)
    {
        PredictionResponse predictionResponse = new () {Prediction = input.Input};

        return Ok(predictionResponse);
    }
}