namespace RunningPlanner.Core.Models;

public class IntensityTarget
{
    private IntensityTarget()
    {
    }

    public IntensityTargetType Type { get; private set; }
    public TimeSpan Min { get; private set; }
    public TimeSpan Max { get; private set; }
    public TimeSpan Average => CalculateAverage();

    private TimeSpan CalculateAverage()
    {
        return Mean(new List<TimeSpan> { Min, Max });
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

    public class IntensityTargetBuilder
    {
        private readonly IntensityTarget _intensityTarget;

        private IntensityTargetBuilder()
        {
            _intensityTarget = new IntensityTarget();
        }

        public IntensityTargetBuilder WithType(IntensityTargetType type)
        {
            _intensityTarget.Type = type;

            return this;
        }

        public IntensityTargetBuilder WithPaceRange(TimeSpan min, TimeSpan max)
        {
            _intensityTarget.Type = IntensityTargetType.Pace;
            _intensityTarget.Min = min;
            _intensityTarget.Max = max;

            return this;
        }

        public IntensityTarget Build()
        {
            if (!Enum.IsDefined(_intensityTarget.Type))
            {
                throw new ArgumentException("Invalid intensity target type.");
            }

            if (_intensityTarget.Type is IntensityTargetType.Pace
                && _intensityTarget.Min == TimeSpan.MinValue
                && _intensityTarget.Max == TimeSpan.MinValue)
            {
                throw new ArgumentException("Intensity target must contain a pace range.");
            }

            return _intensityTarget;
        }

        public static IntensityTargetBuilder CreateBuilder() => new();
    }
}

public enum IntensityTargetType
{
    NoTarget,
    Pace
}