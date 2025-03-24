using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.PlanGenerator.Test1;

public class CustomMarathonPlanGenerator
{
    public class PlanParameters
    {
        public DateTime RaceDate { get; set; }
        public int RunningDaysPerWeek { get; set; } = 4;
        public List<DayOfWeek> QualityWorkoutDays { get; set; } = new();
        public DayOfWeek LongRunDay { get; set; } = DayOfWeek.Sunday;
        public ExperienceLevel RunnerLevel { get; set; } = ExperienceLevel.Intermediate;
        public List<WorkoutType> WorkoutTypesToInclude { get; set; } = new();
        public decimal CurrentWeeklyMileage { get; set; } = 20;
        public decimal TargetPeakWeeklyMileage { get; set; } = 60;
        public int PlanWeeks { get; set; } = 18;
        public decimal LongRunMaxPercentage { get; set; } = 0.35m; // Long run as percentage of weekly mileage
        public decimal WeeklyMileageIncrease { get; set; } = 0.10m; // Weekly mileage increase (10%)
        public int StepbackEveryNWeeks { get; set; } = 3; // Apply step-back weeks every N weeks
        public decimal StepbackAmount { get; set; } = 0.20m; // Reduce mileage by 20% on step-back weeks
    }

    public TrainingPlan GenerateTrainingPlan(PlanParameters parameters)
    {
        // Calculate start date from race date and plan weeks
        DateTime startDate = parameters.RaceDate.AddDays(-7 * parameters.PlanWeeks);
        
        // Create a list to hold the training weeks
        var trainingWeeks = new List<TrainingWeek>();
        
        // Generate weekly mileage progression
        var weeklyMileageProgression = GenerateWeeklyMileageProgression(parameters);
        
        // Generate training phases
        var trainingPhases = GenerateTrainingPhases(parameters.PlanWeeks);
        
        // Create the training weeks
        for (int weekNumber = 1; weekNumber <= parameters.PlanWeeks; weekNumber++)
        {
            var weekMileage = weeklyMileageProgression[weekNumber - 1];
            var phase = trainingPhases[weekNumber - 1];
            
            var week = GenerateTrainingWeek(
                weekNumber,
                phase,
                weekMileage,
                parameters,
                startDate.AddDays(7 * (weekNumber - 1))
            );
            
            trainingWeeks.Add(week);
        }
        
        return TrainingPlan.Create(trainingWeeks);
    }

    private List<decimal> GenerateWeeklyMileageProgression(PlanParameters parameters)
    {
        var weeklyMileageProgression = new List<decimal>();
        
        // Calculate how much we need to increase weekly to reach the peak
        decimal mileageDifference = parameters.TargetPeakWeeklyMileage - parameters.CurrentWeeklyMileage;
        
        // We'll peak 3 weeks before the race, then taper
        int weeksToIncrease = parameters.PlanWeeks - 3;
        
        decimal currentMileage = parameters.CurrentWeeklyMileage;
        
        for (int week = 1; week <= parameters.PlanWeeks; week++)
        {
            // Apply step-back week every N weeks (except for the last 3 weeks)
            bool isStepbackWeek = week % parameters.StepbackEveryNWeeks == 0 && week <= weeksToIncrease;
            
            if (week <= weeksToIncrease && !isStepbackWeek)
            {
                // Progressive increase
                decimal targetIncrease = Math.Min(parameters.WeeklyMileageIncrease * currentMileage, 
                    (mileageDifference / weeksToIncrease));
                
                currentMileage += targetIncrease;
            }
            else if (isStepbackWeek)
            {
                // Step-back week
                currentMileage -= (currentMileage * parameters.StepbackAmount);
            }
            else if (week > weeksToIncrease)
            {
                // Taper weeks
                int taperWeek = week - weeksToIncrease;
                
                switch (taperWeek)
                {
                    case 1:
                        currentMileage = parameters.TargetPeakWeeklyMileage * 0.8m;
                        break;
                    case 2:
                        currentMileage = parameters.TargetPeakWeeklyMileage * 0.6m;
                        break;
                    case 3:
                        currentMileage = parameters.TargetPeakWeeklyMileage * 0.4m;
                        break;
                }
            }
            
            weeklyMileageProgression.Add(Math.Round(currentMileage, 1));
        }
        
        return weeklyMileageProgression;
    }

