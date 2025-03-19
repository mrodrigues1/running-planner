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

    private string BuildWorkoutName()
    {
        if (Type is WorkoutType.Rest)
        {
            return Type.ToString();
        }

        var workoutName = new List<string>();

        workoutName.Add(Type.ToString());

        var metric = Steps
            .SelectMany<Step, SimpleStep>(
                x => x.SimpleStep is not null
                    ? [x.SimpleStep]
                    : x.Repeat?.RepeatableSteps ?? [])
            .Select(
                x => x.Duration is Duration.DistanceDuration distanceDuration
                    ? distanceDuration.Metric
                    : DistanceMetric.Invalid)
            .FirstOrDefault();

        workoutName.Add(
            $"{TotalDistance.DistanceValue.ToString(CultureInfo.InvariantCulture)} {metric.ToString() ?? "km"}");

        return string.Join(" - ", workoutName);
    }

    private TimeSpan CalculateTotalTime()
    {
        var totalTicks = Steps
            .SelectMany<Step, SimpleStep>(
                x => x.SimpleStep is not null
                    ? [x.SimpleStep]
                    : x.Repeat?.RepeatableSteps ?? [])
            .Sum(step => step.TotalTime.Ticks);

        return new TimeSpan(totalTicks);
    }

    private TimeSpan CalculateEstimatedTime()
    {
        var totalEstimatedTicks = Steps
            .SelectMany<Step, SimpleStep>(
                x => x.SimpleStep is not null
                    ? [x.SimpleStep]
                    : x.Repeat?.RepeatableSteps ?? [])
            .Sum(step => step.EstimatedTime.Ticks);

        return new TimeSpan(totalEstimatedTicks);
    }

    private Distance CalculateTotalDistance()
    {
        var distance = Steps
            .SelectMany<Step, SimpleStep>(
                x => x.SimpleStep is not null
                    ? [x.SimpleStep]
                    : x.Repeat?.RepeatableSteps ?? [])
            .Sum(step => step.TotalDistance.DistanceValue);

        return Distance.Kilometers(Math.Round(distance, 1, MidpointRounding.AwayFromZero));
    }

    private Distance CalculateEstimatedDistance()
    {
        var estimatedDistance = Steps
            .SelectMany<Step, SimpleStep>(
                x => x.SimpleStep is not null
                    ? [x.SimpleStep]
                    : x.Repeat?.RepeatableSteps ?? [])
            .Sum(step => step.EstimatedDistance.DistanceValue);

        return Distance.Kilometers(estimatedDistance);
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
            if (_workout.Type is WorkoutType.Invalid || !Enum.IsDefined(_workout.Type))
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
            if (_workout.Type is WorkoutType.Invalid || !Enum.IsDefined(_workout.Type))
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

        public Workout BuildRepeatWorkout()
        {
            if (_workout.Type is WorkoutType.Invalid || !Enum.IsDefined(_workout.Type))
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

            if (_workout.Steps.Count < 1)
            {
                throw new ArgumentException("Workout must contain at least one step.");
            }

            if (string.IsNullOrEmpty(_workout.Name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }

            return _workout;
        }

        public Workout BuildRestWorkout()
        {
            if (_workout.Type is WorkoutType.Invalid || !Enum.IsDefined(_workout.Type))
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

        public WorkoutBuilder WithSimpleRunStep(decimal distance, (TimeSpan min, TimeSpan max) pace)
        {
            var simpleStep = SimpleStep.CreateWithKilometers(
                StepType.Run,
                distance,
                IntensityTarget.Pace(pace.min, pace.max));

            var step = Step.FromSimpleStep(simpleStep);

            WithStep(step);

            return this;
        }

        public WorkoutBuilder WithSimpleStep(
            StepType stepType,
            decimal distance,
            (TimeSpan min, TimeSpan max) pace)
        {
            var simpleStep = SimpleStep.CreateWithKilometers(
                stepType,
                distance,
                IntensityTarget.Pace(pace.min, pace.max));

            var step = Step.FromSimpleStep(simpleStep);

            WithStep(step);

            return this;
        }

        public WorkoutBuilder WithRaceStep(decimal distance)
        {
            var simpleStep = SimpleStep.CreateWithKilometers(StepType.Run, distance, IntensityTarget.None());

            var step = Step.FromSimpleStep(simpleStep);

            WithStep(step);

            return this;
        }

        public WorkoutBuilder WithRepeatStep(
            int repeats,
            decimal repeatDistance,
            decimal restDistance,
            (TimeSpan min, TimeSpan max) intervalPace,
            (TimeSpan min, TimeSpan max) restPace)
        {
            var steps = new List<SimpleStep>();

            for (var i = 0; i < repeats; i++)
            {
                var runStep = SimpleStep
                    .CreateWithKilometers(
                        StepType.Run,
                        repeatDistance,
                        IntensityTarget.Pace(intervalPace.min, intervalPace.max));

                var restStep = SimpleStep
                    .CreateWithKilometers(
                        StepType.Recover,
                        restDistance,
                        IntensityTarget.Pace(restPace.min, restPace.max));

                steps.Add(runStep);
                steps.Add(restStep);
            }

            var totalRepeats = repeats * 2;

            var repeat = Repeat.Create(totalRepeats, steps);

            var step = Step.FromRepeat(repeat);

            WithStep(step);

            return this;
        }

        public Workout BuildRaceWorkout()
        {
            if (_workout.Type is WorkoutType.Invalid || !Enum.IsDefined(_workout.Type))
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
    WalkRun,
    EasyRun,
    MediumRun,
    Repetition,
    Intervals,
    Threshold,
    ThresholdRepeat,
    TempoRun,
    TempoRunRepeat,
    Fartlek,
    HillRepeat,
    RacePace,
    LongRun,
    Race
}
