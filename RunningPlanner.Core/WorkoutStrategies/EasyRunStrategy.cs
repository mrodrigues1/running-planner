using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public class EasyRunStrategy : IWorkoutStrategy
{
    public Workout Generate(WorkoutParameters parameters)
    {
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            parameters.TotalDistance,
            IntensityTarget.Pace(parameters.Paces.EasyPace.Min, parameters.Paces.EasyPace.Max));

        return Workout.Create(WorkoutType.EasyRun, (IEnumerable<Step>) [step]);
    }
}
