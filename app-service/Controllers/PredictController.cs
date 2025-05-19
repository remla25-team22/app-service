using app_service.Models;
using app_service.Util;
using Microsoft.AspNetCore.Mvc;
using ModelServiceConnector;
using Prometheus;

namespace app_service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PredictController : ControllerBase
{
    private readonly IApiClient _apiClient;
    private readonly ILogger<PredictController> _logger;

    public PredictController(ILogger<PredictController> logger)
    {
        _logger = logger;
        _apiClient = new ApiClient(new HttpClient
        {
            BaseAddress = new Uri("http://model-service:8080")
        });
    }

    /// <summary>
    ///     Gets a prediction based on the given input
    /// </summary>
    /// <response code="200">prediction response</response>
    [HttpPost]
    [ProducesResponseType(typeof(PredictionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] PredictionInput input)
    {
        MetricsRegistry.Predictions.Inc();

        Response apiResponse;
        using (MetricsRegistry.PredictionResponseTime.NewTimer())
        {
            apiResponse = await _apiClient.AnonymousAsync(new Text { Text1 = input.Input });
        }

        _logger.LogInformation(apiResponse.Prediction.ToString());
        PredictionResponse predictionResponse = new()
        {
            Prediction = apiResponse.Prediction == 1
        };
        return Ok(predictionResponse);
    }
}