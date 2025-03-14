using System.Globalization;

namespace RunningPlanner.Core.Models;

public class Workout
{
    private Workout()
    {
    }

    public WorkoutType Type { get; private set; }
    public List<Step> Steps { get; private set; }
    public string Name => BuildWorkoutName();
    public TimeSpan TotalTime => CalculateTotalTime();
    public TimeSpan EstimatedTime => CalculateEstimatedTime();
    public Distance TotalDistance => CalculateTotalDistance();
    public Distance EstimatedDistance => CalculateEstimatedDistance();
    
    public string BuildWorkoutName()
    {
        if (Type is WorkoutType.Rest)
        {
            return Type.ToString();
        }
        
        var workoutName = new List<string>();

        workoutName.Add(Type.ToString());

        var metric = Steps
            .SelectMany<Step, SimpleStep>(x => x.SimpleStep is not null
                ? new[] {x.SimpleStep}
                : x.Repeat?.Steps
                  ?? [])
            .Select(x => x.Duration?.DistanceMetric)
            .First();

        workoutName.Add($"{TotalDistance.DistanceValue.ToString(CultureInfo.InvariantCulture)} {metric.ToString()}");

        return string.Join(" - ", workoutName);
    }
    
    private TimeSpan CalculateTotalTime()
    {
        var totalTicks = Steps
            .SelectMany<Step, SimpleStep>(x => x.SimpleStep is not null
                ? new[] {x.SimpleStep}
                : x.Repeat?.Steps
                  ?? [])
            .Sum(step => step.TotalTime.Ticks);

        return new TimeSpan(totalTicks);
    }

    private TimeSpan CalculateEstimatedTime()
    {
        var totalEstimatedTicks = Steps
            .SelectMany<Step, SimpleStep>(x => x.SimpleStep is not null
                ? new[] {x.SimpleStep}
                : x.Repeat?.Steps
                  ?? [])
            .Sum(step => step.EstimatedTime.Ticks);

        return new TimeSpan(totalEstimatedTicks);
    }
    
    private Distance CalculateTotalDistance()
    {
        var distance = Steps
            .SelectMany<Step, SimpleStep>(x => x.SimpleStep is not null
                ? new[] {x.SimpleStep}
                : x.Repeat?.Steps
                  ?? [])
            .Sum(step => step.TotalDistance.DistanceValue);

        return Distance.DistanceBuilder
            .CreateBuilder()
            .WithKilometers(distance)
            .Build();
    }
    
    private Distance CalculateEstimatedDistance()
    {
        var estimatedDistance = Steps
            .SelectMany<Step, SimpleStep>(x => x.SimpleStep is not null
                ? new[] { x.SimpleStep }
                : x.Repeat?.Steps
                  ?? [])
            .Sum(step => step.EstimatedDistance.DistanceValue);

        return Distance.DistanceBuilder
            .CreateBuilder()
            .WithKilometers(estimatedDistance)
            .Build();
    }


    public class WorkoutBuilder
    {
        private readonly Workout _workout;

        private WorkoutBuilder()
        {
            _workout = new Workout
            {
                Steps = []
            };
        }

        public WorkoutBuilder WithType(WorkoutType type)
        {
            _workout.Type = type;

            return this;
        }

        public WorkoutBuilder WithStep(Step step)
        {
            _workout.Steps.Add(step);

            return this;
        }

        public Workout Build()
        {
            if (_workout.Type is WorkoutType.Invalid
                || !Enum.IsDefined(_workout.Type))
            {
                throw new ArgumentException("Invalid workout type.");
            }

            if (_workout.Type is not WorkoutType.Rest && _workout.Steps is null)
            {
                throw new ArgumentException("Steps cannot be null.");
            }

            if (_workout.Type is not WorkoutType.Rest && _workout.Steps.Count < 1)
            {
                throw new ArgumentException("Workout must contain at least one step.");
            }

            if (string.IsNullOrEmpty(_workout.Name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }

            return _workout;
        }
        
        public Workout BuildSimpleWorkout()
        {
            if (_workout.Type is WorkoutType.Invalid
                || !Enum.IsDefined(_workout.Type))
            {
                throw new ArgumentException("Invalid workout type.");
            }

            if (_workout.Type is WorkoutType.Rest)
            {
                throw new ArgumentException("Workout type must not be rest.");
            }

            if (_workout.Steps is null)
            {
                throw new ArgumentException("Steps cannot be null.");
            }

            if (_workout.Steps.Count is not 1)
            {  
                throw new ArgumentException("Workout must contain exactly one step.");
            }

            if (string.IsNullOrEmpty(_workout.Name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }

            return _workout;
        }
        
        public Workout BuildRestWorkout()
        {
            if (_workout.Type is WorkoutType.Invalid
                || !Enum.IsDefined(_workout.Type))
            {
                throw new ArgumentException("Invalid workout type.");
            }

            if (_workout.Type is not WorkoutType.Rest)
            {
                throw new ArgumentException("Workout type must be rest.");
            }

            if (_workout.Steps.Count > 0)
            {
                throw new ArgumentException("Workout must not contain any steps.");
            }

            if (string.IsNullOrEmpty(_workout.Name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }

            return _workout;
        }
        
        public Workout BuildRaceWorkout()
        {
            if (_workout.Type is WorkoutType.Invalid
                || !Enum.IsDefined(_workout.Type))
            {
                throw new ArgumentException("Invalid workout type.");
            }

            if (_workout.Type is not WorkoutType.Race)
            {
                throw new ArgumentException("Workout type must be race.");
            }

            if (_workout.Steps.Count is not 1)
            {
                throw new ArgumentException("Workout must contain one step.");
            }

            if (string.IsNullOrEmpty(_workout.Name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }

            return _workout;
        }

        public static WorkoutBuilder CreateBuilder() => new();
    }
}

public enum WorkoutType
{
    Invalid,
    Rest,
    EasyRun,
    MediumRun,
    Repetition,
    Intervals,
    Threshold,
    Fartlek,
    RacePace,
    LongRun,
    Race
}