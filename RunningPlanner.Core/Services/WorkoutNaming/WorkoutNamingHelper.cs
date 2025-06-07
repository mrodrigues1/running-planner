using System.Globalization;
using RunningPlanner.Core.Extensions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Services.WorkoutNaming;

/// <summary>
/// Helper class containing common utilities for workout naming strategies.
/// </summary>
public static class WorkoutNamingHelper
{
    /// <summary>
    /// Gets all simple steps from a workout, flattening any repeat structures.
    /// </summary>
    /// <param name="workout">The workout to process.</param>
    /// <returns>Enumerable of all simple steps in the workout.</returns>
    public static IEnumerable<SimpleStep> GetFlattenedSteps(Workout workout)
    {
        return workout.Steps.SelectMany<Step, SimpleStep>(step => step switch
        {
            SimpleStep simpleStep => [simpleStep],
            Repeat repeat => repeat.Steps,
            _ => []
        });
    }

    /// <summary>
    /// Gets the distance metric from a collection of steps.
    /// </summary>
    /// <param name="steps">The steps to analyze.</param>
    /// <returns>The distance metric used by the steps.</returns>
    public static DistanceMetric GetDistanceMetric(IEnumerable<SimpleStep> steps)
    {
        return steps
            .Select(x => x.Duration is Duration.DistanceDuration distanceDuration
                ? distanceDuration.Metric
                : DistanceMetric.Invalid)
            .FirstOrDefault();
    }

    /// <summary>
    /// Formats the total distance for display in workout names.
    /// </summary>
    /// <param name="totalDistance">The total distance to format.</param>
    /// <param name="metric">The distance metric to use.</param>
    /// <returns>Formatted distance string.</returns>
    public static string FormatTotalDistance(Distance totalDistance, DistanceMetric metric)
    {
        var metricSuffix = metric == DistanceMetric.Kilometers ? "km" : metric.ToString();

        var distanceStr = totalDistance.DistanceValue % 1 == 0
            ? ((int) totalDistance.DistanceValue).ToString(CultureInfo.InvariantCulture)
            : totalDistance.DistanceValue.ToString(CultureInfo.InvariantCulture);

        return $"{distanceStr} {metricSuffix}";
    }

    /// <summary>
    /// Formats a step distance value for display in workout names.
    /// </summary>
    /// <param name="distanceValue">The distance value in kilometers.</param>
    /// <returns>Formatted distance string with appropriate units.</returns>
    public static string FormatStepDistance(decimal distanceValue)
    {
        return distanceValue >= 1m
            ? $"{distanceValue.RoundTo(0)}km"
            : $"{(distanceValue * 1000).RoundTo(0)}m";
    }
}