    private List<TrainingPhase> GenerateTrainingPhases(int planWeeks)
    {
        var phases = new List<TrainingPhase>();
        
        // Base phase - first 30% of plan
        int basePhaseWeeks = (int)(planWeeks * 0.3);
        phases.AddRange(Enumerable.Repeat(TrainingPhase.Base, basePhaseWeeks));
        
        // Build phase - next 40% of plan
        int buildPhaseWeeks = (int)(planWeeks * 0.4);
        phases.AddRange(Enumerable.Repeat(TrainingPhase.Build, buildPhaseWeeks));
        
        // Peak phase - next 15% of plan
        int peakPhaseWeeks = (int)(planWeeks * 0.15);
        phases.AddRange(Enumerable.Repeat(TrainingPhase.Peak, peakPhaseWeeks));
        
        // Taper phase - final 15% of plan
        int taperPhaseWeeks = planWeeks - basePhaseWeeks - buildPhaseWeeks - peakPhaseWeeks;
        phases.AddRange(Enumerable.Repeat(TrainingPhase.Taper, taperPhaseWeeks - 1));
        
        // Race week
        phases.Add(TrainingPhase.Race);
        
        return phases;
    }

    private TrainingWeek GenerateTrainingWeek(
        int weekNumber,
        TrainingPhase phase,
        decimal weeklyMileage,
        PlanParameters parameters,
        DateTime weekStartDate)
    {
        // Dictionary to store workouts for each day
        var workouts = new Dictionary<DayOfWeek, Workout>();
        
        // Calculate the long run distance based on weekly mileage
        decimal longRunDistance = CalculateLongRunDistance(weeklyMileage, parameters.LongRunMaxPercentage, phase);
        
        // Assign the long run to the specified day
        workouts[parameters.LongRunDay] = phase == TrainingPhase.Race && weekNumber == parameters.PlanWeeks
            ? Workout.CreateRace(42.2m)
            : Workout.CreateLongRun(longRunDistance, EasyPaceRange(parameters.RunnerLevel));
        
        // Calculate remaining mileage to distribute
        decimal remainingMileage = weeklyMileage - longRunDistance;
        
        // Calculate easy run mileage per day
        decimal easyRunMileagePerDay = remainingMileage / (parameters.RunningDaysPerWeek - 1);
        
        // Quality workout days
        foreach (var qualityDay in parameters.QualityWorkoutDays)
        {
            if (qualityDay == parameters.LongRunDay)
                continue; // Skip if this is already the long run day
            
            // Don't schedule quality workouts in the final week
            if (weekNumber == parameters.PlanWeeks)
            {
                workouts[qualityDay] = Workout.CreateEasyRun(
                    Math.Min(5, easyRunMileagePerDay),
                    EasyPaceRange(parameters.RunnerLevel));
                continue;
            }
            
            // Determine what type of quality workout based on phase and included types
            Workout qualityWorkout = GenerateQualityWorkout(
                phase,
                Math.Round(easyRunMileagePerDay * 1.2m, 1), // Quality workouts slightly longer than easy runs
                parameters.WorkoutTypesToInclude,
                parameters.RunnerLevel);
            
            workouts[qualityDay] = qualityWorkout;
        }
        
        // Fill in the rest of the running days with easy runs
        foreach (DayOfWeek day in Enum.GetValues<DayOfWeek>())
        {
            // Skip days we've already assigned workouts to
            if (workouts.ContainsKey(day))
                continue;
            
            // Skip rest days (non-running days)
            int runningDaysAssigned = workouts.Count;
            if (runningDaysAssigned >= parameters.RunningDaysPerWeek)
            {
                workouts[day] = Workout.CreateRest();
                continue;
            }
            
            // Add easy run
            workouts[day] = Workout.CreateEasyRun(
                Math.Round(easyRunMileagePerDay, 1),
                EasyPaceRange(parameters.RunnerLevel));
        }
        
        // Create the training week
        return TrainingWeek.Create(
            weekNumber: weekNumber,
            trainingPhase: phase,
            monday: workouts[DayOfWeek.Monday],
            tuesday: workouts[DayOfWeek.Tuesday],
            wednesday: workouts[DayOfWeek.Wednesday],
            thursday: workouts[DayOfWeek.Thursday],
            friday: workouts[DayOfWeek.Friday],
            saturday: workouts[DayOfWeek.Saturday],
            sunday: workouts[DayOfWeek.Sunday]);
    }

