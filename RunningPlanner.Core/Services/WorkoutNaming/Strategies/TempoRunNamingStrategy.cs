using RunningPlanner.Core.Extensions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Services.WorkoutNaming.Strategies;

/// <summary>
/// Naming strategy for tempo and threshold runs.
/// </summary>
public class TempoRunNamingStrategy : IWorkoutNamingStrategy
{
    public IReadOnlySet<WorkoutType> SupportedWorkoutTypes { get; } = new HashSet<WorkoutType>
    {
        WorkoutType.TempoRun,
        WorkoutType.Threshold
    };

    public IEnumerable<string> GenerateNameComponents(Workout workout)
    {
        yield return workout.Type.GetDescription();

        var steps = WorkoutNamingHelper.GetFlattenedSteps(workout);
        var metric = WorkoutNamingHelper.GetDistanceMetric(steps);

        yield return WorkoutNamingHelper.FormatTotalDistance(workout.TotalDistance, metric);

        if (steps.Any())
        {
            var stepRun = steps.FirstOrDefault(x => x.Type == StepType.Run);
            if (stepRun != null)
            {
                var stepRunText = WorkoutNamingHelper.FormatStepDistance(stepRun.TotalDistance.DistanceValue);
                yield return $"{stepRunText}{stepRun.IntensityTarget.PaceFormatted()}";
            }
        }
    }
}