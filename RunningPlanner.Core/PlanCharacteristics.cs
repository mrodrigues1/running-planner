using RunningPlanner.Core.Models;

namespace RunningPlanner.Core
{
    // Base generic plan characteristics class that can be used for any training plan system
    public record PlanCharacteristics
    {
        // Basic Information
        public string PlanName { get; init; }
        public string Author { get; init; }
        public ExperienceLevel Level { get; init; }
        public RaceDistance Distance { get; init; }
        public int WeekCount { get; init; }
        public int RunDaysPerWeek { get; init; }
        public int CrossTrainingDaysPerWeek { get; init; }
        public int RestDaysPerWeek { get; init; }
        public decimal LongestRunDistance { get; init; }
        public decimal PeakWeeklyMileage { get; init; }


        // Workout Types
        public bool IncludesSpeedWork { get; init; }
        public bool IncludesStrengthTraining { get; init; }
        public bool IncludesRacePaceRuns { get; init; }
        public bool IncludesHillRepeatWork { get; init; }
        public bool IncludesIntervals { get; init; }
        public bool IncludesTempoRuns { get; init; }
        public bool IncludesStrideWork { get; init; }
        public bool IncludesProgressionRuns { get; init; }

        // Plan Structure Information
        public bool HasStepbackWeeks { get; init; }
        public int QualityWorkoutsPerWeek { get; init; }

        public decimal
            LongRunWeeklyPercentageAverage { get; init; } // What percentage of weekly mileage comes from the long run

        public ProgressionStyle ProgressionStyle { get; init; }
        public bool HasMidweekMediumLongRun { get; init; }
        public DayOfWeek PrimaryLongRunDay { get; init; }


        // Training Specifics
        public List<WorkoutType> WorkoutTypes { get; init; } = new();
        public bool RunsLongOnWeekends { get; set; }
        public int TaperWeeks { get; init; }
        public decimal AverageWeeklyDistance { get; set; }
        public decimal TotalDistance { get; set; }
        public bool HasTuneUpRace { get; set; }
        public List<Periodization> TrainingPeriodization { get; set; }
        public List<SampleWeek> SampleWeekWorkoutTypes { get; set; }
        public List<SampleWorkoutByPhase> SampleWorkoutByPhases { get; set; }

        public string Description { get; init; }
        public string SuitableFor { get; init; }
        public Dictionary<string, object> AdditionalAttributes { get; init; } = new();
    }

    public enum ProgressionStyle
    {
    }

    public record Periodization(int Week, TrainingPhase TrainingPhase);

    public record SampleWeek(DayOfWeek DayOfWeek, WorkoutType[] WorkoutTypes);

    public record SampleWorkoutByPhase(TrainingPhase TrainingPhase, WorkoutSample[] Workouts);

    // Hal Higdon-specific extension of the generic plan characteristics
}
