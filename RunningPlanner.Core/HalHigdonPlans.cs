using RunningPlanner.Core.Models;
using RunningPlanner.Core.TrainingPlans;

namespace RunningPlanner.Core;

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
                LongRunWeeklyPercentageAverage = 30,
                LongestRunDistance = 32.5m, // 20 miles in km
                PeakWeeklyMileage = 64.5m, // ~40 miles in km
                AverageWeeklyDistance = 41m,
                TotalDistance = 742m,
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
                TaperWeeks = 2,
                HasTuneUpRace = true,
                TrainingPeriodization = HalHigdonTrainingPeriodization(),
                SampleWeekWorkoutTypes =
                [
                    new(DayOfWeek.Monday, [WorkoutType.Rest]),
                    new(DayOfWeek.Tuesday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Wednesday, [WorkoutType.MediumRun]),
                    new(DayOfWeek.Thursday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Friday, [WorkoutType.Rest]),
                    new(DayOfWeek.Saturday, [WorkoutType.LongRun]),
                    new(DayOfWeek.Sunday, [WorkoutType.Cross])
                ],
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
                LongRunWeeklyPercentageAverage = 30,
                LongestRunDistance = 32.5m, // 20 miles in km
                PeakWeeklyMileage = 58m, // ~45 miles in km
                AverageWeeklyDistance = 44m,
                TotalDistance = 787m,
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
                TaperWeeks = 2,
                HasTuneUpRace = true,
                TrainingPeriodization = HalHigdonTrainingPeriodization(),
                SampleWeekWorkoutTypes =
                [
                    new(DayOfWeek.Monday, [WorkoutType.Rest]),
                    new(DayOfWeek.Tuesday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Wednesday, [WorkoutType.MediumRun, WorkoutType.RacePace]),
                    new(DayOfWeek.Thursday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Friday, [WorkoutType.Rest]),
                    new(DayOfWeek.Saturday, [WorkoutType.LongRun]),
                    new(DayOfWeek.Sunday, [WorkoutType.Cross])
                ],
                SampleWorkoutByPhases =
                [
                    new SampleWorkoutByPhase(
                        TrainingPhase.Base,
                        [
                            WorkoutSample.CreateRacePace(8m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Build,
                        [
                            WorkoutSample.CreateRacePace(10m),
                            WorkoutSample.CreateRacePace(11.5m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Peak,
                        [
                            WorkoutSample.CreateRacePace(13m),
                            WorkoutSample.CreateRacePace(8m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Taper,
                        [
                            WorkoutSample.CreateRacePace(6.5m)
                        ])
                ],
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
                LongRunWeeklyPercentageAverage = 30,
                LongestRunDistance = 32.5m,
                PeakWeeklyMileage = 71m,
                AverageWeeklyDistance = 53m,
                TotalDistance = 947m,
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
                TaperWeeks = 2,
                HasTuneUpRace = true,
                TrainingPeriodization = HalHigdonTrainingPeriodization(),
                SampleWeekWorkoutTypes =
                [
                    new(DayOfWeek.Monday, [WorkoutType.Cross]),
                    new(DayOfWeek.Tuesday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Wednesday, [WorkoutType.MediumRun]),
                    new(DayOfWeek.Thursday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Friday, [WorkoutType.Rest]),
                    new(DayOfWeek.Saturday, [WorkoutType.MediumRun, WorkoutType.RacePace]),
                    new(DayOfWeek.Sunday, [WorkoutType.LongRun])
                ],
                SampleWorkoutByPhases =
                [
                    new SampleWorkoutByPhase(
                        TrainingPhase.Base,
                        [
                            WorkoutSample.CreateRacePace(8m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Build,
                        [
                            WorkoutSample.CreateRacePace(10m),
                            WorkoutSample.CreateRacePace(11.5m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Peak,
                        [
                            WorkoutSample.CreateRacePace(13m),
                            WorkoutSample.CreateRacePace(8m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Taper,
                        [
                            WorkoutSample.CreateRacePace(6.5m)
                        ])
                ],
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
                LongRunWeeklyPercentageAverage = 30,
                LongestRunDistance = 32.5m,
                PeakWeeklyMileage = 80.5m,
                AverageWeeklyDistance = 58m,
                TotalDistance = 1036m,
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
                TaperWeeks = 2,
                HasTuneUpRace = true,
                TrainingPeriodization = HalHigdonTrainingPeriodization(),
                SampleWeekWorkoutTypes =
                [
                    new(DayOfWeek.Monday, [WorkoutType.Cross]),
                    new(DayOfWeek.Tuesday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Wednesday, [WorkoutType.MediumRun]),
                    new(DayOfWeek.Thursday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Friday, [WorkoutType.Rest]),
                    new(DayOfWeek.Saturday, [WorkoutType.MediumRun, WorkoutType.RacePace]),
                    new(DayOfWeek.Sunday, [WorkoutType.LongRun])
                ],
                SampleWorkoutByPhases =
                [
                    new SampleWorkoutByPhase(
                        TrainingPhase.Base,
                        [
                            WorkoutSample.CreateRacePace(8m),
                            WorkoutSample.CreateRacePace(10m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Build,
                        [
                            WorkoutSample.CreateRacePace(10m),
                            WorkoutSample.CreateRacePace(11.5m),
                            WorkoutSample.CreateRacePace(13m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Peak,
                        [
                            WorkoutSample.CreateRacePace(10m),
                            WorkoutSample.CreateRacePace(15m),
                            WorkoutSample.CreateRacePace(16m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Taper,
                        [
                            WorkoutSample.CreateRacePace(6.5m)
                        ])
                ],
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
                LongRunWeeklyPercentageAverage = 30,
                LongestRunDistance = 32.5m,
                PeakWeeklyMileage = 94m,
                AverageWeeklyDistance = 67m,
                TotalDistance = 1206m,
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
                TaperWeeks = 2,
                HasTuneUpRace = true,
                TrainingPeriodization = HalHigdonTrainingPeriodization(),
                SampleWeekWorkoutTypes =
                [
                    new(DayOfWeek.Monday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Tuesday, [WorkoutType.MediumRun]),
                    new(DayOfWeek.Wednesday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Thursday, [WorkoutType.HillRepeat, WorkoutType.TempoRun, WorkoutType.Intervals]),
                    new(DayOfWeek.Friday, [WorkoutType.Rest]),
                    new(DayOfWeek.Saturday, [WorkoutType.MediumRun, WorkoutType.RacePace]),
                    new(DayOfWeek.Sunday, [WorkoutType.LongRun])
                ],
                SampleWorkoutByPhases =
                [
                    new SampleWorkoutByPhase(
                        TrainingPhase.Base,
                        [
                            WorkoutSample.CreateRacePace(8m),
                            WorkoutSample.CreateRacePace(10m),
                            WorkoutSample.CreateHillRepeat(
                                3,
                                0.4m,
                                0.4m,
                                8m),
                            WorkoutSample.CreateTempo(30),
                            WorkoutSample.CreateInterval(
                                4,
                                800,
                                400,
                                8m,
                                restBeforeStartIntervalDistance: 0.2m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Build,
                        [
                            WorkoutSample.CreateRacePace(10m),
                            WorkoutSample.CreateRacePace(11.5m),
                            WorkoutSample.CreateRacePace(13m),
                            WorkoutSample.CreateHillRepeat(
                                4,
                                0.4m,
                                0.4m,
                                8m),
                            WorkoutSample.CreateHillRepeat(
                                5,
                                0.4m,
                                0.4m,
                                8m),
                            WorkoutSample.CreateTempo(35),
                            WorkoutSample.CreateTempo(40),
                            WorkoutSample.CreateInterval(
                                5,
                                800,
                                400,
                                10m,
                                restBeforeStartIntervalDistance: 0.2m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Peak,
                        [
                            WorkoutSample.CreateRacePace(10m),
                            WorkoutSample.CreateRacePace(15m),
                            WorkoutSample.CreateRacePace(16m),
                            WorkoutSample.CreateHillRepeat(
                                6,
                                0.4m,
                                0.4m,
                                8m),
                            WorkoutSample.CreateHillRepeat(
                                7,
                                0.4m,
                                0.4m,
                                10m),
                            WorkoutSample.CreateTempo(45),
                            WorkoutSample.CreateInterval(
                                6,
                                800,
                                400,
                                10m,
                                restBeforeStartIntervalDistance: 0.2m),
                            WorkoutSample.CreateInterval(
                                7,
                                800,
                                400,
                                10m,
                                restBeforeStartIntervalDistance: 0.2m),
                            WorkoutSample.CreateInterval(
                                8,
                                800,
                                400,
                                10m,
                                restBeforeStartIntervalDistance: 0.2m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Taper,
                        [
                            WorkoutSample.CreateRacePace(6.5m)
                        ])
                ],
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
                LongRunWeeklyPercentageAverage = 30,
                LongestRunDistance = 32.5m,
                PeakWeeklyMileage = 97m,
                AverageWeeklyDistance = 66m,
                TotalDistance = 1194m,
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
                TaperWeeks = 2,
                HasTuneUpRace = true,
                TrainingPeriodization = HalHigdonTrainingPeriodization(),
                SampleWeekWorkoutTypes =
                [
                    new(DayOfWeek.Monday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Tuesday, [WorkoutType.HillRepeat, WorkoutType.TempoRun, WorkoutType.Intervals]),
                    new(DayOfWeek.Wednesday, [WorkoutType.EasyRun]),
                    new(DayOfWeek.Thursday, [WorkoutType.TempoRun, WorkoutType.RacePace]),
                    new(DayOfWeek.Friday, [WorkoutType.Rest]),
                    new(DayOfWeek.Saturday, [WorkoutType.MediumRun, WorkoutType.RacePace]),
                    new(DayOfWeek.Sunday, [WorkoutType.LongRun])
                ],
                SampleWorkoutByPhases =
                [
                    new SampleWorkoutByPhase(
                        TrainingPhase.Base,
                        [
                            WorkoutSample.CreateRacePace(5m),
                            WorkoutSample.CreateRacePace(8m),
                            WorkoutSample.CreateRacePace(10m),
                            WorkoutSample.CreateTempo(30),
                            WorkoutSample.CreateInterval(
                                4,
                                800,
                                400,
                                8m,
                                restBeforeStartIntervalDistance: 0.2m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Build,
                        [
                            WorkoutSample.CreateRacePace(10m),
                            WorkoutSample.CreateRacePace(11.5m),
                            WorkoutSample.CreateRacePace(13m),
                            WorkoutSample.CreateHillRepeat(
                                4,
                                0.4m,
                                0.4m,
                                8m),
                            WorkoutSample.CreateHillRepeat(
                                5,
                                0.4m,
                                0.4m,
                                8m),
                            WorkoutSample.CreateTempo(35),
                            WorkoutSample.CreateTempo(40),
                            WorkoutSample.CreateInterval(
                                5,
                                800,
                                400,
                                10m,
                                restBeforeStartIntervalDistance: 0.2m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Peak,
                        [
                            WorkoutSample.CreateRacePace(6.5m),
                            WorkoutSample.CreateRacePace(8m),
                            WorkoutSample.CreateRacePace(16m),
                            WorkoutSample.CreateHillRepeat(
                                6,
                                0.4m,
                                0.4m,
                                8m),
                            WorkoutSample.CreateHillRepeat(
                                7,
                                0.4m,
                                0.4m,
                                10m),
                            WorkoutSample.CreateTempo(40),
                            WorkoutSample.CreateTempo(45),
                            WorkoutSample.CreateTempo(50),
                            WorkoutSample.CreateInterval(
                                6,
                                800,
                                400,
                                10m,
                                restBeforeStartIntervalDistance: 0.2m),
                            WorkoutSample.CreateInterval(
                                7,
                                800,
                                400,
                                10m,
                                restBeforeStartIntervalDistance: 0.2m),
                            WorkoutSample.CreateInterval(
                                8,
                                800,
                                400,
                                10m,
                                restBeforeStartIntervalDistance: 0.2m)
                        ]),
                    new SampleWorkoutByPhase(
                        TrainingPhase.Taper,
                        [
                            WorkoutSample.CreateRacePace(6.5m),
                            WorkoutSample.CreateHillRepeat(
                                4,
                                0.4m,
                                0.4m,
                                8m),
                            WorkoutSample.CreateHillRepeat(
                                6,
                                0.4m,
                                0.4m,
                                8m),
                            WorkoutSample.CreateTempo(30),
                            WorkoutSample.CreateInterval(
                                4,
                                800,
                                400,
                                8m,
                                restBeforeStartIntervalDistance: 0.2m)
                        ])
                ],
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

    private static List<Periodization> HalHigdonTrainingPeriodization()
    {
        return
        [
            new Periodization(1, TrainingPhase.Base),
            new Periodization(2, TrainingPhase.Base),
            new Periodization(3, TrainingPhase.Base),
            new Periodization(4, TrainingPhase.Build),
            new Periodization(5, TrainingPhase.Build),
            new Periodization(6, TrainingPhase.Build),
            new Periodization(7, TrainingPhase.Build),
            new Periodization(8, TrainingPhase.Build),
            new Periodization(9, TrainingPhase.TuneUpRace),
            new Periodization(10, TrainingPhase.Peak),
            new Periodization(11, TrainingPhase.Peak),
            new Periodization(12, TrainingPhase.Peak),
            new Periodization(13, TrainingPhase.Peak),
            new Periodization(14, TrainingPhase.Peak),
            new Periodization(15, TrainingPhase.Peak),
            new Periodization(16, TrainingPhase.Taper),
            new Periodization(17, TrainingPhase.Taper),
            new Periodization(18, TrainingPhase.Race)
        ];
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
