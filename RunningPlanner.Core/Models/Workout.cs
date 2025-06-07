using System.ComponentModel;
using RunningPlanner.Core.Exceptions;
using RunningPlanner.Core.Services.WorkoutNaming;

namespace RunningPlanner.Core.Models;

/// <summary>
/// Represents a workout consisting of one or more steps.
/// </summary>
public record Workout
{
    /// <summary>
    /// Gets the type of workout.
    /// </summary>
    public WorkoutType Type { get; }

    /// <summary>
    /// Gets the steps that make up this workout.
    /// </summary>
    public IReadOnlyList<Step> Steps { get; }

    private static readonly Lazy<IWorkoutNamingService> NamingService = new(() => new WorkoutNamingService());
    
    /// <summary>
    /// Gets the name of the workout.
    /// </summary>
    public string Name => NamingService.Value.GenerateWorkoutName(this);

    /// <summary>
    /// Gets the total time for the workout.
    /// </summary>
    public TimeSpan TotalTime => CalculateTotalTime();

    /// <summary>
    /// Gets the estimated time for the workout.
    /// </summary>
    public TimeSpan EstimatedTime => CalculateEstimatedTime();

    /// <summary>
    /// Gets the total distance for the workout.
    /// </summary>
    public Distance TotalDistance => CalculateTotalDistance();

    /// <summary>
    /// Gets the estimated distance for the workout.
    /// </summary>
    public Distance EstimatedDistance => CalculateEstimatedDistance();

    private Workout(WorkoutType type, IEnumerable<Step> steps)
    {
        Type = type;

        if (Type is WorkoutType.Rest)
        {
            Steps = new List<Step>().AsReadOnly();

            return;
        }

        var stepsList = steps?.ToList() ?? [];

        if (stepsList.Count < 1)
        {
            throw new WorkoutGenerationException(type, "Workout must contain at least one step.");
        }

        // Ensure no null steps
        if (stepsList.Any(step => step == null))
        {
            throw new WorkoutGenerationException(type, "Steps collection cannot contain null elements.");
        }

        Steps = stepsList.AsReadOnly();
    }


    /// <summary>
    /// Calculates the total time for the workout.
    /// </summary>
    private TimeSpan CalculateTotalTime()
    {
        var totalTicks = Steps
            .SelectMany<Step, SimpleStep>(step => step is SimpleStep simpleStep
                ? [simpleStep]
                : (step as Repeat)?.Steps ?? [])
            .Sum(step => step.TotalTime.Ticks);

        return new TimeSpan(totalTicks);
    }

    /// <summary>
    /// Calculates the estimated time for the workout.
    /// </summary>
    private TimeSpan CalculateEstimatedTime()
    {
        var totalEstimatedTicks = Steps
            .SelectMany<Step, SimpleStep>(step => step is SimpleStep simpleStep
                ? [simpleStep]
                : (step as Repeat)?.Steps ?? [])
            .Sum(step => step.EstimatedTime.Ticks);

        return new TimeSpan(totalEstimatedTicks);
    }

    /// <summary>
    /// Calculates the total distance for the workout.
    /// </summary>
    private Distance CalculateTotalDistance()
    {
        var distance = Steps
            .SelectMany<Step, SimpleStep>(step => step is SimpleStep simpleStep
                ? [simpleStep]
                : (step as Repeat)?.Steps ?? [])
            .Sum(step => step.TotalDistance.DistanceValue);

        return Distance.Kilometers(Math.Round(distance, 1, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Calculates the estimated distance for the workout.
    /// </summary>
    private Distance CalculateEstimatedDistance()
    {
        var estimatedDistance = Steps
            .SelectMany<Step, SimpleStep>(step => step is SimpleStep simpleStep
                ? [simpleStep]
                : (step as Repeat)?.Steps ?? [])
            .Sum(step => step.EstimatedDistance.DistanceValue);

        return Distance.Kilometers(estimatedDistance);
    }

    /// <summary>
    /// Creates a standard workout with the specified type and steps.
    /// </summary>
    public static Workout Create(WorkoutType type, IEnumerable<Step> steps) =>
        new(type, steps);

    /// <summary>
    /// Creates a standard workout with the specified type and steps.
    /// </summary>
    public static Workout Create(WorkoutType type, params Step[] steps) =>
        new(type, steps);
}

public enum WorkoutType
{
    Invalid,
    [Description("Cross Training")] Cross,
    [Description("Rest Day")] Rest,
    [Description("Walk/Run Intervals")] WalkRun,
    [Description("Easy Run")] EasyRun,
    [Description("Easy Run w/ Strides")] EasyRunWithStrides,
    [Description("Mid Distance Run")] MediumRun,
    [Description("Repetition")] Repetition,
    [Description("Intervals")] Intervals,
    [Description("Threshold")] Threshold,
    [Description("Threshold Intervals")] ThresholdRepeat,
    [Description("Tempo Run")] TempoRun,
    [Description("Tempo Run Intervals")] TempoRunRepeat,
    [Description("Fartlek")] Fartlek,
    [Description("Hill Repeat")] HillRepeat,
    [Description("Race Pace")] RacePace,
    [Description("Long Run")] LongRun,
    [Description("Race")] Race
}
