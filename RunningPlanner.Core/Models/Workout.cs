using System.ComponentModel;
using System.Globalization;
using RunningPlanner.Core.Exceptions;
using RunningPlanner.Core.Extensions;

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

    /// <summary>
    /// Gets the name of the workout.
    /// </summary>
    public string Name => BuildWorkoutName();

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
    /// Builds the workout name based on its type and contents.
    /// </summary>
    private string BuildWorkoutName()
    {
        if (Type is WorkoutType.Rest)
        {
            return Type.GetDescription();
        }

        var workoutName = new List<string>();

        workoutName.Add(Type.GetDescription());

        var steps = Steps
            .SelectMany<Step, SimpleStep>(step => step is SimpleStep simpleStep
                ? [simpleStep]
                : (step as Repeat)?.Steps ?? []);

        var metric = steps
            .Select(x => x.Duration is Duration.DistanceDuration distanceDuration
                ? distanceDuration.Metric
                : DistanceMetric.Invalid)
            .FirstOrDefault();

        workoutName.Add(
            $"{TotalDistance.DistanceValue.ToString(CultureInfo.InvariantCulture)} {(metric is DistanceMetric.Kilometers ? "km" : metric.ToString())}");

        switch (Type)
        {
            case WorkoutType.EasyRun
                or WorkoutType.MediumRun
                or WorkoutType.LongRun:
                workoutName.Add(steps.First().IntensityTarget.PaceFormatted());

                break;
            case WorkoutType.HillRepeat
                or WorkoutType.Intervals
                or WorkoutType.Repetition
                or WorkoutType.TempoRunRepeat
                or WorkoutType.ThresholdRepeat:
            {
                var stepQuantity = steps.Count() / 2;
                var stepQuantityText = $"{stepQuantity}";

                var stepRun = steps.First(x => x.Type == StepType.Run);

                var stepRunText = stepRun.TotalDistance.DistanceValue >= 1m
                    ? $"{stepRun.TotalDistance.DistanceValue.RoundTo(0)}km"
                    : $"{(stepRun.TotalDistance.DistanceValue * 1000).RoundTo(0)}m";

                var stepRecover = steps.First(x => x.Type is StepType.Walk or StepType.Recover or StepType.Rest);

                var stepRecoverText = stepRecover.TotalDistance.DistanceValue >= 1m
                    ? $"{stepRun.TotalDistance.DistanceValue.RoundTo(0)}km {stepRecover.Type.GetDescription()}"
                    : $"{(stepRun.TotalDistance.DistanceValue * 1000).RoundTo(0)}m {stepRecover.Type.GetDescription()}";

                workoutName.Add(
                    $"{stepQuantityText} x ({stepRunText}{stepRun.IntensityTarget.PaceFormatted()} + {stepRecoverText})");

                break;
            }
            case WorkoutType.TempoRun
                or WorkoutType.Threshold:
            {
                var stepRun = steps.First(x => x.Type == StepType.Run);

                var stepRunText = stepRun.TotalDistance.DistanceValue >= 1m
                    ? $"{stepRun.TotalDistance.DistanceValue.RoundTo(0)}km"
                    : $"{(stepRun.TotalDistance.DistanceValue * 1000).RoundTo(0)}m";

                workoutName.Add($"{stepRunText}{stepRun.IntensityTarget.PaceFormatted()}");

                break;
            }
            case WorkoutType.EasyRunWithStrides:
            {
                var onlyStrideSteps = steps.Skip(1).Take(steps.Count());
                var stepQuantity = onlyStrideSteps.Count() / 2;
                var stepQuantityText = $"{stepQuantity}";

                var stepRun = onlyStrideSteps.First(x => x.Type == StepType.Run);

                var stepRunText = stepRun.TotalDistance.DistanceValue >= 1m
                    ? $"{stepRun.TotalDistance.DistanceValue.RoundTo(0)}km"
                    : $"{(stepRun.TotalDistance.DistanceValue * 1000).RoundTo(0)}m";

                var stepRecover =
                    onlyStrideSteps.First(x => x.Type is StepType.Walk or StepType.Recover or StepType.Rest);

                var stepRecoverText = stepRecover.TotalDistance.DistanceValue >= 1m
                    ? $"{stepRun.TotalDistance.DistanceValue.RoundTo(0)}km {stepRecover.Type.GetDescription()}"
                    : $"{(stepRun.TotalDistance.DistanceValue * 1000).RoundTo(0)}m {stepRecover.Type.GetDescription()}";

                workoutName.Add(
                    $"{stepQuantityText} x ({stepRunText}{stepRun.IntensityTarget.PaceFormatted()} + {stepRecoverText})");

                break;
            }
        }

        return string.Join(" - ", workoutName);
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
