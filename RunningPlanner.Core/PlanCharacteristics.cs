using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.TrainingPlans
{
    public enum RaceDistance
    {
        FiveK,
        TenK,
        HalfMarathon,
        Marathon,
        UltraMarathon
    }

    public enum ExperienceLevel
    {
        Beginner,
        Novice,
        Intermediate,
        Advanced,
        Elite
    }

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
            LongRunWeeklyPercentage { get; init; } // What percentage of weekly mileage comes from the long run

        public ProgressionStyle ProgressionStyle { get; init; }
        public bool HasMidweekMediumLongRun { get; init; }
        public DayOfWeek PrimaryLongRunDay { get; init; }


        // Training Specifics
        public List<WorkoutType> WorkoutTypes { get; init; } = new();
        public decimal WeeklyMileageIncrease { get; init; } // Average percentage increase in weekly mileage
        public bool RunsLongOnWeekends { get; set; }
        public int TaperWeeks { get; init; }

        public string Description { get; init; }
        public string SuitableFor { get; init; }
        public Dictionary<string, object> AdditionalAttributes { get; init; } = new Dictionary<string, object>();
    }

    public enum ProgressionStyle
    {
    }

    // Hal Higdon-specific extension of the generic plan characteristics
    public static class HalHigdonPlans
    {
        public enum HalHigdonPlanLevel
        {
            Novice1,
            Novice2,
            Intermediate1,
            Intermediate2,
            Advanced1,
            Advanced2
        }

        // Factory method to create Hal Higdon plans
        // In the HalHigdonPlans class, expand the Create method

        public static PlanCharacteristics Create(HalHigdonPlanLevel level, RaceDistance distance)
        {
            return (level, distance) switch
            {
                (HalHigdonPlanLevel.Novice1, RaceDistance.Marathon) => new PlanCharacteristics
                {
                    PlanName = "Marathon Novice 1",
                    Author = "Hal Higdon",
                    Level = ExperienceLevel.Beginner,
                    Distance = distance,
                    WeekCount = 18,
                    RunDaysPerWeek = 4,
                    CrossTrainingDaysPerWeek = 1,
                    RestDaysPerWeek = 2,
                    QualityWorkoutsPerWeek = 1,
                    WorkoutTypes = [WorkoutType.EasyRun, WorkoutType.MediumRun, WorkoutType.LongRun, WorkoutType.Race],
                    IncludesStrideWork = false,
                    IncludesProgressionRuns = false,
                    WeeklyMileageIncrease = 1,
                    LongRunWeeklyPercentage = 30,
                    LongestRunDistance = 32.5m, // 20 miles in km
                    PeakWeeklyMileage = 64.5m, // ~40 miles in km
                    IncludesSpeedWork = false,
                    IncludesStrengthTraining = false,
                    IncludesRacePaceRuns = false,
                    IncludesHillRepeatWork = false,
                    IncludesIntervals = false,
                    IncludesTempoRuns = false,
                    HasStepbackWeeks = true,
                    RunsLongOnWeekends = true,
                    HasMidweekMediumLongRun = true,
                    PrimaryLongRunDay = DayOfWeek.Saturday,
                    TaperWeeks = 3,
                    Description =
                        "This is Hal's most popular program: the Novice 1 Marathon Training Program. If you are training for your first marathon, this is the training program for you!",
                    SuitableFor =
                        "First-time marathoners or experienced runners seeking a gentle approach to marathon training.",
                    AdditionalAttributes = new Dictionary<string, object>
                    {
                        {"NumberOfTwentyMilers", 1}
                    }
                },

                (HalHigdonPlanLevel.Novice2, RaceDistance.Marathon) => new PlanCharacteristics
                {
                    PlanName = "Marathon Novice 2",
                    Author = "Hal Higdon",
                    Level = ExperienceLevel.Novice,
                    Distance = distance,
                    WeekCount = 18,
                    RunDaysPerWeek = 4,
                    CrossTrainingDaysPerWeek = 1,
                    RestDaysPerWeek = 2,
                    QualityWorkoutsPerWeek = 2,
                    WorkoutTypes =
                    [
                        WorkoutType.EasyRun, WorkoutType.MediumRun, WorkoutType.RacePace, WorkoutType.LongRun,
                        WorkoutType.Race
                    ],
                    IncludesStrideWork = false,
                    IncludesProgressionRuns = false,
                    WeeklyMileageIncrease = 1,
                    LongRunWeeklyPercentage = 30,
                    LongestRunDistance = 32.5m, // 20 miles in km
                    PeakWeeklyMileage = 72.5m, // ~45 miles in km
                    IncludesSpeedWork = false,
                    IncludesStrengthTraining = false,
                    IncludesRacePaceRuns = true,
                    IncludesHillRepeatWork = false,
                    IncludesIntervals = false,
                    IncludesTempoRuns = false,
                    HasStepbackWeeks = true,
                    RunsLongOnWeekends = true,
                    HasMidweekMediumLongRun = true,
                    PrimaryLongRunDay = DayOfWeek.Saturday,
                    TaperWeeks = 3,
                    Description =
                        "Novice 2 is designed to fit comfortably between the Novice 1 and Intermediate 1 marathon plans.",
                    SuitableFor =
                        "Runners who may already have run and finished their first marathon and want to add just a bit more mileage while training for their second or third marathons.",
                    AdditionalAttributes = new Dictionary<string, object>
                    {
                        {"NumberOfTwentyMilers", 1},
                        {"IncludesRacePaceWorkouts", true}
                    }
                },

                (HalHigdonPlanLevel.Intermediate1, RaceDistance.Marathon) => new PlanCharacteristics
                {
                    PlanName = "Marathon Intermediate 1",
                    Author = "Hal Higdon",
                    Level = ExperienceLevel.Intermediate,
                    Distance = distance,
                    WeekCount = 18,
                    RunDaysPerWeek = 5,
                    CrossTrainingDaysPerWeek = 1,
                    RestDaysPerWeek = 1,
                    QualityWorkoutsPerWeek = 2,
                    WorkoutTypes =
                    [
                        WorkoutType.EasyRun, WorkoutType.MediumRun, WorkoutType.RacePace, WorkoutType.LongRun,
                        WorkoutType.Race
                    ],
                    IncludesStrideWork = false,
                    IncludesProgressionRuns = false,
                    WeeklyMileageIncrease = 1,
                    LongRunWeeklyPercentage = 30,
                    LongestRunDistance = 32.5m, // 20 miles in km
                    PeakWeeklyMileage = 82.0m, // ~51 miles in km
                    IncludesSpeedWork = false,
                    IncludesStrengthTraining = false,
                    IncludesRacePaceRuns = true,
                    IncludesHillRepeatWork = false,
                    IncludesIntervals = false,
                    IncludesTempoRuns = false,
                    HasStepbackWeeks = true,
                    RunsLongOnWeekends = true,
                    HasMidweekMediumLongRun = true,
                    PrimaryLongRunDay = DayOfWeek.Sunday,
                    TaperWeeks = 3,
                    Description =
                        "A slight jump in difficulty from the Novice programs, with higher weekly mileage and pace runs on Saturdays.",
                    SuitableFor =
                        "Runners who have used novice programs for their first marathons and are looking to increase their training levels and improve their times.",
                    AdditionalAttributes = new Dictionary<string, object>
                    {
                        {"NumberOfTwentyMilers", 2},
                        {"CombinedWeekendMileage", "High"}
                    }
                },

                (HalHigdonPlanLevel.Intermediate2, RaceDistance.Marathon) => new PlanCharacteristics
                {
                    PlanName = "Marathon Intermediate 2",
                    Author = "Hal Higdon",
                    Level = ExperienceLevel.Intermediate,
                    Distance = distance,
                    WeekCount = 18,
                    RunDaysPerWeek = 5,
                    CrossTrainingDaysPerWeek = 1,
                    RestDaysPerWeek = 1,
                    QualityWorkoutsPerWeek = 2,
                    WorkoutTypes =
                    [
                        WorkoutType.EasyRun, WorkoutType.MediumRun, WorkoutType.RacePace, WorkoutType.LongRun,
                        WorkoutType.Race
                    ],
                    IncludesStrideWork = false,
                    IncludesProgressionRuns = false,
                    WeeklyMileageIncrease = 1,
                    LongRunWeeklyPercentage = 30,
                    LongestRunDistance = 32.5m, // 20 miles in km
                    PeakWeeklyMileage = 88.5m, // ~55 miles in km
                    IncludesSpeedWork = false,
                    IncludesStrengthTraining = false,
                    IncludesRacePaceRuns = true,
                    IncludesHillRepeatWork = false,
                    IncludesIntervals = false,
                    IncludesTempoRuns = false,
                    HasStepbackWeeks = true,
                    RunsLongOnWeekends = true,
                    HasMidweekMediumLongRun = true,
                    PrimaryLongRunDay = DayOfWeek.Sunday,
                    TaperWeeks = 3,
                    Description =
                        "A slight jump in difficulty from Intermediate 1, with three 20-milers toward the end of the program and higher overall mileage.",
                    SuitableFor =
                        "Intermediate runners who have completed several marathons and are looking for more challenging training.",
                    AdditionalAttributes = new Dictionary<string, object>
                    {
                        {"NumberOfTwentyMilers", 3},
                        {"IdealForGoofy", true} // Specifically mentioned as ideal for Disney's Goofy Challenge
                    }
                },

                (HalHigdonPlanLevel.Advanced1, RaceDistance.Marathon) => new PlanCharacteristics
                {
                    PlanName = "Marathon Advanced 1",
                    Author = "Hal Higdon",
                    Level = ExperienceLevel.Advanced,
                    Distance = distance,
                    WeekCount = 18,
                    RunDaysPerWeek = 6,
                    CrossTrainingDaysPerWeek = 0,
                    RestDaysPerWeek = 1,
                    QualityWorkoutsPerWeek = 3,
                    WorkoutTypes =
                    [
                        WorkoutType.EasyRun, 
                        WorkoutType.MediumRun, 
                        WorkoutType.RacePace,
                        WorkoutType.TempoRun,
                        WorkoutType.HillRepeat,
                        WorkoutType.LongRun,
                        WorkoutType.Race
                    ],
                    IncludesStrideWork = false,
                    IncludesProgressionRuns = false,
                    WeeklyMileageIncrease = 1,
                    LongRunWeeklyPercentage = 30,
                    LongestRunDistance = 32.5m, // 20 miles in km
                    PeakWeeklyMileage = 96.5m, // ~60 miles in km
                    IncludesSpeedWork = true,
                    IncludesStrengthTraining = false,
                    IncludesRacePaceRuns = true,
                    IncludesHillRepeatWork = true,
                    IncludesIntervals = true,
                    IncludesTempoRuns = true,
                    HasStepbackWeeks = true,
                    RunsLongOnWeekends = true,
                    HasMidweekMediumLongRun = true,
                    PrimaryLongRunDay = DayOfWeek.Sunday,
                    TaperWeeks = 3,
                    Description =
                        "A challenging program with a progressive buildup to three 20-milers, with one quality speed session per week.",
                    SuitableFor =
                        "Advanced runners with multiple marathons under their belt who want to incorporate speedwork into their training.",
                    AdditionalAttributes = new Dictionary<string, object>
                    {
                        {"NumberOfTwentyMilers", 3},
                        {"SpeedworkDaysPerWeek", 1}
                    }
                },

                (HalHigdonPlanLevel.Advanced2, RaceDistance.Marathon) => new PlanCharacteristics
                {
                    PlanName = "Marathon Advanced 2",
                    Author = "Hal Higdon",
                    Level = ExperienceLevel.Advanced,
                    Distance = distance,
                    WeekCount = 18,
                    RunDaysPerWeek = 6,
                    CrossTrainingDaysPerWeek = 0,
                    RestDaysPerWeek = 1,
                    QualityWorkoutsPerWeek = 4,
                    WorkoutTypes =
                    [
                        WorkoutType.EasyRun, 
                        WorkoutType.MediumRun, 
                        WorkoutType.RacePace,
                        WorkoutType.TempoRun,
                        WorkoutType.HillRepeat,
                        WorkoutType.Intervals,
                        WorkoutType.LongRun,
                        WorkoutType.Race
                    ],
                    IncludesStrideWork = false,
                    IncludesProgressionRuns = false,
                    WeeklyMileageIncrease = 1,
                    LongRunWeeklyPercentage = 30,
                    LongestRunDistance = 32.5m, // 20 miles in km
                    PeakWeeklyMileage = 113m, // ~70 miles in km
                    IncludesSpeedWork = true,
                    IncludesStrengthTraining = false,
                    IncludesRacePaceRuns = true,
                    IncludesHillRepeatWork = true,
                    IncludesIntervals = true,
                    IncludesTempoRuns = true,
                    HasStepbackWeeks = true,
                    RunsLongOnWeekends = true,
                    HasMidweekMediumLongRun = true,
                    PrimaryLongRunDay = DayOfWeek.Sunday,
                    TaperWeeks = 3,
                    Description =
                        "Hal's most difficult program with two quality speed sessions per week. Designed only for the hard core, those willing to take it to the limit.",
                    SuitableFor =
                        "Very experienced marathoners looking to maximize their performance through high mileage and multiple speed sessions.",
                    AdditionalAttributes = new Dictionary<string, object>
                    {
                        {"NumberOfTwentyMilers", 3},
                        {"SpeedworkDaysPerWeek", 2}
                    }
                },

                // Default case for unsupported combinations
                _ => throw new ArgumentException($"Combination of level {level} and distance {distance} not supported")
            };
        }

        // Method to get all available Hal Higdon marathon plans
        public static List<PlanCharacteristics> GetAllMarathonPlans()
        {
            return
            [
                Create(HalHigdonPlanLevel.Novice1, RaceDistance.Marathon),
                Create(HalHigdonPlanLevel.Novice2, RaceDistance.Marathon),
                Create(HalHigdonPlanLevel.Intermediate1, RaceDistance.Marathon),
                Create(HalHigdonPlanLevel.Intermediate2, RaceDistance.Marathon),
                Create(HalHigdonPlanLevel.Advanced1, RaceDistance.Marathon),
                Create(HalHigdonPlanLevel.Advanced2, RaceDistance.Marathon)
            ];
        }
    }
}
