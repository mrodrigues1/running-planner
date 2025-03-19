namespace RunningPlanner.Core.Models;

public record Distance
{
    private Distance(decimal value, DistanceMetric metric)
    {
        if (value < 0)
        {
            throw new ArgumentException("Distance cannot be negative.");
        }

        if (metric is DistanceMetric.Invalid || !Enum.IsDefined(metric))
        {
            throw new ArgumentException("Invalid distance metric.");
        }
        
        DistanceValue = value;
        DistanceMetric = metric;
    }

    public decimal DistanceValue { get; }
    public DistanceMetric DistanceMetric { get; }

    // Factory methods for common use cases
    public static Distance Kilometers(decimal value) => new(value, DistanceMetric.Kilometers);
    public static Distance Miles(decimal value) => new(value, DistanceMetric.Miles);
    public static Distance Meters(decimal value) => new(value, DistanceMetric.Meters);
}


public enum DistanceMetric
{
    Invalid,
    Kilometers,
    Miles,
    Meters
}