using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.PlanGenerator.Test2;

public class CustomMarathonPlanGenerator2
{
    private readonly MarathonPlanParameters _parameters;

    public CustomMarathonPlanGenerator2(MarathonPlanParameters parameters)
    {
        _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
    }

    public TrainingPlan Generate()
    {
        // Generate the training weeks
        var trainingWeeks = GenerateTrainingWeeks();

        // Create and return the training plan
        return TrainingPlan.Create(trainingWeeks);
    }

    private List<TrainingWeek> GenerateTrainingWeeks()
    {
        var weeks = new List<TrainingWeek>();

        // Calculate progression
        var weeksToRace = _parameters.PlanWeeks;
        var taperStartWeek = weeksToRace - _parameters.TaperWeeks;
        var buildWeeks = (int) Math.Ceiling(taperStartWeek * 0.5);
        var peakWeeks = (int) Math.Ceiling(taperStartWeek * 0.2);
        var baseWeeks = taperStartWeek - buildWeeks - peakWeeks;

        // Generate phases
        var phaseWeeks = new Dictionary<TrainingPhase, int>
        {
            {TrainingPhase.Base, baseWeeks},
            {TrainingPhase.Build, buildWeeks},
            {TrainingPhase.Peak, peakWeeks},
            {TrainingPhase.Taper, _parameters.TaperWeeks - 1}, // -1 for race week
            {TrainingPhase.Race, 1}
        };

        // Create each training week
        int weekNumber = 1;

        foreach (var phase in phaseWeeks)
        {
            for (int i = 0; i < phase.Value; i++)
            {
                weeks.Add(
                    GenerateTrainingWeek(
                        weekNumber,
                        phase.Key,
                        weekNumber == weeksToRace));
                weekNumber++;
            }
        }

        return weeks;
    }

    private TrainingWeek GenerateTrainingWeek(
        int weekNumber,
        TrainingPhase phase,
        bool isRaceWeek)
    {
        // Determine weekly mileage based on progression
        decimal weeklyMileage = CalculateWeeklyMileage(weekNumber, phase);

        // Create workouts for each day
        var workouts = GenerateWeeklyWorkouts(
            weekNumber,
            phase,
            weeklyMileage,
            isRaceWeek);

        // Convert to training week format
        return TrainingWeek.Create(
            weekNumber: weekNumber,
            trainingPhase: phase,
            monday: workouts[DayOfWeek.Monday],
            tuesday: workouts[DayOfWeek.Tuesday],
            wednesday: workouts[DayOfWeek.Wednesday],
            thursday: workouts[DayOfWeek.Thursday],
            friday: workouts[DayOfWeek.Friday],
            saturday: workouts[DayOfWeek.Saturday],
            sunday: workouts[DayOfWeek.Sunday]
        );
    }

    private decimal CalculateWeeklyMileage(int weekNumber, TrainingPhase phase)
    {
        // Calculate progressive weekly mileage
        if (phase == TrainingPhase.Race)
        {
            return _parameters.CurrentWeeklyMileage * 0.5m; // Race week is typically around 50% volume
        }

        if (phase == TrainingPhase.Taper)
        {
            var taperReduction = (decimal) _parameters.TaperWeeks -
                                 (weekNumber - (_parameters.PlanWeeks - _parameters.TaperWeeks)) +
                                 1;
            var taperPercent = 1m - (taperReduction * 0.15m); // Reduce by ~15% each taper week

            return _parameters.PeakWeeklyMileage * taperPercent;
        }

        if (phase == TrainingPhase.Base)
        {
            // Linear progression from current to 75% of peak during base phase
            var basePhaseWeeks = _parameters.PlanWeeks -
                                 _parameters.TaperWeeks -
                                 (int) Math.Ceiling((_parameters.PlanWeeks - _parameters.TaperWeeks) * 0.6);
            var baseProgress = (decimal) weekNumber / basePhaseWeeks;

            return _parameters.CurrentWeeklyMileage +
                   ((_parameters.PeakWeeklyMileage * 0.75m) - _parameters.CurrentWeeklyMileage) * baseProgress;
        }

        // Build phase: progress from 75% to 100% of peak
        var totalBuildWeeks = (int) Math.Ceiling((_parameters.PlanWeeks - _parameters.TaperWeeks) * 0.6);
        var basePhaseLength = _parameters.PlanWeeks - _parameters.TaperWeeks - totalBuildWeeks;
        var buildWeekNumber = weekNumber - basePhaseLength;
        var buildProgress = (decimal) buildWeekNumber / totalBuildWeeks;

        // Every third week is a step-back week (except near the peak)
        bool isStepBackWeek = buildWeekNumber % 3 == 0 && buildWeekNumber < totalBuildWeeks - 1;

        var targetMileage = (_parameters.PeakWeeklyMileage * 0.75m) +
                            ((_parameters.PeakWeeklyMileage - _parameters.PeakWeeklyMileage * 0.75m) * buildProgress);

        return isStepBackWeek ? targetMileage * 0.80m : targetMileage;
    }

