using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Extensions.JackDaniels;

/// <summary>
/// Provides workout implementations for Jack Daniels' Novice Marathon Training Plan
/// for weeks 18-16 until race (first phase).
/// </summary>
public class JackDanielsMarathonNoviceSections18To16UntilRace
{
    /// <summary>
    /// Session A: 15 × 1:00 E w/1:00 W
    /// </summary>
    public static Workout CreateSessionA()
    {
        // 15 × 1:00 E w/1:00 W
        return WorkoutExtensions.CreateRunWalkWorkout(
            EasyPaceRange(),
            WalkPaceRange(),
            new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))
        );
    }

    /// <summary>
    /// Session B: Repeat of Session A (optional)
    /// </summary>
    public static Workout CreateSessionB()
    {
        // Repeat of Session A (optional)
        return CreateSessionA();
    }

    /// <summary>
    /// Session C: 9 × 1:00 E w/1:00 W + 3 × 2:00 E w/2:00 W
    /// </summary>
    public static Workout CreateSessionC()
    {
        // 9 × 1:00 E w/1:00 W + 3 × 2:00 E w/2:00 W
        return WorkoutExtensions.CreateRunWalkWorkout(
            EasyPaceRange(),
            WalkPaceRange(),
            new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
            new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))
        );
    }

    /// <summary>
    /// Session D: Repeat of Session C (optional)
    /// </summary>
    public static Workout CreateSessionD()
    {
        // Repeat of Session C (optional)
        return CreateSessionC();
    }

    /// <summary>
    /// Session E: 9 × 1:00 E w/1:00 W + 2 × 3:00 E w/3:00 W
    /// </summary>
    public static Workout CreateSessionE()
    {
        // 9 × 1:00 E w/1:00 W + 2 × 3:00 E w/3:00 W
        return WorkoutExtensions.CreateRunWalkWorkout(
            EasyPaceRange(),
            WalkPaceRange(),
            new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
            new WalkRunInterval(2, TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(3))
        );
    }
    
    /// <summary>
    /// Defines the easy running pace range for novice runners (per kilometer)
    /// </summary>
    private static (TimeSpan min, TimeSpan max) EasyPaceRange()
    {
        // Using a conservative pace for beginners
        return (TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(30)),
            TimeSpan.FromMinutes(9).Add(TimeSpan.FromSeconds(0)));
    }

    /// <summary>
    /// Defines the walking pace range for recovery segments (per kilometer)
    /// </summary>
    private static (TimeSpan min, TimeSpan max) WalkPaceRange()
    {
        return (TimeSpan.FromMinutes(9).Add(TimeSpan.FromSeconds(30)),
            TimeSpan.FromMinutes(12).Add(TimeSpan.FromSeconds(0)));
    }
}