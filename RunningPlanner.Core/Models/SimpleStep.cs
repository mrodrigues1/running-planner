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
        long totalTicks = Duration.Time?.Ticks ?? 0;

        long avgPaceTicks = IntensityTarget?.Average.Ticks ?? 0;

        decimal paceTicks = (decimal) totalTicks / (decimal) avgPaceTicks;
        var calculateEstimatedDistance = Math.Round(paceTicks, MidpointRounding.AwayFromZero);

        return Distance.DistanceBuilder
            .CreateBuilder()
            .WithKilometers(calculateEstimatedDistance)
            .Build();
    }

    private Distance CalculateTotalDistance()
    {
        var stepDistance = Duration?.DistanceValue ?? 0;

        var distanceMetric = Duration?.DistanceMetric ?? DistanceMetric.Invalid;

        return distanceMetric is DistanceMetric.Kilometers
            ? Distance.DistanceBuilder
                .CreateBuilder()
                .WithKilometers(stepDistance)
                .Build()
            : Distance.DistanceBuilder
                .CreateBuilder()
                .WithMiles(stepDistance)
                .Build();
    }

    private TimeSpan CalculateEstimatedTime()
    {
        var averagePace = IntensityTarget?.Average ?? TimeSpan.MinValue;
        var distance = Duration?.DistanceValue ?? 0;
        var totalTicks = averagePace.Ticks * distance;

        return new TimeSpan((long) totalTicks);
    }

    public TimeSpan CalculateTotalTime()
    {
        var totalTicks = Duration.Time?.Ticks ?? 0;

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
            var duration = Duration.DurationBuilder
                .CreateBuilder()
                .WithKilometers(distance)
                .Build();

            WithDuration(duration);
            
            return this;
        }

        public SimpleStepBuilder WithPaceRange((TimeSpan paceMin, TimeSpan paceMax) paceRange)
        {
            var intensityTarget = IntensityTarget.IntensityTargetBuilder
                .CreateBuilder()
                .WithType(IntensityTargetType.Pace)
                .WithPaceRange(paceRange.paceMin, paceRange.paceMax)
                .Build();

            WithIntensityTarget(intensityTarget);
            
            return this;
        }
        
        public SimpleStepBuilder WithNoTargetPaceRange()
        {
            var intensityTarget = IntensityTarget.IntensityTargetBuilder
                .CreateBuilder()
                .WithType(IntensityTargetType.NoTarget)
                .Build();

            WithIntensityTarget(intensityTarget);
            
            return this;
        }

        public SimpleStep Build()
        {
            if (_step.Type == StepType.Invalid
                || !Enum.IsDefined(_step.Type))
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