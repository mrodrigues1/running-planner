using RunningPlanner.Core.Extensions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Services.WorkoutNaming.Strategies;

/// <summary>
/// Naming strategy for interval-based workouts (Hills, Intervals, Repetitions, etc.).
/// </summary>
public class IntervalNamingStrategy : IWorkoutNamingStrategy
{
    public IReadOnlySet<WorkoutType> SupportedWorkoutTypes { get; } = new HashSet<WorkoutType>
    {
        WorkoutType.HillRepeat,
        WorkoutType.Intervals,
        WorkoutType.Repetition,
        WorkoutType.TempoRunRepeat,
        WorkoutType.ThresholdRepeat
    };

    public IEnumerable<string> GenerateNameComponents(Workout workout)
    {
        yield return workout.Type.GetDescription();

        var steps = WorkoutNamingHelper.GetFlattenedSteps(workout);
        var metric = WorkoutNamingHelper.GetDistanceMetric(steps);

        yield return WorkoutNamingHelper.FormatTotalDistance(workout.TotalDistance, metric);

        if (steps.Any())
        {
            var intervalDetails = BuildIntervalDetails(steps);
            if (!string.IsNullOrEmpty(intervalDetails))
            {
                yield return intervalDetails;
            }
        }
    }

    private static string BuildIntervalDetails(IEnumerable<SimpleStep> steps)
    {
        var stepsList = steps.ToList();
        
        if (stepsList.Count < 2)
            return string.Empty;

        var stepQuantity = stepsList.Count / 2;
        var stepRun = stepsList.FirstOrDefault(x => x.Type == StepType.Run);
        var stepRecover = stepsList.FirstOrDefault(x => x.Type is StepType.Walk or StepType.Recover or StepType.Rest);

        if (stepRun == null || stepRecover == null)
            return string.Empty;

        var stepRunText = WorkoutNamingHelper.FormatStepDistance(stepRun.TotalDistance.DistanceValue);
        var stepRecoverText = $"{WorkoutNamingHelper.FormatStepDistance(stepRecover.TotalDistance.DistanceValue)} {stepRecover.Type.GetDescription()}";

        return $"{stepQuantity} x ({stepRunText}{stepRun.IntensityTarget.PaceFormatted()} + {stepRecoverText})";
    }
}