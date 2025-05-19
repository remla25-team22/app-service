using Prometheus;

namespace app_service.Util;

public static class MetricsRegistry
{
    public static readonly Gauge IncorrectPredictionRate =
        Metrics.CreateGauge("incorrect_prediction_rate", "Current number of active sessions");

    public static readonly Counter IncorrectPredictions =
        Metrics.CreateCounter("incorrect_prediction_count", "Total number of submits");

    public static readonly Counter Predictions =
        Metrics.CreateCounter("prediction_count", "Total number of submits");

    public static readonly Histogram PredictionResponseTime =
        Metrics.CreateHistogram("prediction_response_times", "Backend prediction response time in seconds", new HistogramConfiguration
        {
            Buckets = Histogram.LinearBuckets(start: 0, width: .05, count: 20)
        }
        );

    public static void UpdateIncorrectGauge()
    {
        IncorrectPredictionRate.Set(IncorrectPredictions.Value/Predictions.Value);
    }
}