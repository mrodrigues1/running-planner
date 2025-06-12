using RunningPlanner.Core.Exceptions;

namespace RunningPlanner.Core.Models;

/// <summary>
/// Represents a single workout step.
/// </summary>
public sealed record SimpleStep : Step
{
    public StepType Type { get; }
    public Duration Duration { get; }
    public IntensityTarget IntensityTarget { get; }

    // Calculated properties
    public Distance EstimatedDistance => CalculateEstimatedDistance();
    public Distance TotalDistance => CalculateTotalDistance();
    public TimeSpan EstimatedTime => CalculateEstimatedTime();
    public TimeSpan TotalTime => CalculateTotalTime();

    // Private constructor ensures validation at creation time
    private SimpleStep(StepType type, Duration duration, IntensityTarget intensityTarget)
    {
        if (!Enum.IsDefined(type))
        {
            throw new WorkoutGenerationException($"Invalid step type: {type}. Must be a valid StepType enum value.");
        }

        Type = type;
        Duration = duration ?? throw new WorkoutGenerationException("Duration cannot be null when creating a step.");
        IntensityTarget = intensityTarget ?? throw new WorkoutGenerationException("IntensityTarget cannot be null when creating a step.");
    }

    // Factory methods for common use cases
    public static SimpleStep Create(StepType type, Duration duration, IntensityTarget intensityTarget)
        => new(type, duration, intensityTarget);

    public static SimpleStep Create(StepType type, Duration duration, (TimeSpan min, TimeSpan max) paceRange)
        => new(type, duration, IntensityTarget.Pace(paceRange.min, paceRange.max));

    public static SimpleStep CreateWithKilometers(StepType type, decimal distance, IntensityTarget intensityTarget)
        => new(type, Duration.ForKilometers(distance), intensityTarget);

    public static SimpleStep CreateWithKilometers(
        StepType type,
        decimal distance,
        (TimeSpan min, TimeSpan max) paceRange)
        => new(type, Duration.ForKilometers(distance), IntensityTarget.Pace(paceRange.min, paceRange.max));

    public static SimpleStep CreateWithNoTarget(StepType type, Duration duration)
        => new(type, duration, IntensityTarget.None());

    public static SimpleStep CreateWithNoTarget(StepType type, decimal distance)
        => new(type, Duration.ForKilometers(distance), IntensityTarget.None());

    // Implementation details for calculated properties
    private Distance CalculateEstimatedDistance()
    {
        var timeDuration = Duration as Duration.TimeDuration;
        long totalTicks = timeDuration?.Time.Ticks ?? 0;

        var paceIntensity = IntensityTarget as IntensityTarget.PaceIntensity;
        long avgPaceTicks = paceIntensity?.Average.Ticks ?? 0;

        decimal estimatedDistance = (decimal) totalTicks / (decimal) avgPaceTicks;

        return Distance.Kilometers(Math.Round(estimatedDistance, 2, MidpointRounding.AwayFromZero));
    }

    private Distance CalculateTotalDistance()
    {
        var distanceDuration = Duration as Duration.DistanceDuration;
        var stepDistance = distanceDuration?.Value ?? 0;

        var distanceMetric = distanceDuration?.Metric ?? DistanceMetric.Invalid;

        return distanceMetric is DistanceMetric.Kilometers
            ? Distance.Kilometers(stepDistance)
            : Distance.Miles(stepDistance);
    }

    private TimeSpan CalculateEstimatedTime()
    {
        // Implementation logic here
        if (Duration is Duration.TimeDuration timeDuration)
        {
            return timeDuration.Time;
        }

        // If we have distance and pace, calculate time
        if (Duration is Duration.DistanceDuration distanceDuration &&
            IntensityTarget is IntensityTarget.PaceIntensity paceTarget)
        {
            // Simple estimation using average pace
            return TimeSpan.FromTicks((long) (distanceDuration.Value * paceTarget.Average.Ticks));
        }

        return TimeSpan.Zero; // Default
    }

    private TimeSpan CalculateTotalTime()
    {
        var timeDuration = Duration as Duration.TimeDuration;
        var totalTicks = timeDuration?.Time.Ticks ?? 0;

        return new TimeSpan(totalTicks);
    }
}

public enum StepType
{
    Invalid,
    WarmUp,
    CoolDown,
    Rest,
    Recover,
    Run,
    Walk
}
