using RunningPlanner.Core.Models;
using RunningPlanner.Core.WorkoutStrategies;

namespace RunningPlanner.Core.PlanGenerator;

public class WorkoutGenerator
{
    private readonly Dictionary<WorkoutType, IWorkoutStrategy> _strategies =
        new()
        {
            {WorkoutType.TempoRun, new TempoRunStrategy()},
            // etc
        };

    // etc

    public Workout GenerateWorkout(WorkoutType type, WorkoutParameters parameters)
    {
        return _strategies[type].Generate(parameters);
    }
}