    private decimal CalculateLongRunDistance(decimal weeklyMileage, decimal maxPercentage, TrainingPhase phase)
    {
        decimal percentage = phase switch
        {
            TrainingPhase.Base => maxPercentage * 0.8m,
            TrainingPhase.Build => maxPercentage * 0.9m,
            TrainingPhase.Peak => maxPercentage,
            TrainingPhase.Taper => maxPercentage * 0.7m,
            TrainingPhase.Race => maxPercentage * 0.5m,
            _ => maxPercentage * 0.8m
        };
        
        // Calculate the long run distance
        decimal longRunDistance = Math.Round(weeklyMileage * percentage, 1);
        
        // Cap the long run at marathon distance (except for ultra training)
        return Math.Min(longRunDistance, 35);
    }

    private Workout GenerateQualityWorkout(
        TrainingPhase phase,
        decimal distance,
        List<WorkoutType> typesToInclude,
        ExperienceLevel runnerLevel)
    {
        // Choose a workout type based on the training phase and included workout types
        List<WorkoutType> phaseAppropriateTypes = phase switch
        {
            TrainingPhase.Base => new List<WorkoutType> 
            { 
                WorkoutType.TempoRun,
                WorkoutType.EasyRunWithStrides 
            },
            
            TrainingPhase.Build => new List<WorkoutType> 
            { 
                WorkoutType.TempoRun,
                WorkoutType.HillRepeat,
                WorkoutType.Intervals 
            },
            
            TrainingPhase.Peak => new List<WorkoutType> 
            { 
                WorkoutType.Intervals,
                WorkoutType.TempoRun,
                WorkoutType.RacePace 
            },
            
            TrainingPhase.Taper => new List<WorkoutType> 
            { 
                WorkoutType.TempoRun,
                WorkoutType.RacePace 
            },
            
            _ => new List<WorkoutType> { WorkoutType.EasyRun }
        };
        
        // Filter to only include requested workout types
        var availableTypes = phaseAppropriateTypes
            .Where(t => typesToInclude.Contains(t))
            .ToList();
        
        // If no types match, default to easy run
        if (!availableTypes.Any())
        {
            return Workout.CreateEasyRun(distance, EasyPaceRange(runnerLevel));
        }
        
        // Pick a random workout type from the available types
        var random = new Random();
        var selectedType = availableTypes[random.Next(availableTypes.Count)];
        
        // Generate the workout based on the selected type
        return selectedType switch
        {
            WorkoutType.TempoRun => Workout.CreateTempo(
                tempoMinutes: 20,  // Adjust based on distance
                totalDistance: distance,
                easyPaceRange: EasyPaceRange(runnerLevel),
                tempoPaceRange: TempoPaceRange(runnerLevel)),
                
            WorkoutType.HillRepeat => Workout.CreateHillRepeat(
                repeats: CalculateHillRepeats(runnerLevel, phase),
                totalDistance: distance,
                easyPaceRange: EasyPaceRange(runnerLevel),
                hillPaceRange: HillPaceRange(runnerLevel),
                recoveryPaceRange: RecoveryPaceRange(runnerLevel)),
                
            WorkoutType.Intervals => Workout.CreateInterval(
                repeats: CalculateIntervalRepeats(runnerLevel, phase),
                meters: 800,
                recoveryMeters: 400,
                totalDistance: distance,
                easyPaceRange: EasyPaceRange(runnerLevel),
                intervalPaceRange: IntervalPaceRange(runnerLevel),
                recoveryPaceRange: RecoveryPaceRange(runnerLevel)),
                
            WorkoutType.RacePace => Workout.CreateRacePace(
                distance,
                RacePaceRange(runnerLevel)),
                
            _ => Workout.CreateEasyRun(distance, EasyPaceRange(runnerLevel))
        };
    }

    private int CalculateHillRepeats(ExperienceLevel level, TrainingPhase phase)
    {
        return (level, phase) switch
        {
            (ExperienceLevel.Beginner, _) => 3,
            (ExperienceLevel.Novice, _) => 4,
            (ExperienceLevel.Intermediate, TrainingPhase.Base) => 4,
            (ExperienceLevel.Intermediate, _) => 6,
            (ExperienceLevel.Advanced, TrainingPhase.Base) => 5,
            (ExperienceLevel.Advanced, TrainingPhase.Build) => 6,
            (ExperienceLevel.Advanced, _) => 8,
            (ExperienceLevel.Elite, _) => 10,
            _ => 4
        };
    }

