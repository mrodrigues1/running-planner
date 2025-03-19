namespace RunningPlanner.Core.Models;

public abstract record IntensityTarget
{
    // Private constructor to prevent direct instantiation
    private protected IntensityTarget()
    {
    }

    // Factory methods for creating different types of intensity targets
    public static IntensityTarget None() => new NoTargetIntensity();
    public static IntensityTarget Pace(TimeSpan min, TimeSpan max) => new PaceIntensity(min, max);

    // Concrete implementations
    public sealed record NoTargetIntensity : IntensityTarget
    {
        internal NoTargetIntensity()
        {
        }
    }

    public sealed record PaceIntensity : IntensityTarget
    {
        public TimeSpan Min { get; }
        public TimeSpan Max { get; }
        public TimeSpan Average => CalculateAverage();

        internal PaceIntensity(TimeSpan min, TimeSpan max)
        {
            if (min > max)
            {
                throw new ArgumentException("Minimum pace must be less than or equal to maximum pace.");
            }

            if (min <= TimeSpan.Zero)
            {
                throw new ArgumentException("Pace must be greater than zero.");
            }

            Min = min;
            Max = max;
        }

        private TimeSpan CalculateAverage()
        {
            return Mean(new List<TimeSpan> {Min, Max});
        }

        private static TimeSpan Mean(ICollection<TimeSpan> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            long mean = 0L;
            long remainder = 0L;
            int n = source.Count;

            foreach (var item in source)
            {
                long ticks = item.Ticks;
                mean += ticks / n;
                remainder += ticks % n;
                mean += remainder / n;
                remainder %= n;
            }

            return TimeSpan.FromTicks(mean);
        }
    }
}
