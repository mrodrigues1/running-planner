using System.Collections.Immutable;
using RunningPlanner.Core.Models;
using RunningPlanner.Core.Services.WorkoutNaming.Strategies;

namespace RunningPlanner.Core.Services.WorkoutNaming;

/// <summary>
/// Service for generating descriptive names for workouts using a strategy pattern.
/// </summary>
public class WorkoutNamingService : IWorkoutNamingService
{
    private readonly ImmutableDictionary<WorkoutType, IWorkoutNamingStrategy> _strategyMap;
    private readonly IWorkoutNamingStrategy _defaultStrategy;

    public WorkoutNamingService()
    {
        var strategies = new List<IWorkoutNamingStrategy>
        {
            new SimpleRunNamingStrategy(),
            new IntervalNamingStrategy(),
            new TempoRunNamingStrategy(),
            new EasyRunWithStridesNamingStrategy()
        };

        _defaultStrategy = new DefaultNamingStrategy();
        _strategyMap = BuildStrategyMap(strategies);
    }

    /// <summary>
    /// Constructor for dependency injection with custom strategies.
    /// </summary>
    /// <param name="strategies">Collection of naming strategies to use.</param>
    /// <param name="defaultStrategy">Default strategy for unhandled workout types.</param>
    public WorkoutNamingService(IEnumerable<IWorkoutNamingStrategy> strategies, IWorkoutNamingStrategy? defaultStrategy = null)
    {
        _defaultStrategy = defaultStrategy ?? new DefaultNamingStrategy();
        _strategyMap = BuildStrategyMap(strategies);
    }

    public string GenerateWorkoutName(Workout workout)
    {
        ArgumentNullException.ThrowIfNull(workout);

        var strategy = _strategyMap.GetValueOrDefault(workout.Type, _defaultStrategy);
        var nameComponents = strategy.GenerateNameComponents(workout);
        
        return string.Join(" - ", nameComponents.Where(component => !string.IsNullOrWhiteSpace(component)));
    }

    private static ImmutableDictionary<WorkoutType, IWorkoutNamingStrategy> BuildStrategyMap(IEnumerable<IWorkoutNamingStrategy> strategies)
    {
        var builder = ImmutableDictionary.CreateBuilder<WorkoutType, IWorkoutNamingStrategy>();

        foreach (var strategy in strategies)
        {
            foreach (var workoutType in strategy.SupportedWorkoutTypes)
            {
                builder[workoutType] = strategy;
            }
        }

        return builder.ToImmutable();
    }
}