    private int CalculateIntervalRepeats(ExperienceLevel level, TrainingPhase phase)
    {
        return (level, phase) switch
        {
            (ExperienceLevel.Beginner, _) => 3,
            (ExperienceLevel.Novice, _) => 4,
            (ExperienceLevel.Intermediate, TrainingPhase.Base) => 4,
            (ExperienceLevel.Intermediate, _) => 6,
            (ExperienceLevel.Advanced, TrainingPhase.Base) => 5,
            (ExperienceLevel.Advanced, TrainingPhase.Build) => 6,
            (ExperienceLevel.Advanced, _) => 8,
            (ExperienceLevel.Elite, _) => 10,
            _ => 4
        };
    }

    // Pace range calculation methods based on runner level
    private static (TimeSpan min, TimeSpan max) EasyPaceRange(ExperienceLevel level)
    {
        return level switch
        {
            ExperienceLevel.Beginner => (TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(8).Add(TimeSpan.FromSeconds(30))),
                
            ExperienceLevel.Novice => (TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(8).Add(TimeSpan.FromSeconds(0))),
                
            ExperienceLevel.Intermediate => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(45))),
                
            ExperienceLevel.Advanced => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(15))),
                
            ExperienceLevel.Elite => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30))),
                
            _ => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(30)))
        };
    }

    private static (TimeSpan min, TimeSpan max) TempoPaceRange(ExperienceLevel level)
    {
        return level switch
        {
            ExperienceLevel.Beginner => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(0))),
                
            ExperienceLevel.Novice => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(15)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(45))),
                
            ExperienceLevel.Intermediate => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(0))),
                
            ExperienceLevel.Advanced => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30))),
                
            ExperienceLevel.Elite => (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(0))),
                
            _ => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(15)))
        };
    }

    private static (TimeSpan min, TimeSpan max) HillPaceRange(ExperienceLevel level)
    {
        return level switch
        {
            ExperienceLevel.Beginner => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30))),
                
            ExperienceLevel.Novice => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(15))),
                
            ExperienceLevel.Intermediate => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30))),
                
            ExperienceLevel.Advanced => (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(0))),
                
            ExperienceLevel.Elite => (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30))),
                
            _ => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(0)))
        };
    }

    private static (TimeSpan min, TimeSpan max) IntervalPaceRange(ExperienceLevel level)
    {
        return level switch
        {
            ExperienceLevel.Beginner => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(15))),
                
            ExperienceLevel.Novice => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(0))),
                
            ExperienceLevel.Intermediate => (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(15))),
                
            ExperienceLevel.Advanced => (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(15)),
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(45))),
                
            ExperienceLevel.Elite => (TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(15))),
                
            _ => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)))
        };
    }

    private static (TimeSpan min, TimeSpan max) RecoveryPaceRange(ExperienceLevel level)
    {
        return level switch
        {
            ExperienceLevel.Beginner => (TimeSpan.FromMinutes(8).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(9).Add(TimeSpan.FromSeconds(30))),
                
            ExperienceLevel.Novice => (TimeSpan.FromMinutes(8).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(9).Add(TimeSpan.FromSeconds(0))),
                
            ExperienceLevel.Intermediate => (TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(8).Add(TimeSpan.FromSeconds(0))),
                
            ExperienceLevel.Advanced => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(30))),
                
            ExperienceLevel.Elite => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(0))),
                
            _ => (TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(8).Add(TimeSpan.FromSeconds(30)))
        };
    }

    private static (TimeSpan min, TimeSpan max) RacePaceRange(ExperienceLevel level)
    {
        return level switch
        {
            ExperienceLevel.Beginner => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(30))),
                
            ExperienceLevel.Novice => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(15))),
                
            ExperienceLevel.Intermediate => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(15))),
                
            ExperienceLevel.Advanced => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30))),
                
            ExperienceLevel.Elite => (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(15)),
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(45))),
                
            _ => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(0)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(45)))
        };
    }
}