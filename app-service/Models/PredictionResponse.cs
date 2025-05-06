using System.ComponentModel.DataAnnotations;

namespace app_service.Models;

/// <summary>
///     Response containing a prediction.
/// </summary>
public class PredictionResponse
{
    /// <summary>
    ///     Prediction on the given input.
    /// </summary>
    public required bool Prediction { get; init; }
}
