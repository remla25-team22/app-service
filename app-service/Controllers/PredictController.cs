using app_service.Models;
using Microsoft.AspNetCore.Mvc;
using ModelServiceConnector;

namespace app_service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PredictController : ControllerBase
{
    private IApiClient _apiClient;
    private ILogger<PredictController> _logger;
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
        var apiResponse = await _apiClient.AnonymousAsync(new Text{Text1 = input.Input});
        _logger.LogInformation(apiResponse.Prediction.ToString());
        PredictionResponse predictionResponse = new()
            {
                Prediction = apiResponse.Prediction == 1
            };
        return Ok(predictionResponse);
    }
}