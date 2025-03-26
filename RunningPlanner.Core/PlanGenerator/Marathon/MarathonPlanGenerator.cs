using RunningPlanner.Core.Models;
using RunningPlanner.Core.Models.Paces;

namespace RunningPlanner.Core.PlanGenerator.Marathon;

public class MarathonPlanGeneratorParameters
{
    // Required parameters
    public DateTime RaceDate { get; set; }
    public ExperienceLevel RunnerLevel { get; set; }
    public DayOfWeek[] WeeklyRunningDays { get; set; } = [];
    public decimal CurrentWeeklyMileage { get; set; }
    public decimal RaceDistance { get; set; }
    public decimal PeakWeeklyMileage { get; set; }
    public (TimeSpan Time, decimal Distance) RecentRaceResult { get; set; }
    public TimeSpan GoalTime { get; set; }

    // Optional parameters with sensible defaults
    public DayOfWeek[] QualityWorkoutDays { get; set; } = [];
    public DayOfWeek LongRunDay { get; set; } = DayOfWeek.Sunday;
    public int TaperWeeks { get; set; } = 3;
    public WorkoutType[] PreferredWorkoutTypes { get; set; } = [];
    public bool IncludeMidWeekMediumRun { get; set; }

    // Derived properties
    public int PlanWeeks => CalculatePlanWeeks();

    private int CalculatePlanWeeks()
    {
        var weeksUntilRace = (int) Math.Ceiling((RaceDate - DateTime.Today).TotalDays / 7.0);

        return weeksUntilRace;
    }
}

public class MarathonPlanGenerator
{
    private const decimal MediumRunPercent = 0.20m;
    private readonly MarathonPlanGeneratorParameters _parameters;

    public MarathonPlanGenerator(MarathonPlanGeneratorParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

        _parameters = parameters;
    }