    private Dictionary<DayOfWeek, Workout> GenerateWeeklyWorkouts(
        int weekNumber,
        TrainingPhase phase,
        decimal weeklyMileage,
        bool isRaceWeek)
    {
        var workouts = new Dictionary<DayOfWeek, Workout>();

        // Initialize all days with rest
        foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
        {
            workouts[day] = Workout.CreateRest();
        }

        if (isRaceWeek)
        {
            // Race week is special
            workouts[_parameters.LongRunDay] = Workout.CreateRace(42.2m, RacePaceRange());
            PopulateRaceWeekWorkouts(workouts, _parameters.LongRunDay);

            return workouts;
        }

        // Assign long run
        decimal longRunDistance = CalculateLongRunDistance(weekNumber, phase, weeklyMileage);

        workouts[_parameters.LongRunDay] = phase == TrainingPhase.Taper && weekNumber >= _parameters.PlanWeeks - 1
            ? Workout.CreateEasyRun(longRunDistance, EasyPaceRange())
            : Workout.CreateLongRun(longRunDistance, EasyPaceRange());

        // Assign quality workouts
        AssignQualityWorkouts(
            workouts,
            phase,
            weekNumber,
            weeklyMileage);

        // Fill in remaining running days with easy runs
        FillRemainingRunningDays(workouts, weeklyMileage);

        return workouts;
    }

    private decimal CalculateLongRunDistance(int weekNumber, TrainingPhase phase, decimal weeklyMileage)
    {
        if (phase == TrainingPhase.Race)
        {
            return 42.2m; // Marathon distance
        }

        // Long run is typically 30-40% of weekly volume
        decimal longRunPercent = 0.32m; // Starting point

        switch (phase)
        {
            case TrainingPhase.Base:
                longRunPercent = 0.32m;

                break;
            case TrainingPhase.Build:
                longRunPercent = 0.36m;
                // Cap at 32km during build for most levels except elite
                var calculatedDistanceBuild = weeklyMileage * longRunPercent;

                return _parameters.RunnerLevel == ExperienceLevel.Elite
                    ? Math.Min(calculatedDistanceBuild, 35m)
                    : Math.Min(calculatedDistanceBuild, 32m);
            case TrainingPhase.Peak:
                longRunPercent = 0.4m;
                // Cap at 32km during build for most levels except elite
                var calculatedDistancePeak = weeklyMileage * longRunPercent;

                return _parameters.RunnerLevel == ExperienceLevel.Elite
                    ? Math.Min(calculatedDistancePeak, 35m)
                    : Math.Min(calculatedDistancePeak, 32m);
            case TrainingPhase.Taper:
                // During taper, long run reduces more significantly
                int weeksToRace = _parameters.PlanWeeks - weekNumber + 1;
                longRunPercent = 0.3m - (0.05m * (_parameters.TaperWeeks - weeksToRace));

                break;
        }

        return weeklyMileage * longRunPercent;
    }

