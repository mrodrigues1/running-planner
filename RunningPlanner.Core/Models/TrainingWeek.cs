namespace RunningPlanner.Core.Models;

public class TrainingWeek
{
    private TrainingWeek()
    {
    }

    public int WeekNumber { get; private set; }
    public TrainingPhase TrainingPhase { get; private set; }
    public Workout Monday { get; private set; }
    public Workout Tuesday { get; private set; }
    public Workout Wednesday { get; private set; }
    public Workout Thursday { get; private set; }
    public Workout Friday { get; private set; }
    public Workout Saturday { get; private set; }
    public Workout Sunday { get; private set; }

    public decimal TotalKilometerMileage => CalculateTotalKilometerMileage();
    public TimeSpan TotalEstimatedTime => CalculateTotalEstimatedTime();

    public List<Workout> Workouts =>
    [
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    ];

    private decimal CalculateTotalKilometerMileage()
    {
        var workouts = new List<Workout>
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        };
        
        var totalKilometers = workouts.Sum(x => x.TotalDistance.DistanceValue);

        return Math.Round(totalKilometers, 1);
    }
    
    private TimeSpan CalculateTotalEstimatedTime()
    {
        var workouts = new List<Workout>
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        };
        
        var totalEstimatedTimeTicks = workouts.Sum(x => x.EstimatedTime.Ticks);

        return new TimeSpan(totalEstimatedTimeTicks);
    }

    public class TrainingWeekBuilder
    {
        private readonly TrainingWeek _trainingWeek;

        private TrainingWeekBuilder()
        {
            _trainingWeek = new TrainingWeek();
        }

        public TrainingWeekBuilder WithWeekNumber(int weekNumber)
        {
            _trainingWeek.WeekNumber = weekNumber;

            return this;
        }

        public TrainingWeekBuilder WithTrainingPhase(TrainingPhase trainingPhase)
        {
            _trainingWeek.TrainingPhase = trainingPhase;
            
            return this;
        }

        public TrainingWeekBuilder WithMonday(Workout monday)
        {
            _trainingWeek.Monday = monday;

            return this;
        }

        public TrainingWeekBuilder WithTuesday(Workout tuesday)
        {
            _trainingWeek.Tuesday = tuesday;

            return this;
        }

        public TrainingWeekBuilder WithWednesday(Workout wednesday)
        {
            _trainingWeek.Wednesday = wednesday;

            return this;
        }

        public TrainingWeekBuilder WithThursday(Workout thursday)
        {
            _trainingWeek.Thursday = thursday;

            return this;
        }

        public TrainingWeekBuilder WithFriday(Workout friday)
        {
            _trainingWeek.Friday = friday;

            return this;
        }

        public TrainingWeekBuilder WithSaturday(Workout saturday)
        {
            _trainingWeek.Saturday = saturday;

            return this;
        }

        public TrainingWeekBuilder WithSunday(Workout sunday)
        {
            _trainingWeek.Sunday = sunday;

            return this;
        }

        public TrainingWeek Build()
        {
            if (_trainingWeek.WeekNumber < 1)
            {
                throw new ArgumentException("Week number cannot be less than 1.");
            }

            if (!Enum.IsDefined(_trainingWeek.TrainingPhase)
                && _trainingWeek.TrainingPhase is TrainingPhase.Invalid)
            {
                throw new ArgumentException("Invalid training phase.");
            }

            if (_trainingWeek.Monday is null)
            {
                throw new ArgumentException("Monday cannot be null.");
            }

            if (_trainingWeek.Tuesday is null)
            {
                throw new ArgumentException("Tuesday cannot be null.");
            }

            if (_trainingWeek.Wednesday is null)
            {
                throw new ArgumentException("Wednesday cannot be null.");
            }

            if (_trainingWeek.Thursday is null)
            {
                throw new ArgumentException("Thursday cannot be null.");
            }

            if (_trainingWeek.Friday is null)
            {
                throw new ArgumentException("Friday cannot be null.");
            }

            if (_trainingWeek.Saturday is null)
            {
                throw new ArgumentException("Saturday cannot be null.");
            }

            if (_trainingWeek.Sunday is null)
            {
                throw new ArgumentException("Sunday cannot be null.");
            }

            return _trainingWeek;
        }

        public static TrainingWeekBuilder CreateBuilder() => new();
    }
}