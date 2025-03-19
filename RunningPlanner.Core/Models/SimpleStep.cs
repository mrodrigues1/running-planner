namespace RunningPlanner.Core.Models;

public class SimpleStep
{
    private SimpleStep()
    {
    }

    public StepType Type { get; private set; }
    public Duration Duration { get; private set; }
    public IntensityTarget IntensityTarget { get; private set; }
    public TimeSpan EstimatedTime => CalculateEstimatedTime();
    public TimeSpan TotalTime => CalculateTotalTime();
    public Distance EstimatedDistance => CalculateEstimatedDistance();
    public Distance TotalDistance => CalculateTotalDistance();

    private Distance CalculateEstimatedDistance()
    {
        var timeDuration = Duration as Duration.TimeDuration;
        long totalTicks = timeDuration?.Time.Ticks ?? 0;

        var paceIntensity = IntensityTarget as IntensityTarget.PaceIntensity;
        long avgPaceTicks = paceIntensity?.Average.Ticks ?? 0;

        decimal estimatedDistance = (decimal) totalTicks / (decimal) avgPaceTicks;

        return Distance.Kilometers(Math.Round(estimatedDistance, MidpointRounding.AwayFromZero));
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
        var paceIntensity = IntensityTarget as IntensityTarget.PaceIntensity;
        var distanceDuration = Duration as Duration.DistanceDuration;
        var averagePace = paceIntensity?.Average ?? TimeSpan.MinValue;
        var distance = distanceDuration?.Value ?? 0;
        var totalTicks = averagePace.Ticks * distance;

        return new TimeSpan((long) totalTicks);
    }

    public TimeSpan CalculateTotalTime()
    {
        var timeDuration = Duration as Duration.TimeDuration;
        var totalTicks = timeDuration?.Time.Ticks ?? 0;

        return new TimeSpan(totalTicks);
    }

    public class SimpleStepBuilder
    {
        private readonly SimpleStep _step;

        private SimpleStepBuilder()
        {
            _step = new SimpleStep();
        }

        public SimpleStepBuilder WithType(StepType type)
        {
            _step.Type = type;

            return this;
        }

        public SimpleStepBuilder WithDuration(Duration duration)
        {
            _step.Duration = duration;

            return this;
        }

        public SimpleStepBuilder WithIntensityTarget(IntensityTarget intensityTarget)
        {
            _step.IntensityTarget = intensityTarget;

            return this;
        }

        public SimpleStepBuilder WithKilometers(decimal distance)
        {
            var duration = Duration.ForKilometers(distance);

            WithDuration(duration);

            return this;
        }

        public SimpleStepBuilder WithPaceRange((TimeSpan paceMin, TimeSpan paceMax) paceRange)
        {
            var intensityTarget = IntensityTarget.Pace(paceRange.paceMin, paceRange.paceMax);

            WithIntensityTarget(intensityTarget);

            return this;
        }

        public SimpleStepBuilder WithNoTargetPaceRange()
        {
            var intensityTarget = IntensityTarget.None();

            WithIntensityTarget(intensityTarget);

            return this;
        }

        public SimpleStep Build()
        {
            if (_step.Type == StepType.Invalid || !Enum.IsDefined(_step.Type))
            {
                throw new ArgumentException("Invalid step type.");
            }

            if (_step.Duration is null)
            {
                throw new ArgumentException("Duration cannot be null.");
            }

            if (_step.IntensityTarget is null)
            {
                throw new ArgumentException("Intensity target cannot be null.");
            }

            return _step;
        }

        public static SimpleStepBuilder CreateBuilder() => new();
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