    public TrainingPlan Generate()
    {
        var planWeeks = _parameters.PlanWeeks;

        if (planWeeks is < 12 or > 24)
        {
            throw new Exception(
                $"Plan weeks must be between 12 and 24, but was {planWeeks}.");
        }

        if (_parameters.WeeklyRunningDays.Length < 3)
        {
            throw new Exception(
                $"At least 3 weekly running days must be specified, but only {_parameters.WeeklyRunningDays.Length} were specified.");
        }

        if (_parameters.QualityWorkoutDays.Length > 2)
        {
            throw new Exception(
                $"At most 2 quality workout days can be specified, but {_parameters.QualityWorkoutDays.Length} were specified.");
        }

        var phaseWeeks = GeneratePhaseWeeks(planWeeks);

        var weeklyMileages = CalculateWeeklyMileage(phaseWeeks);

        List<TrainingWeek> trainingWeeks = [];

        TrainingPaces trainingPaces;

        if (_parameters.GoalTime != TimeSpan.Zero)
        {
            trainingPaces = VdotCalculator.GetTrainingPacesFromRace(_parameters.RaceDistance, _parameters.GoalTime);
        }
        else
        {
            trainingPaces = VdotCalculator.GetTrainingPacesFromRace(
                _parameters.RecentRaceResult.Distance,
                _parameters.RecentRaceResult.Time);
        }

        foreach (var weeklyMileage in weeklyMileages)
        {
            var workouts = new Dictionary<DayOfWeek, Workout>();

            var longRun = CalculateLongRunDistance(
                weeklyMileage.week,
                weeklyMileage.trainingPhase,
                weeklyMileage.weeklyMileage);

            var nonQualityDays = _parameters.WeeklyRunningDays
                .Except(_parameters.QualityWorkoutDays)
                .Except([_parameters.LongRunDay])
                .ToArray();

            if (weeklyMileage.trainingPhase is not TrainingPhase.Race)
            {
                var longRunWorkout = Workout.CreateLongRun(longRun.distance, trainingPaces.EasyPace);

                workouts[_parameters.LongRunDay] = longRunWorkout;

                var mediumRunDistance = 0m;

                if (_parameters.IncludeMidWeekMediumRun)
                {
                    var (mediumRun, mediumRunDay) = MediumRun(weeklyMileage, trainingPaces, nonQualityDays);

                    mediumRunDistance = mediumRun.TotalDistance.DistanceValue;

                    workouts[mediumRunDay] = mediumRun;
                }

                var remainingPercent = 1m - longRun.percent - MediumRunPercent;
                var remainingMileage = weeklyMileage.weeklyMileage - longRun.distance - mediumRunDistance;

                if (_parameters.QualityWorkoutDays.Length != 0)
                {
                    var qualityWorkoutPercent = 0.4m;
                    var qualityWorkoutDistance = remainingMileage * qualityWorkoutPercent;

                    foreach (var qualityWorkoutDay in _parameters.QualityWorkoutDays)
                    {
                        switch (weeklyMileage.trainingPhase)
                        {
                            case TrainingPhase.Base:
                                break;
                            case TrainingPhase.Build:
                                break;
                            case TrainingPhase.Peak:
                                break;
                            case TrainingPhase.Taper:
                                break;
                        }
                    }
                }

                var remainingRunningDays = nonQualityDays.Length;
                var distancePerDay = remainingMileage / remainingRunningDays;

                foreach (var nonQualityDay in nonQualityDays)
                {
                    workouts[nonQualityDay] = Workout.CreateEasyRun(distancePerDay, trainingPaces.EasyPace);
                }
            }
            else
            {
                var longRunWorkout = Workout.CreateRace(longRun.distance);

                var raceDayOfWeek = _parameters.RaceDate.DayOfWeek;
                workouts[raceDayOfWeek] = longRunWorkout;

                var remainingRunningDays = 3;
                var remainingMileage = weeklyMileage.weeklyMileage / remainingRunningDays;

                var runningDays = _parameters.WeeklyRunningDays.Where(day => day != raceDayOfWeek).ToArray();

                for (int i = 0; i < remainingRunningDays; i++)
                {
                    var day = runningDays[i];

                    workouts[day] = Workout.CreateEasyRun(remainingMileage, trainingPaces.EasyPace);
                }
            }

            trainingWeeks.Add(
                TrainingWeek.Create(
                    weekNumber: weeklyMileage.week,
                    trainingPhase: weeklyMileage.trainingPhase,
                    monday: workouts.GetValueOrDefault(DayOfWeek.Monday),
                    tuesday: workouts.GetValueOrDefault(DayOfWeek.Tuesday),
                    wednesday: workouts.GetValueOrDefault(DayOfWeek.Wednesday),
                    thursday: workouts.GetValueOrDefault(DayOfWeek.Thursday),
                    friday: workouts.GetValueOrDefault(DayOfWeek.Friday),
                    saturday: workouts.GetValueOrDefault(DayOfWeek.Saturday),
                    sunday: workouts.GetValueOrDefault(DayOfWeek.Sunday)
                )
            );
        }

        return TrainingPlan.Create(trainingWeeks);
    }

    private Dictionary<TrainingPhase, int> GeneratePhaseWeeks(int planWeeks)
    {
        var taperStartWeek = planWeeks - _parameters.TaperWeeks;
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

        return phaseWeeks;
    }

