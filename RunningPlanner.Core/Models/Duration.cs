namespace RunningPlanner.Core.Models;

public abstract record Duration
{
    // Private constructor to prevent direct instantiation
    private protected Duration() { }

    // Factory methods using static pattern
    public static Duration ForKilometers(decimal value) => new DistanceDuration(value, DistanceMetric.Kilometers);
    public static Duration ForMiles(decimal value) => new DistanceDuration(value, DistanceMetric.Miles);
    public static Duration ForTime(TimeSpan time) => new TimeDuration(time);

    // Specialized duration types
    public sealed record DistanceDuration : Duration
    {
        public decimal Value { get; }
        public DistanceMetric Metric { get; }

        internal DistanceDuration(decimal value, DistanceMetric metric)
        {
            if (value < 0)
            {
                throw new ArgumentException("Distance cannot be negative.");
            }

            if (metric is DistanceMetric.Invalid || !Enum.IsDefined(metric))
            {
                throw new ArgumentException("Invalid distance metric.");
            }

            Value = value;
            Metric = metric;
        }
    }

    public sealed record TimeDuration : Duration
    {
        public TimeSpan Time { get; }

        internal TimeDuration(TimeSpan time)
        {
            if (time < TimeSpan.Zero)
            {
                throw new ArgumentException("Time cannot be negative.");
            }

            Time = time;
        }
    }
}

public enum DurationType
{
    Time,
    Distance
}
