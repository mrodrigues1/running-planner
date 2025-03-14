namespace RunningPlanner.Core.Models;

public class Duration
{
    private Duration()
    {
    }

    public DurationType Type { get; private set; }
    public decimal? DistanceValue { get; private set; }
    public DistanceMetric DistanceMetric { get; private set; }
    public TimeSpan? Time { get; private set; }

    public class DurationBuilder
    {
        private readonly Duration _duration;

        private DurationBuilder()
        {
            _duration = new Duration();
        }

        public DurationBuilder WithType(DurationType type)
        {
            _duration.Type = type;

            return this;
        }
        
        public DurationBuilder WithKilometers(decimal value)
        {
            _duration.Type = DurationType.Distance;
            _duration.DistanceMetric = DistanceMetric.Kilometers;

            _duration.DistanceValue = value;

            return this;
        }

        public DurationBuilder WithMiles(decimal value)
        {
            _duration.Type = DurationType.Distance;
            _duration.DistanceMetric = DistanceMetric.Miles;

            _duration.DistanceValue = value;

            return this;
        }

        public DurationBuilder WithTime(TimeSpan time)
        {
            _duration.Type = DurationType.Time;

            _duration.Time = time;

            return this;
        }

        public Duration Build()
        {
            if (_duration.Type is DurationType.Invalid
                || !Enum.IsDefined(_duration.Type))
            {
                throw new ArgumentException("Invalid duration type.");
            }

            if (_duration.DistanceValue is null && _duration.Time is null)
            {
                throw new ArgumentException("Duration must contain either distance or time.");
            }

            if (_duration.DistanceValue is not null && _duration.Time is not null)
            {
                throw new ArgumentException("Duration cannot contain both distance and time.");
            }

            return _duration;
        }

        public static DurationBuilder CreateBuilder() => new();
    }
}

public enum DurationType
{
    Invalid,
    Time,
    Distance
}