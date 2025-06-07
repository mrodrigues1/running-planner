using RunningPlanner.Core.Exceptions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

/// <summary>
/// Base class for workout strategies providing common validation and utility methods.
/// </summary>
public abstract class BaseWorkoutStrategy : IWorkoutStrategy
{
    public abstract Workout Generate(WorkoutParameters parameters);

    /// <summary>
    /// Validates that a distance value is within reasonable bounds for a step.
    /// </summary>
    /// <param name="distance">The distance to validate in kilometers.</param>
    /// <param name="workoutType">The workout type being generated.</param>
    /// <param name="stepDescription">Description of the step being created.</param>
    /// <exception cref="WorkoutGenerationException">Thrown when distance is invalid.</exception>
    protected static void ValidateStepDistance(decimal distance, WorkoutType workoutType, string stepDescription = "step")
    {
        if (distance <= 0)
        {
            throw new WorkoutGenerationException(workoutType,
                $"Distance for {stepDescription} must be positive, but was {distance:F2} km.");
        }

        if (distance > 30) // Reasonable upper bound for a single step
        {
            throw new WorkoutGenerationException(workoutType,
                $"Distance for {stepDescription} ({distance:F2} km) exceeds reasonable limit of 30 km.");
        }
    }

    /// <summary>
    /// Validates that a duration value is within reasonable bounds for a step.
    /// </summary>
    /// <param name="duration">The duration to validate.</param>
    /// <param name="workoutType">The workout type being generated.</param>
    /// <param name="stepDescription">Description of the step being created.</param>
    /// <exception cref="WorkoutGenerationException">Thrown when duration is invalid.</exception>
    protected static void ValidateStepDuration(TimeSpan duration, WorkoutType workoutType, string stepDescription = "step")
    {
        if (duration <= TimeSpan.Zero)
        {
            throw new WorkoutGenerationException(workoutType,
                $"Duration for {stepDescription} must be positive, but was {duration}.");
        }

        if (duration > TimeSpan.FromHours(6)) // Reasonable upper bound for a single step
        {
            throw new WorkoutGenerationException(workoutType,
                $"Duration for {stepDescription} ({duration}) exceeds reasonable limit of 6 hours.");
        }
    }

    /// <summary>
    /// Validates that a repeat count is within reasonable bounds.
    /// </summary>
    /// <param name="repeatCount">The repeat count to validate.</param>
    /// <param name="workoutType">The workout type being generated.</param>
    /// <param name="description">Description of what is being repeated.</param>
    /// <exception cref="WorkoutGenerationException">Thrown when repeat count is invalid.</exception>
    protected static void ValidateRepeatCount(int repeatCount, WorkoutType workoutType, string description = "intervals")
    {
        if (repeatCount <= 0)
        {
            throw new WorkoutGenerationException(workoutType,
                $"Repeat count for {description} must be positive, but was {repeatCount}.");
        }

        if (repeatCount > 50) // Reasonable upper bound
        {
            throw new WorkoutGenerationException(workoutType,
                $"Repeat count for {description} ({repeatCount}) exceeds reasonable limit of 50 repetitions.");
        }
    }

    /// <summary>
    /// Creates a safe distance calculation ensuring the result is valid.
    /// </summary>
    /// <param name="totalDistance">Total workout distance.</param>
    /// <param name="percentage">Percentage of total distance.</param>
    /// <param name="workoutType">The workout type being generated.</param>
    /// <param name="description">Description of the distance calculation.</param>
    /// <returns>Calculated distance in kilometers.</returns>
    /// <exception cref="WorkoutGenerationException">Thrown when calculation results in invalid distance.</exception>
    protected static decimal CalculateDistanceSafely(decimal totalDistance, decimal percentage, WorkoutType workoutType, string description)
    {
        if (percentage < 0 || percentage > 1)
        {
            throw new WorkoutGenerationException(workoutType,
                $"Percentage for {description} must be between 0 and 1, but was {percentage:F2}.");
        }

        var result = totalDistance * percentage;
        
        if (result <= 0)
        {
            throw new WorkoutGenerationException(workoutType,
                $"Calculated distance for {description} must be positive, but was {result:F2} km (total: {totalDistance:F2} km, percentage: {percentage:P0}).");
        }

        return result;
    }
}