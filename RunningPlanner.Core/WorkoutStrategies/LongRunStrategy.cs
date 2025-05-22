using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public class LongRunStrategy : IWorkoutStrategy
{
    public Workout Generate(WorkoutParameters parameters)
    {
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            parameters.TotalDistance,
            IntensityTarget.Pace(parameters.Paces.EasyPace.Min, parameters.Paces.EasyPace.Max));

        return Workout.Create(WorkoutType.LongRun, (IEnumerable<Step>) [step]);
    }
}
