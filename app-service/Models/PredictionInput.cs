namespace app_service.Models;

/// <summary>
///     Input object for prediction
/// </summary>
public class PredictionInput
{
    /// <summary>
    ///     Input string.
    /// </summary>
    public required string Input { get; init; }
}
