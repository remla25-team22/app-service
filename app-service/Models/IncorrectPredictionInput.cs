namespace app_service.Models;

public class IncorrectPredictionInput
{
    public bool IsPosistive { get; init; }
    public string Input { get; init; }
    public string  ModelVersion { get; init; } = "unknown";

}