    private void AssignQualityWorkouts(
        Dictionary<DayOfWeek, Workout> workouts,
        TrainingPhase phase,
        int weekNumber,
        decimal weeklyMileage)
    {
        // Calculate how many quality workouts should be assigned
        int qualityWorkoutsToAssign = phase switch
        {
            TrainingPhase.Base => Math.Min(1, _parameters.QualityWorkoutDays),
            TrainingPhase.Build => _parameters.QualityWorkoutDays,
            TrainingPhase.Peak => _parameters.QualityWorkoutDays,
            TrainingPhase.Taper => Math.Max(1, _parameters.QualityWorkoutDays - 1),
            TrainingPhase.Race => 0,
            _ => 0
        };

        if (qualityWorkoutsToAssign == 0)
            return;

        // Find appropriate days for quality workouts (not adjacent to long run day)
        var potentialQualityDays = GetPotentialQualityWorkoutDays(_parameters.LongRunDay);
        var qualityDays = potentialQualityDays.Take(qualityWorkoutsToAssign).ToList();

        // Assign specific quality workout types based on phase and preferences
        for (int i = 0; i < qualityDays.Count; i++)
        {
            var day = qualityDays[i];

            // Skip if we somehow selected the long run day
            if (day == _parameters.LongRunDay)
                continue;

            switch (phase)
            {
                case TrainingPhase.Base:
                    // Base phase focuses on building endurance
                    if (_parameters.IncludeRacePaceRuns)
                    {
                        workouts[day] = Workout.CreateRacePace(weeklyMileage * 0.15m, RacePaceRange());
                    }
                    else
                    {
                        workouts[day] = Workout.CreateEasyRun(weeklyMileage * 0.15m, EasyPaceRange());
                    }

                    break;

                case TrainingPhase.Build:
                    // Build phase includes more specific workouts
                    if (i == 0 && _parameters.IncludeTempoRuns)
                    {
                        // First quality workout in build is often tempo
                        workouts[day] = Workout.CreateTempo(
                            30,
                            weeklyMileage * 0.18m,
                            EasyPaceRange(),
                            TempoPaceRange());
                    }
                    else if (i == 1 && _parameters.IncludeSpeedWork)
                    {
                        // Second quality is intervals or hills
                        if (_parameters.IncludeHillWork && weekNumber % 3 == 0)
                        {
                            workouts[day] = Workout.CreateHillRepeat(
                                4 + (weekNumber / 3), // Progression in repeats
                                weeklyMileage * 0.15m,
                                EasyPaceRange(),
                                IntervalPaceRange(),
                                EasyPaceRange());
                        }
                        else
                        {
                            workouts[day] = Workout.CreateInterval(
                                4 + (weekNumber / 4), // Progression in repeats
                                800,
                                400,
                                weeklyMileage * 0.15m,
                                EasyPaceRange(),
                                IntervalPaceRange(),
                                EasyPaceRange());
                        }
                    }
                    else if (_parameters.IncludeRacePaceRuns)
                    {
                        // Default quality workout is race pace
                        workouts[day] = Workout.CreateRacePace(weeklyMileage * 0.18m, RacePaceRange());
                    }
                    else
                    {
                        // Fallback is a medium-length easy run
                        workouts[day] = Workout.CreateMediumRun(weeklyMileage * 0.18m, EasyPaceRange());
                    }

                    break;

                case TrainingPhase.Peak:
                    // Build phase includes more specific workouts
                    if (i == 0 && _parameters.IncludeTempoRuns)
                    {
                        // First quality workout in build is often tempo
                        workouts[day] = Workout.CreateTempo(
                            40,
                            weeklyMileage * 0.18m,
                            EasyPaceRange(),
                            TempoPaceRange());
                    }
                    else if (i == 1 && _parameters.IncludeSpeedWork)
                    {
                        // Second quality is intervals or hills
                        if (_parameters.IncludeHillWork && weekNumber % 3 == 0)
                        {
                            workouts[day] = Workout.CreateHillRepeat(
                                4 + (weekNumber / 3), // Progression in repeats
                                weeklyMileage * 0.15m,
                                EasyPaceRange(),
                                IntervalPaceRange(),
                                EasyPaceRange());
                        }
                        else
                        {
                            workouts[day] = Workout.CreateInterval(
                                4 + (weekNumber / 4), // Progression in repeats
                                800,
                                400,
                                weeklyMileage * 0.15m,
                                EasyPaceRange(),
                                IntervalPaceRange(),
                                EasyPaceRange());
                        }
                    }
                    else if (_parameters.IncludeRacePaceRuns)
                    {
                        // Default quality workout is race pace
                        workouts[day] = Workout.CreateRacePace(weeklyMileage * 0.18m, RacePaceRange());
                    }
                    else
                    {
                        // Fallback is a medium-length easy run
                        workouts[day] = Workout.CreateMediumRun(weeklyMileage * 0.18m, EasyPaceRange());
                    }

                    break;

                case TrainingPhase.Taper:
                    // Taper phase reduces intensity but maintains some quality
                    if (i == 0 && _parameters.IncludeRacePaceRuns)
                    {
                        // Race pace work during taper helps with mental preparation
                        decimal racePaceDistance = Math.Min(10m, weeklyMileage * 0.15m);
                        workouts[day] = Workout.CreateRacePace(racePaceDistance, RacePaceRange());
                    }
                    else if (_parameters.IncludeTempoRuns && weekNumber < _parameters.PlanWeeks - 1)
                    {
                        // Lighter tempo workout earlier in taper
                        workouts[day] = Workout.CreateTempo(
                            20,
                            weeklyMileage * 0.12m,
                            EasyPaceRange(),
                            TempoPaceRange());
                    }
                    else
                    {
                        // Final week should be very easy
                        workouts[day] = Workout.CreateEasyRun(weeklyMileage * 0.12m, EasyPaceRange());
                    }

                    break;
            }
        }
    }

