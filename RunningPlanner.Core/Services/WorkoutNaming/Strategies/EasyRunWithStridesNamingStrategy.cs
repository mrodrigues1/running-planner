using RunningPlanner.Core.Extensions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Services.WorkoutNaming.Strategies;

/// <summary>
/// Naming strategy for easy runs with strides.
/// </summary>
public class EasyRunWithStridesNamingStrategy : IWorkoutNamingStrategy
{
    public IReadOnlySet<WorkoutType> SupportedWorkoutTypes { get; } = new HashSet<WorkoutType>
    {
        WorkoutType.EasyRunWithStrides
    };

    public IEnumerable<string> GenerateNameComponents(Workout workout)
    {
        yield return workout.Type.GetDescription();

        var steps = WorkoutNamingHelper.GetFlattenedSteps(workout);
        var metric = WorkoutNamingHelper.GetDistanceMetric(steps);

        yield return WorkoutNamingHelper.FormatTotalDistance(workout.TotalDistance, metric);

        if (steps.Any())
        {
            var stridesDetails = BuildStridesDetails(steps);
            if (!string.IsNullOrEmpty(stridesDetails))
            {
                yield return stridesDetails;
            }
        }
    }

    private static string BuildStridesDetails(IEnumerable<SimpleStep> steps)
    {
        var stepsList = steps.ToList();
        
        // Skip the first step (main easy run) and process the strides
        var onlyStrideSteps = stepsList.Skip(1).ToList();
        
        if (onlyStrideSteps.Count < 2)
            return string.Empty;

        var stepQuantity = onlyStrideSteps.Count / 2;
        var stepRun = onlyStrideSteps.FirstOrDefault(x => x.Type == StepType.Run);
        var stepRecover = onlyStrideSteps.FirstOrDefault(x => x.Type is StepType.Walk or StepType.Recover or StepType.Rest);

        if (stepRun == null || stepRecover == null)
            return string.Empty;

        var stepRunText = WorkoutNamingHelper.FormatStepDistance(stepRun.TotalDistance.DistanceValue);
        var stepRecoverText = $"{WorkoutNamingHelper.FormatStepDistance(stepRecover.TotalDistance.DistanceValue)} {stepRecover.Type.GetDescription()}";

        return $"{stepQuantity} x ({stepRunText}{stepRun.IntensityTarget.PaceFormatted()} + {stepRecoverText})";
    }
}