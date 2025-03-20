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
        public bool IncludesSpeedwork { get; init; }
        public bool IncludesStrengthTraining { get; init; }
        public bool IncludesRacePaceRuns { get; init; }
        public bool IncludesHillWork { get; init; }
        public bool IncludesIntervals { get; init; }
        public bool IncludesTempoRuns { get; init; }
        public int NumberOfLongRuns { get; init; }
        public bool IncludesStepbackWeeks { get; init; }
        public bool RunsLongOnWeekends { get; init; }
        public bool HasMidweekMediumLongRun { get; init; }
        public string PrimaryLongRunDay { get; init; }
        public int TaperWeeks { get; init; }
        public string Description { get; init; }
        public string SuitableFor { get; init; }
        public Dictionary<string, object> AdditionalAttributes { get; init; } = new Dictionary<string, object>();
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
                    LongestRunDistance = 32.2m, // 20 miles in km
                    PeakWeeklyMileage = 64.4m, // ~40 miles in km
                    IncludesSpeedwork = false,
                    IncludesStrengthTraining = false,
                    IncludesRacePaceRuns = false,
                    IncludesHillWork = false,
                    IncludesIntervals = false,
                    IncludesTempoRuns = false,
                    NumberOfLongRuns = 18,
                    IncludesStepbackWeeks = true,
                    RunsLongOnWeekends = true,
                    HasMidweekMediumLongRun = true,
                    PrimaryLongRunDay = "Saturday",
                    TaperWeeks = 3,
                    Description = "This is Hal's most popular program: the Novice 1 Marathon Training Program. If you are training for your first marathon, this is the training program for you!",
                    SuitableFor = "First-time marathoners or experienced runners seeking a gentle approach to marathon training.",
                    AdditionalAttributes = new Dictionary<string, object>
                    {
                        { "NumberOfTwentyMilers", 1 }
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
                    LongestRunDistance = 32.2m, // 20 miles in km
                    PeakWeeklyMileage = 72.4m, // ~45 miles in km
                    IncludesSpeedwork = false,
                    IncludesStrengthTraining = false,
                    IncludesRacePaceRuns = true,
                    IncludesHillWork = false,
                    IncludesIntervals = false,
                    IncludesTempoRuns = false,
                    NumberOfLongRuns = 18,
                    IncludesStepbackWeeks = true,
                    RunsLongOnWeekends = true,
                    HasMidweekMediumLongRun = true,
                    PrimaryLongRunDay = "Saturday",
                    TaperWeeks = 3,
                    Description = "Novice 2 is designed to fit comfortably between the Novice 1 and Intermediate 1 marathon plans.",
                    SuitableFor = "Runners who may already have run and finished their first marathon and want to add just a bit more mileage while training for their second or third marathons.",
                    AdditionalAttributes = new Dictionary<string, object>
                    {
                        { "NumberOfTwentyMilers", 1 }
                    }
                },
                
                // Additional Hal Higdon plans would be defined here, following the same pattern
                
                _ => throw new ArgumentException($"Combination of level {level} and distance {distance} not supported")
            };
        }

        // Method to get all available Hal Higdon marathon plans
        public static List<PlanCharacteristics> GetAllMarathonPlans()
        {
            return new List<PlanCharacteristics>
            {
                Create(HalHigdonPlanLevel.Novice1, RaceDistance.Marathon),
                Create(HalHigdonPlanLevel.Novice2, RaceDistance.Marathon),
                // Add other plan types as needed
            };
        }
    }

    // Jack Daniels-specific extension
    public static class JackDanielsPlans
    {
        public enum JackDanielsPlanType
        {
            Novice,
            TwoQ,
            FourWeekCycle,
            FiveWeekCycle,
            Final18Weeks,
            Final12Weeks
        }

        // Factory method for Jack Daniels plans
        public static PlanCharacteristics Create(JackDanielsPlanType planType, RaceDistance distance)
        {
            // Implementation would be similar to Hal Higdon's but with Jack Daniels-specific attributes
            return new PlanCharacteristics(); // Placeholder - would be implemented similarly
        }
    }

    // Additional training plan systems could be implemented as static classes following the same pattern
}