using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public class WorkoutGenerator
{
    private readonly Dictionary<WorkoutType, IWorkoutStrategy> _strategies =
        new()
        {
            {WorkoutType.EasyRun, new EasyRunStrategy()},
            {WorkoutType.LongRun, new LongRunStrategy()},
            {WorkoutType.TempoRun, new TempoRunStrategy()},
            {WorkoutType.Intervals, new IntervalsStrategy()}
        };

    public Workout GenerateWorkout(WorkoutType type, WorkoutParameters parameters)
    {
        return _strategies[type].Generate(parameters);
    }
}
