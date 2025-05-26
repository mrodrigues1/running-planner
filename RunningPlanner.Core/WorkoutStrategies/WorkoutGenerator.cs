using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public interface IWorkoutGenerator
{
    Workout GenerateWorkout(WorkoutType type, WorkoutParameters parameters);
}

public class WorkoutGenerator : IWorkoutGenerator
{
    private readonly Dictionary<WorkoutType, IWorkoutStrategy> _strategies =
        new()
        {
            {WorkoutType.EasyRun, new EasyRunStrategy()},
            {WorkoutType.LongRun, new LongRunStrategy()},
            {WorkoutType.TempoRun, new TempoRunStrategy()},
            {WorkoutType.Intervals, new IntervalsStrategy()},
            {WorkoutType.HillRepeat, new HillRepeatsStrategy()},
            {WorkoutType.RacePace, new RacePaceStrategy()},
            {WorkoutType.EasyRunWithStrides, new EasyRunWithStridesStrategy()},
            {WorkoutType.Race, new RaceStrategy()}
        };

    public Workout GenerateWorkout(WorkoutType type, WorkoutParameters parameters)
    {
        return _strategies[type].Generate(parameters);
    }
}