    private List<(int week, decimal weeklyMileage, TrainingPhase trainingPhase)> CalculateWeeklyMileage(
        Dictionary<TrainingPhase, int> phaseWeeks)
    {
        List<(int week, decimal weeklyMileage, TrainingPhase trainingPhase)> mileageByWeek = [];

        // Create each training week
        int weekNumber = 1;

        foreach (var phase in phaseWeeks)
        {
            for (int i = 0; i < phase.Value; i++)
            {
                switch (phase.Key)
                {
                    case TrainingPhase.Base:
                    {
                        // Linear progression from current to 70% of peak during base phase
                        var totalBasePhaseWeeks = phase.Value;
                        var baseProgress = (decimal) weekNumber / totalBasePhaseWeeks;

                        var targetMileage = _parameters.CurrentWeeklyMileage +
                                            ((_parameters.PeakWeeklyMileage * 0.7m) -
                                             _parameters.CurrentWeeklyMileage) *
                                            baseProgress;

                        var baseWeekMileage = CalculateWeekMileageConsideringStepBackWeek(
                            weekNumber,
                            totalBasePhaseWeeks,
                            targetMileage,
                            reducePercentage: 0.9m);
                        mileageByWeek.Add((weekNumber, baseWeekMileage, TrainingPhase.Base));

                        weekNumber++;

                        break;
                    }
                    case TrainingPhase.Build:
                    {
                        // Build phase: progress from 70% to 90% of peak
                        var totalBuildWeeks = phase.Value;
                        var basePhaseLength = phaseWeeks[TrainingPhase.Base];
                        var buildWeekNumber = weekNumber - basePhaseLength;
                        var buildProgress = (decimal) buildWeekNumber / totalBuildWeeks;

                        var targetMileage = (_parameters.PeakWeeklyMileage * 0.7m) +
                                            ((_parameters.PeakWeeklyMileage * 0.9m -
                                              _parameters.PeakWeeklyMileage * 0.7m) *
                                             buildProgress);

                        var buildWeekMileage = CalculateWeekMileageConsideringStepBackWeek(
                            buildWeekNumber,
                            totalBuildWeeks,
                            targetMileage);

                        mileageByWeek.Add((weekNumber, buildWeekMileage, TrainingPhase.Build));

                        weekNumber++;

                        break;
                    }
                    case TrainingPhase.Peak:
                    {
                        // Peak phase is at 90-100% of peak mileage
                        int peakWeekNumber = weekNumber -
                                             (_parameters.PlanWeeks - phase.Value - phaseWeeks[TrainingPhase.Taper]) +
                                             1;

                        // First peak week is about 90%, second is 95%, third is 100%
                        var peakPercentage = 0.9m + (0.05m * (peakWeekNumber - 1));
                        var targetMileage = _parameters.PeakWeeklyMileage * Math.Min(1m, peakPercentage);

                        var peakWeekMileage = CalculateWeekMileageConsideringStepBackWeek(
                            peakWeekNumber,
                            phase.Value,
                            targetMileage);
                        mileageByWeek.Add((weekNumber, peakWeekMileage, TrainingPhase.Peak));
                        weekNumber++;

                        break;
                    }
                    case TrainingPhase.Taper:
                    {
                        var taperReduction = (weekNumber - (_parameters.PlanWeeks - phase.Value)) + 1;
                        var taperPercent = 1m - (taperReduction * 0.15m); // Reduce by ~15% each taper week

                        var taperWeekMileage = _parameters.PeakWeeklyMileage * taperPercent;

                        mileageByWeek.Add(
                            (weekNumber, Math.Round(taperWeekMileage, 0, MidpointRounding.AwayFromZero),
                                TrainingPhase.Taper));
                        weekNumber++;

                        break;
                    }
                    case TrainingPhase.Race:
                    {
                        var raceWeekMileage =
                            _parameters.CurrentWeeklyMileage * 0.35m; // Race week is typically around 35% volume

                        mileageByWeek.Add(
                            (weekNumber, Math.Round(raceWeekMileage, 0, MidpointRounding.AwayFromZero),
                                TrainingPhase.Race));

                        weekNumber++;

                        break;
                    }
                }
            }
        }

        return mileageByWeek;
    }

    private static decimal CalculateWeekMileageConsideringStepBackWeek(
        int weekNumber,
        int totalWeeks,
        decimal targetMileage,
        int stepBackWeeks = 3,
        decimal reducePercentage = 0.85m)
    {
        // Every third week is a step-back week (except near end of phase)
        bool isStepBackWeek = weekNumber % stepBackWeeks == 0
                              // avoid start of phase
                              &&
                              weekNumber > 1 &&
                              weekNumber < totalWeeks - 1;

        var buildWeekMileage = isStepBackWeek ? targetMileage * reducePercentage : targetMileage;

        return Math.Round(buildWeekMileage, 0, MidpointRounding.AwayFromZero);
    }

