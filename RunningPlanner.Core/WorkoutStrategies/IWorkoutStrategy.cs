using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public interface IWorkoutStrategy
{
    Workout Generate(WorkoutParameters parameters);
}