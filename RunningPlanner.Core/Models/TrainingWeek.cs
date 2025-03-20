namespace RunningPlanner.Core.Models;

public record TrainingWeek
{
    public int WeekNumber { get; init; }
    public TrainingPhase TrainingPhase { get; init; }
    public Workout? Monday { get; init; }
    public Workout? Tuesday { get; init; }
    public Workout? Wednesday { get; init; }
    public Workout? Thursday { get; init; }
    public Workout? Friday { get; init; }
    public Workout? Saturday { get; init; }
    public Workout? Sunday { get; init; }

    // Computed properties
    public decimal TotalKilometerMileage => CalculateTotalKilometerMileage();
    public TimeSpan TotalEstimatedTime => CalculateTotalEstimatedTime();

    // Collection of all workouts in the week
    public IReadOnlyList<Workout> Workouts => new[]
        {
            Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
        }.Where(w => w != null)
        .Cast<Workout>()
        .ToList()
        .AsReadOnly();

    // Optional parameterless constructor
    private TrainingWeek()
    {
    }

    // Factory method with named parameters for clarity
    public static TrainingWeek Create(
        int weekNumber,
        TrainingPhase trainingPhase,
        Workout? monday = null,
        Workout? tuesday = null,
        Workout? wednesday = null,
        Workout? thursday = null,
        Workout? friday = null,
        Workout? saturday = null,
        Workout? sunday = null)
    {
        return new TrainingWeek
        {
            WeekNumber = weekNumber,
            TrainingPhase = trainingPhase,
            Monday = monday,
            Tuesday = tuesday,
            Wednesday = wednesday,
            Thursday = thursday,
            Friday = friday,
            Saturday = saturday,
            Sunday = sunday
        };
    }

    private decimal CalculateTotalKilometerMileage()
    {
        // Implement calculation logic using Workouts collection
        return Workouts.Sum(w => w.TotalDistance.DistanceValue);
    }

    private TimeSpan CalculateTotalEstimatedTime()
    {
        // Implement calculation logic using Workouts collection
        return TimeSpan.FromTicks(Workouts.Sum(w => w.EstimatedTime.Ticks));
    }
}
