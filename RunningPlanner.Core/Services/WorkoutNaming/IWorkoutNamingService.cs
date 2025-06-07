using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Services.WorkoutNaming;

/// <summary>
/// Service for generating descriptive names for workouts based on their type and structure.
/// </summary>
public interface IWorkoutNamingService
{
    /// <summary>
    /// Generates a descriptive name for the given workout.
    /// </summary>
    /// <param name="workout">The workout to generate a name for.</param>
    /// <returns>A descriptive workout name.</returns>
    string GenerateWorkoutName(Workout workout);
}