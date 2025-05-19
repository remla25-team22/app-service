using app_service.Models;
using app_service.Util;
using Microsoft.AspNetCore.Mvc;
using ModelServiceConnector;
using Prometheus;

namespace app_service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PredictController(ILogger<PredictController> logger, IApiClient apiClient) : ControllerBase
{
    /// <summary>
    ///     Gets a prediction based on the given input
    /// </summary>
    /// <response code="200">prediction response</response>
    [HttpPost]
    [ProducesResponseType(typeof(PredictionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] PredictionInput input)
    {
        MetricsRegistry.Predictions.Inc();
        MetricsRegistry.UpdateIncorrectGauge();
        Response apiResponse;
        using (MetricsRegistry.PredictionResponseTime.NewTimer())
        {
            apiResponse = await apiClient.AnonymousAsync(new Text { Text1 = input.Input });
        }

        logger.LogInformation(apiResponse.Prediction.ToString());
        PredictionResponse predictionResponse = new()
        {
            Prediction = apiResponse.Prediction == 1
        };
        return Ok(predictionResponse);
    }
}