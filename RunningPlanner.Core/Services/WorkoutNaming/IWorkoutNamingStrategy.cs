using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Services.WorkoutNaming;

/// <summary>
/// Strategy interface for generating names for specific workout types.
/// </summary>
public interface IWorkoutNamingStrategy
{
    /// <summary>
    /// Gets the workout types this strategy can handle.
    /// </summary>
    IReadOnlySet<WorkoutType> SupportedWorkoutTypes { get; }
    
    /// <summary>
    /// Generates a name for the workout.
    /// </summary>
    /// <param name="workout">The workout to generate a name for.</param>
    /// <returns>The generated workout name components.</returns>
    IEnumerable<string> GenerateNameComponents(Workout workout);
}