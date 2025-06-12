using System.ComponentModel;
using RunningPlanner.Core.Exceptions;
using RunningPlanner.Core.Services.WorkoutCalculation;
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
    private static readonly Lazy<IWorkoutCalculationService> CalculationService = new(() => new WorkoutCalculationService());
    
    /// <summary>
    /// Gets the name of the workout.
    /// </summary>
    public string Name => NamingService.Value.GenerateWorkoutName(this);

    /// <summary>
    /// Gets the total time for the workout.
    /// </summary>
    public TimeSpan TotalTime => CalculationService.Value.CalculateTotalTime(this);

    /// <summary>
    /// Gets the estimated time for the workout.
    /// </summary>
    public TimeSpan EstimatedTime => CalculationService.Value.CalculateEstimatedTime(this);

    /// <summary>
    /// Gets the total distance for the workout.
    /// </summary>
    public Distance TotalDistance => CalculationService.Value.CalculateTotalDistance(this);

    /// <summary>
    /// Gets the estimated distance for the workout.
    /// </summary>
    public Distance EstimatedDistance => CalculationService.Value.CalculateEstimatedDistance(this);

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
        
        Steps = stepsList.AsReadOnly();
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