    private List<DayOfWeek> GetPotentialQualityWorkoutDays(DayOfWeek longRunDay)
    {
        // Return days that aren't adjacent to the long run day, prioritizing Tuesday/Thursday
        var all = Enum.GetValues<DayOfWeek>().ToList();

        // Remove long run day and adjacent days
        all.Remove(longRunDay);
        all.Remove(GetAdjacentDay(longRunDay, -1));
        all.Remove(GetAdjacentDay(longRunDay, 1));

        // Prioritize traditional quality days
        var prioritized = new List<DayOfWeek>();
        if (all.Contains(DayOfWeek.Tuesday)) prioritized.Add(DayOfWeek.Tuesday);
        if (all.Contains(DayOfWeek.Thursday)) prioritized.Add(DayOfWeek.Thursday);
        if (all.Contains(DayOfWeek.Wednesday)) prioritized.Add(DayOfWeek.Wednesday);

        // Add any remaining days
        foreach (var day in all)
        {
            if (!prioritized.Contains(day))
                prioritized.Add(day);
        }

        return prioritized;
    }

    private DayOfWeek GetAdjacentDay(DayOfWeek day, int offset)
    {
        int adjustedDay = ((int) day + offset + 7) % 7;

        return (DayOfWeek) adjustedDay;
    }