    private (decimal distance, decimal percent) CalculateLongRunDistance(
        int weekNumber,
        TrainingPhase phase,
        decimal weeklyMileage)
    {
        if (phase == TrainingPhase.Race)
        {
            return (42.2m, 0m); // Marathon distance
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

                return (_parameters.RunnerLevel == ExperienceLevel.Elite
                    ? Math.Min(calculatedDistanceBuild, 35m)
                    : Math.Min(calculatedDistanceBuild, 32m), longRunPercent);
            case TrainingPhase.Peak:
                longRunPercent = 0.4m;
                // Cap at 32km during build for most levels except elite
                var calculatedDistancePeak = weeklyMileage * longRunPercent;

                return (_parameters.RunnerLevel == ExperienceLevel.Elite
                    ? Math.Min(calculatedDistancePeak, 35m)
                    : Math.Min(calculatedDistancePeak, 32m), longRunPercent);
            case TrainingPhase.Taper:
                // During taper, long run reduces more significantly
                int weeksToRace = _parameters.PlanWeeks - weekNumber + 1;
                longRunPercent = 0.3m - (0.05m * (_parameters.TaperWeeks - weeksToRace));

                break;
        }

        return (weeklyMileage * longRunPercent, longRunPercent);
    }

    private (Workout mediumRun, DayOfWeek mediumRunDay) MediumRun(
        (int week, decimal weeklyMileage, TrainingPhase trainingPhase) weeklyMileage,
        TrainingPaces trainingPaces,
        DayOfWeek[] nonQualityDays)
    {
        var mediumRunDistance = CalculateMediumRunDistance(
            weeklyMileage.week,
            weeklyMileage.trainingPhase,
            weeklyMileage.weeklyMileage
        );

        var mediumRun = Workout.CreateMediumRun(mediumRunDistance, trainingPaces.EasyPace);

        var mediumRunDay = nonQualityDays.Contains(DayOfWeek.Wednesday)
            ? DayOfWeek.Wednesday
            : nonQualityDays.FirstOrDefault();

        return (mediumRun, mediumRunDay);
    }

    private decimal CalculateMediumRunDistance(int weekNumber, TrainingPhase phase, decimal weeklyMileage)
    {
        // Long run is typically 20% of weekly volume
        // Starting point

        // switch (phase)
        // {
        //     case TrainingPhase.Base:
        //         mediumRunPercent = 0.32m;
        //
        //         break;
        //     case TrainingPhase.Build:
        //         mediumRunPercent = 0.36m;
        //         // Cap at 32km during build for most levels except elite
        //         var calculatedDistanceBuild = weeklyMileage * mediumRunPercent;
        //
        //         return _parameters.RunnerLevel == ExperienceLevel.Elite
        //             ? Math.Min(calculatedDistanceBuild, 35m)
        //             : Math.Min(calculatedDistanceBuild, 32m);
        //     case TrainingPhase.Peak:
        //         mediumRunPercent = 0.4m;
        //         // Cap at 32km during build for most levels except elite
        //         var calculatedDistancePeak = weeklyMileage * mediumRunPercent;
        //
        //         return _parameters.RunnerLevel == ExperienceLevel.Elite
        //             ? Math.Min(calculatedDistancePeak, 35m)
        //             : Math.Min(calculatedDistancePeak, 32m);
        //     case TrainingPhase.Taper:
        //         // During taper, long run reduces more significantly
        //         int weeksToRace = _parameters.PlanWeeks - weekNumber + 1;
        //         mediumRunPercent = 0.3m - (0.05m * (_parameters.TaperWeeks - weeksToRace));
        //
        //         break;
        // }

        return weeklyMileage * MediumRunPercent;
    }
}
