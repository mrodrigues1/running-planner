namespace RunningPlanner.Core.Models;

/// <summary>
/// Represents a single workout step.
/// </summary>
public sealed record SimpleStepSample : Step
{
    public StepType Type { get; }
    public Duration Duration { get; }

    // Calculated properties
    public Distance TotalDistance => CalculateTotalDistance();
    public TimeSpan TotalTime => CalculateTotalTime();

    // Private constructor ensures validation at creation time
    private SimpleStepSample(StepType type, Duration duration)
    {
        if (!Enum.IsDefined(type))
        {
            throw new ArgumentException("Invalid step type.");
        }

        Type = type;
        Duration = duration ?? throw new ArgumentNullException(nameof(duration));
    }

    // Factory methods for common use cases
    public static SimpleStepSample Create(StepType type, Duration duration)
        => new(type, duration);

    public static SimpleStepSample CreateWithKilometers(
        StepType type,
        decimal distance)
        => new(type, Duration.ForKilometers(distance));
    
    public static SimpleStepSample CreateWithMinutes(
        StepType type,
        int minutes)
        => new(type, Duration.ForTime(TimeSpan.FromMinutes(minutes)));

    private Distance CalculateTotalDistance()
    {
        var distanceDuration = Duration as Duration.DistanceDuration;
        var stepDistance = distanceDuration?.Value ?? 0;

        var distanceMetric = distanceDuration?.Metric ?? DistanceMetric.Invalid;

        return distanceMetric is DistanceMetric.Kilometers
            ? Distance.Kilometers(stepDistance)
            : Distance.Miles(stepDistance);
    }

    private TimeSpan CalculateTotalTime()
    {
        var timeDuration = Duration as Duration.TimeDuration;
        var totalTicks = timeDuration?.Time.Ticks ?? 0;

        return new TimeSpan(totalTicks);
    }
}
