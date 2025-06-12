using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Services.WorkoutCalculation;

/// <summary>
/// Service for performing workout calculations.
/// Abstracts complex calculation logic from the Workout model.
/// </summary>
public class WorkoutCalculationService : IWorkoutCalculationService
{
    public TimeSpan CalculateTotalTime(Workout workout)
    {
        ArgumentNullException.ThrowIfNull(workout);

        var flattenedSteps = GetFlattenedSteps(workout);

        var totalTicks = flattenedSteps
            .Sum(step => step.EstimatedTime.Ticks);

        return new TimeSpan(totalTicks);
    }

    public TimeSpan CalculateEstimatedTime(Workout workout)
    {
        ArgumentNullException.ThrowIfNull(workout);

        var totalEstimatedTicks = GetFlattenedSteps(workout)
            .Sum(step => step.EstimatedTime.Ticks);

        return new TimeSpan(totalEstimatedTicks);
    }

    public Distance CalculateTotalDistance(Workout workout)
    {
        ArgumentNullException.ThrowIfNull(workout);

        var flattenedSteps = GetFlattenedSteps(workout);

        var distance = flattenedSteps
            .Sum(step => step.TotalDistance.DistanceValue + step.EstimatedDistance.DistanceValue);

        return Distance.Kilometers(Math.Round(distance, 1, MidpointRounding.AwayFromZero));
    }

    public Distance CalculateEstimatedDistance(Workout workout)
    {
        ArgumentNullException.ThrowIfNull(workout);

        var estimatedDistance = GetFlattenedSteps(workout)
            .Sum(step => step.EstimatedDistance.DistanceValue + step.TotalDistance.DistanceValue);

        return Distance.Kilometers(estimatedDistance);
    }

    /// <summary>
    /// Gets all simple steps from a workout, flattening any repeat structures.
    /// </summary>
    /// <param name="workout">The workout to process.</param>
    /// <returns>Enumerable of all simple steps in the workout.</returns>
    private static IEnumerable<SimpleStep> GetFlattenedSteps(Workout workout)
    {
        return workout.Steps.SelectMany<Step, SimpleStep>(step =>
        {
            switch (step)
            {
                case SimpleStep simpleStep:
                    return [simpleStep];
                case Repeat repeat:
                {
                    var repeatSteps = new List<SimpleStep>();

                    for (int i = 0; i < repeat.RepetitionCount; i++)
                    {
                        repeatSteps.AddRange(repeat.Steps);
                    }

                    return repeatSteps;
                }
                default:
                    return [];
            }
        });
    }
}
