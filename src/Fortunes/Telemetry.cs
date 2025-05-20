using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Fortunes;

internal static class Telemetry
{
    //tracing
    public static readonly ActivitySource ActivitySource = new ("Fortunes");

    //metrics
    public static readonly Meter Meter = new("Fortunes");

    public static readonly Histogram<int> FortuneSize = Meter.CreateHistogram<int>("ndc.fortune_size", "characters");
}