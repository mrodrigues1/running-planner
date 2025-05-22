using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public class WorkoutGenerator
{
    private readonly Dictionary<WorkoutType, IWorkoutStrategy> _strategies =
        new()
        {
            {WorkoutType.EasyRun, new EasyRunStrategy()},
            {WorkoutType.TempoRun, new TempoRunStrategy()},
            // etc
        };

    // etc

    public Workout GenerateWorkout(WorkoutType type, WorkoutParameters parameters)
    {
        return _strategies[type].Generate(parameters);
    }
}
