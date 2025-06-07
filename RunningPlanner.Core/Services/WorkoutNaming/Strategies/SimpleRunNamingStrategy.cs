using RunningPlanner.Core.Extensions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Services.WorkoutNaming.Strategies;

/// <summary>
/// Naming strategy for simple run workouts (Easy, Medium, Long runs).
/// </summary>
public class SimpleRunNamingStrategy : IWorkoutNamingStrategy
{
    public IReadOnlySet<WorkoutType> SupportedWorkoutTypes { get; } = new HashSet<WorkoutType>
    {
        WorkoutType.EasyRun,
        WorkoutType.MediumRun,
        WorkoutType.LongRun
    };

    public IEnumerable<string> GenerateNameComponents(Workout workout)
    {
        yield return workout.Type.GetDescription();

        var steps = WorkoutNamingHelper.GetFlattenedSteps(workout);
        var metric = WorkoutNamingHelper.GetDistanceMetric(steps);

        yield return WorkoutNamingHelper.FormatTotalDistance(workout.TotalDistance, metric);

        if (steps.Any())
        {
            yield return steps.First().IntensityTarget.PaceFormatted();
        }
    }
}