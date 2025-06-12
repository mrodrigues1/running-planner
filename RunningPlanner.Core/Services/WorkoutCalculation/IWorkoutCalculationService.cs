using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Services.WorkoutCalculation;

/// <summary>
/// Service interface for performing workout calculations.
/// </summary>
public interface IWorkoutCalculationService
{
    /// <summary>
    /// Calculates the total time for a workout.
    /// </summary>
    /// <param name="workout">The workout to calculate time for.</param>
    /// <returns>The total time for the workout.</returns>
    TimeSpan CalculateTotalTime(Workout workout);

    /// <summary>
    /// Calculates the estimated time for a workout.
    /// </summary>
    /// <param name="workout">The workout to calculate estimated time for.</param>
    /// <returns>The estimated time for the workout.</returns>
    TimeSpan CalculateEstimatedTime(Workout workout);

    /// <summary>
    /// Calculates the total distance for a workout.
    /// </summary>
    /// <param name="workout">The workout to calculate distance for.</param>
    /// <returns>The total distance for the workout.</returns>
    Distance CalculateTotalDistance(Workout workout);

    /// <summary>
    /// Calculates the estimated distance for a workout.
    /// </summary>
    /// <param name="workout">The workout to calculate estimated distance for.</param>
    /// <returns>The estimated distance for the workout.</returns>
    Distance CalculateEstimatedDistance(Workout workout);
}