namespace RunningPlanner.Core.Models;

public class Distance
{
    private Distance()
    {
    }

    public decimal DistanceValue { get; private set; }
    public DistanceMetric DistanceMetric { get; private set; }

    public class DistanceBuilder
    {
        private readonly Distance _distance;

        public DistanceBuilder()
        {
            _distance = new Distance();
        }

        public DistanceBuilder WithValue(decimal value)
        {
            _distance.DistanceValue = value;

            return this;
        }

        public DistanceBuilder WithMetric(DistanceMetric metric)
        {
            _distance.DistanceMetric = metric;

            return this;
        }

        public DistanceBuilder WithKilometers(decimal value)
        {
            _distance.DistanceMetric = DistanceMetric.Kilometers;

            _distance.DistanceValue = value;

            return this;
        }

        public DistanceBuilder WithMiles(decimal value)
        {
            _distance.DistanceMetric = DistanceMetric.Miles;

            _distance.DistanceValue = value;

            return this;
        }

        public Distance Build()
        {
            if (_distance.DistanceValue < 0)
            {
                throw new ArgumentException("Distance cannot be negative.");
            }

            if (_distance.DistanceMetric is DistanceMetric.Invalid
                || !Enum.IsDefined(_distance.DistanceMetric))
            {
                throw new ArgumentException("Invalid distance metric.");
            }

            return _distance;
        }

        public static DistanceBuilder CreateBuilder() => new();
    }
}

public enum DistanceMetric
{
    Invalid,
    Kilometers,
    Miles,
    Meters
}