    private void FillRemainingRunningDays(Dictionary<DayOfWeek, Workout> workouts, decimal weeklyMileage)
    {
        // Count already assigned running days
        int assignedRunDays = workouts.Count(w => w.Value.Type != WorkoutType.Rest);
        int remainingRunDays = _parameters.WeeklyRunningDays - assignedRunDays;

        if (remainingRunDays <= 0)
            return;

        // Calculate remaining mileage to distribute
        decimal assignedMileage = workouts.Sum(w => w.Value.TotalDistance.DistanceValue);
        decimal remainingMileage = weeklyMileage - assignedMileage;
        decimal mileagePerRun = remainingMileage / remainingRunDays;

        // Find days not yet assigned and assign easy runs
        var daysToFill = Enum.GetValues<DayOfWeek>()
            .Where(d => workouts[d].Type == WorkoutType.Rest)
            .OrderBy(d => (int) d) // Monday first, then Tuesday, etc.
            .Take(remainingRunDays)
            .ToList();

        foreach (var day in daysToFill)
        {
            workouts[day] = Workout.CreateEasyRun(mileagePerRun, EasyPaceRange());
        }
    }

    private void PopulateRaceWeekWorkouts(Dictionary<DayOfWeek, Workout> workouts, DayOfWeek raceDay)
    {
        // Race week typically has 2-3 very easy short runs and rest days
        int daysToRace = 7;

        foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
        {
            if (day == raceDay)
                continue; // Race day already assigned

            int dayDifference = ((int) raceDay - (int) day + 7) % 7;

            if (dayDifference is 1 or 2)
            {
                // 1-2 days before race: rest
                workouts[day] = Workout.CreateRest();
            }
            else if (dayDifference == 3)
            {
                // 3 days before: short, very easy shake-out
                workouts[day] = Workout.CreateEasyRun(3m, EasyPaceRange());
            }
            else if (dayDifference >= 4)
            {
                // Earlier in the week: short easy runs
                decimal distance = 5m - (dayDifference - 4); // Decreasing distance
                workouts[day] = Workout.CreateEasyRun(Math.Max(3m, distance), EasyPaceRange());
            }
        }
    }

    // Pace calculation methods based on runner level
    private (TimeSpan min, TimeSpan max) EasyPaceRange()
    {
        return _parameters.RunnerLevel switch
        {
            ExperienceLevel.Beginner => (TimeSpan.FromMinutes(7), TimeSpan.FromMinutes(8)),
            ExperienceLevel.Novice => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(15))),
            ExperienceLevel.Intermediate => (TimeSpan.FromMinutes(6),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30))),
            ExperienceLevel.Advanced => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(6)),
            ExperienceLevel.Elite => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(15)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45))),
            _ => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(15)))
        };
    }

    private (TimeSpan min, TimeSpan max) RacePaceRange()
    {
        return _parameters.RunnerLevel switch
        {
            ExperienceLevel.Beginner => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(30))),
            ExperienceLevel.Novice => (TimeSpan.FromMinutes(6), TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(45))),
            ExperienceLevel.Intermediate => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(15)),
                TimeSpan.FromMinutes(6)),
            ExperienceLevel.Advanced => (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(15))),
            ExperienceLevel.Elite => (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(15)),
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(45))),
            _ => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(15)))
        };
    }

    private (TimeSpan min, TimeSpan max) TempoPaceRange()
    {
        return _parameters.RunnerLevel switch
        {
            ExperienceLevel.Beginner => (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(15)),
                TimeSpan.FromMinutes(7)),
            ExperienceLevel.Novice => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(15))),
            ExperienceLevel.Intermediate => (TimeSpan.FromMinutes(5),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30))),
            ExperienceLevel.Advanced => (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(5)),
            ExperienceLevel.Elite => (TimeSpan.FromMinutes(4), TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30))),
            _ => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)), TimeSpan.FromMinutes(6))
        };
    }

    private (TimeSpan min, TimeSpan max) IntervalPaceRange()
    {
        return _parameters.RunnerLevel switch
        {
            ExperienceLevel.Beginner => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30))),
            ExperienceLevel.Novice => (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(15)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45))),
            ExperienceLevel.Intermediate => (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(5)),
            ExperienceLevel.Advanced => (TimeSpan.FromMinutes(4),
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30))),
            ExperienceLevel.Elite => (TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(30)), TimeSpan.FromMinutes(4)),
            _ => (TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)))
        };
    }
}