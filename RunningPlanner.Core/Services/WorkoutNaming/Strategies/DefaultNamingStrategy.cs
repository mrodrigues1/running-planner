using RunningPlanner.Core.Extensions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Services.WorkoutNaming.Strategies;

/// <summary>
/// Default naming strategy for workout types not handled by specific strategies.
/// </summary>
public class DefaultNamingStrategy : IWorkoutNamingStrategy
{
    public IReadOnlySet<WorkoutType> SupportedWorkoutTypes { get; } = new HashSet<WorkoutType>();

    public IEnumerable<string> GenerateNameComponents(Workout workout)
    {
        if (workout.Type is WorkoutType.Rest)
        {
            yield return workout.Type.GetDescription();
            yield break;
        }

        yield return workout.Type.GetDescription();

        var steps = WorkoutNamingHelper.GetFlattenedSteps(workout);
        var metric = WorkoutNamingHelper.GetDistanceMetric(steps);

        yield return WorkoutNamingHelper.FormatTotalDistance(workout.TotalDistance, metric);
    }
}