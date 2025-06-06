using RunningPlanner.Core.Exceptions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public interface IWorkoutGenerator
{
    Workout GenerateWorkout(WorkoutType type, WorkoutParameters parameters);
}

public class WorkoutGenerator : IWorkoutGenerator
{
    private readonly IReadOnlyDictionary<WorkoutType, IWorkoutStrategy> _strategies = InitializeWorkoutStrategies();
    
    private static IReadOnlyDictionary<WorkoutType, IWorkoutStrategy> InitializeWorkoutStrategies()
    {
        return new Dictionary<WorkoutType, IWorkoutStrategy>
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
    }

    public Workout GenerateWorkout(WorkoutType type, WorkoutParameters parameters)
    {
        // Validate parameters
        ValidateWorkoutType(type);
        ValidateWorkoutParameters(parameters);

        // Get strategy and generate workout
        if (!_strategies.TryGetValue(type, out var strategy))
        {
            throw new WorkoutGenerationException(
                type,
                $"No strategy found for workout type '{type}'. Supported types: {string.Join(", ", _strategies.Keys)}");
        }

        try
        {
            return strategy.Generate(parameters);
        }
        catch (Exception ex) when (!(ex is WorkoutGenerationException))
        {
            throw new WorkoutGenerationException(
                type,
                $"Failed to generate workout of type '{type}': {ex.Message}",
                ex);
        }
    }

    private static void ValidateWorkoutType(WorkoutType type)
    {
        if (!Enum.IsDefined(typeof(WorkoutType), type))
        {
            throw new WorkoutGenerationException(
                type,
                $"Invalid workout type: {type}. Must be a valid WorkoutType enum value.");
        }

        if (type == WorkoutType.Invalid)
        {
            throw new WorkoutGenerationException(
                type,
                "Cannot generate workout for 'Invalid' workout type.");
        }
    }

    private static void ValidateWorkoutParameters(WorkoutParameters parameters)
    {
        if (parameters == null)
        {
            throw new WorkoutGenerationException(
                "WorkoutParameters cannot be null.");
        }

        // Validate Phase
        if (!Enum.IsDefined(typeof(TrainingPhase), parameters.Phase))
        {
            throw new WorkoutGenerationException(
                $"Invalid training phase: {parameters.Phase}. Must be a valid TrainingPhase enum value.");
        }

        // Validate ExperienceLevel
        if (!Enum.IsDefined(typeof(ExperienceLevel), parameters.ExperienceLevel))
        {
            throw new WorkoutGenerationException(
                $"Invalid experience level: {parameters.ExperienceLevel}. Must be a valid ExperienceLevel enum value.");
        }

        // Validate Paces
        if (parameters.Paces == null)
        {
            throw new WorkoutGenerationException(
                "TrainingPaces cannot be null.");
        }

        // Validate week numbers
        if (parameters.WeekNumber <= 0)
        {
            throw new WorkoutGenerationException(
                $"WeekNumber must be positive, but was {parameters.WeekNumber}.");
        }

        if (parameters.PhaseWeekNumber <= 0)
        {
            throw new WorkoutGenerationException(
                $"PhaseWeekNumber must be positive, but was {parameters.PhaseWeekNumber}.");
        }

        // Validate TotalDistance
        if (parameters.TotalDistance <= 0)
        {
            throw new WorkoutGenerationException(
                $"TotalDistance must be positive, but was {parameters.TotalDistance:F2} km.");
        }

        // Validate optional RestBeforeStartIntervalDistance
        if (parameters.RestBeforeStartIntervalDistance.HasValue)
        {
            var restDistance = parameters.RestBeforeStartIntervalDistance.Value;

            if (restDistance < 0)
            {
                throw new WorkoutGenerationException(
                    $"RestBeforeStartIntervalDistance cannot be negative, but was {restDistance:F2} km.");
            }

            if (restDistance > parameters.TotalDistance)
            {
                throw new WorkoutGenerationException(
                    $"RestBeforeStartIntervalDistance ({restDistance:F2} km) cannot exceed TotalDistance ({parameters.TotalDistance:F2} km).");
            }
        }
    }
}
