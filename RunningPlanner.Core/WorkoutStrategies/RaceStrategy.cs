using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public class RaceStrategy : IWorkoutStrategy
{
    public Workout Generate(WorkoutParameters parameters)
    {
        var step = SimpleStep.CreateWithNoTarget(StepType.Run, Duration.ForKilometers(parameters.TotalDistance));

        return Workout.Create(WorkoutType.Race, [step]);
    }
}
