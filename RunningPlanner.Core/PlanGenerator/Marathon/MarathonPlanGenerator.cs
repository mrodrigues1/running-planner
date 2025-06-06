using RunningPlanner.Core.Exceptions;
using RunningPlanner.Core.Models;
using RunningPlanner.Core.Models.Paces;
using RunningPlanner.Core.WorkoutStrategies;

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
    private const int MinimumMarathonPlanWeeks = 12;
    private const int MaximumMarathonPlanWeeks = 24;
    private const int MinimumWeekRunningDays = 4;
    private const int MaximumQualityWorkoutDays = 3;
    private readonly MarathonPlanGeneratorParameters _parameters;
    private readonly IWorkoutGenerator _workoutGenerator;

    public MarathonPlanGenerator(MarathonPlanGeneratorParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

        _parameters = parameters;
        _workoutGenerator = new WorkoutGenerator();
    }

    public TrainingPlan Generate()
    {
        var planWeeks = _parameters.PlanWeeks;

        if (planWeeks is < MinimumMarathonPlanWeeks or > MaximumMarathonPlanWeeks)
        {
            throw new InvalidTrainingPlanParametersException(
                nameof(_parameters.PlanWeeks),
                planWeeks,
                $"Plan weeks must be between {MinimumMarathonPlanWeeks} and {MaximumMarathonPlanWeeks}, but was {planWeeks}.");
        }

        if (_parameters.WeeklyRunningDays.Length < MinimumWeekRunningDays)
        {
            throw new InvalidTrainingPlanParametersException(
                nameof(_parameters.WeeklyRunningDays),
                _parameters.WeeklyRunningDays.Length,
                $"At least {MinimumWeekRunningDays} weekly running days must be specified, but only {_parameters.WeeklyRunningDays.Length} were specified.");
        }

        if (_parameters.QualityWorkoutDays.Length > MaximumQualityWorkoutDays)
        {
            throw new InvalidTrainingPlanParametersException(
                nameof(_parameters.QualityWorkoutDays),
                _parameters.QualityWorkoutDays.Length,
                $"At most {MaximumQualityWorkoutDays} quality workout days can be specified, but {_parameters.QualityWorkoutDays.Length} were specified.");
        }

        var phaseWeeks = GeneratePhaseWeeks(planWeeks);

        var weeklyMileages = CalculateWeeklyMileages(phaseWeeks);

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

        foreach (var weeklyPlan in weeklyMileages)
        {
            List<RunDistribution> weekRunsDistribution = new();

            var workouts = new Dictionary<DayOfWeek, Workout>();

            var nonQualityDays = _parameters.WeeklyRunningDays
                .Except(_parameters.QualityWorkoutDays)
                .Except([_parameters.LongRunDay])
                .ToArray();

            var longRun = CalculateLongRunDistance(
                weeklyPlan.Week,
                weeklyPlan.TrainingPhase,
                weeklyPlan.WeeklyMileage);

            var longRunPercent = longRun.percent;

            if (weeklyPlan.TrainingPhase is TrainingPhase.Race)
            {
                var raceDayOfWeek = _parameters.RaceDate.DayOfWeek;
                weekRunsDistribution.Add(new RunDistribution(WorkoutType.Race, longRunPercent, raceDayOfWeek));

                var remainingRunningDays = 3;

                var raceWeekRemainingPercentage = 1m -
                                                  weekRunsDistribution
                                                      .Select(r => r.Percentage)
                                                      .Sum(percentage => percentage);

                var easyRunPercent = raceWeekRemainingPercentage / remainingRunningDays;

                var runningDays = _parameters.WeeklyRunningDays.Where(day => day != raceDayOfWeek).ToArray();

                for (int i = 0; i < remainingRunningDays; i++)
                {
                    var day = runningDays[i];

                    // Check if it's the last easy run to add
                    var workoutType = i == remainingRunningDays - 1
                        ? WorkoutType.EasyRunWithStrides
                        : WorkoutType.EasyRun;

                    weekRunsDistribution.Add(new RunDistribution(workoutType, easyRunPercent, day));
                }

                CreateWorkoutsBasedOnDistribution(
                    weekRunsDistribution,
                    weeklyPlan,
                    trainingPaces,
                    workouts,
                    longRun);

                AddTrainingWeekToSchedule(trainingWeeks, weeklyPlan, workouts);

                continue;
            }

            weekRunsDistribution.Add(new RunDistribution(WorkoutType.LongRun, longRunPercent, _parameters.LongRunDay));

            if (_parameters.QualityWorkoutDays.Length != 0)
            {
                var random = new Random();

                var qualityWorkoutPercent = 0.2m;

                foreach (var qualityWorkoutDay in _parameters.QualityWorkoutDays)
                {
                    var workoutType =
                        _parameters.PreferredWorkoutTypes[random.Next(_parameters.PreferredWorkoutTypes.Length)];

                    weekRunsDistribution.Add(
                        new RunDistribution(workoutType, qualityWorkoutPercent, qualityWorkoutDay));

                    qualityWorkoutPercent -= 0.05m;
                }
            }

            var remainingPercentage = 1m -
                                      weekRunsDistribution
                                          .Select(x => x.Percentage)
                                          .Sum(x => x);

            if (nonQualityDays.Length > 0)
            {
                // Distribute remaining percentage among remaining running days
                var secondRunPercentage = remainingPercentage / nonQualityDays.Length * 1.20m; // 20% larger
                var othersRunPercentage = (remainingPercentage - secondRunPercentage) / (nonQualityDays.Length - 1);

                for (int i = 0; i < nonQualityDays.Length; i++)
                {
                    var dayOfWeek = nonQualityDays[i];

                    var runPercentage = nonQualityDays.Length is 1 ? secondRunPercentage :
                        i == 1 ? secondRunPercentage : othersRunPercentage;

                    weekRunsDistribution.Add(
                        new RunDistribution(WorkoutType.EasyRun, Math.Round(runPercentage, 2), dayOfWeek));
                }
            }

            CreateWorkoutsBasedOnDistribution(
                weekRunsDistribution,
                weeklyPlan,
                trainingPaces,
                workouts,
                longRun);

            AddTrainingWeekToSchedule(trainingWeeks, weeklyPlan, workouts);
        }

        return TrainingPlan.Create(trainingWeeks);
    }

    private static void AddTrainingWeekToSchedule(
        List<TrainingWeek> trainingWeeks,
        WeeklyMileageData weeklyPlan,
        Dictionary<DayOfWeek, Workout> workouts)
    {
        trainingWeeks.Add(
            TrainingWeek.Create(
                weekNumber: weeklyPlan.Week,
                trainingPhase: weeklyPlan.TrainingPhase,
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

    private void CreateWorkoutsBasedOnDistribution(
        List<RunDistribution> weekRunsDistribution,
        WeeklyMileageData weeklyPlan,
        TrainingPaces trainingPaces,
        Dictionary<DayOfWeek, Workout> workouts,
        (decimal distance, decimal percent) longRun)
    {
        foreach (var runDistribution in weekRunsDistribution)
        {
            var workoutType = runDistribution.WorkoutType;
            var dayOfWeek = runDistribution.DayOfWeek;
            var workoutPercent = runDistribution.Percentage;
            var workoutDistance = workoutPercent * weeklyPlan.WeeklyMileage;
            var workoutTotalDistance = workoutType is WorkoutType.Race ? longRun.distance : workoutDistance;

            var workoutParameters = new WorkoutParameters(
                weeklyPlan.TrainingPhase,
                _parameters.RunnerLevel,
                trainingPaces,
                weeklyPlan.Week,
                weeklyPlan.PhaseWeek,
                workoutTotalDistance);

            workouts[dayOfWeek] = _workoutGenerator.GenerateWorkout(workoutType, workoutParameters);
        }
    }

    private Dictionary<TrainingPhase, int> GeneratePhaseWeeks(int planWeeks)
    {
        var taperStartWeek = planWeeks - _parameters.TaperWeeks;
        var buildWeeks = (int) Math.Ceiling(taperStartWeek * 0.5);
        var peakWeeks = (int) Math.Ceiling(taperStartWeek * 0.2);
        var baseWeeks = taperStartWeek - buildWeeks - peakWeeks;

        // Generate phases
        var phaseDistribution = new TrainingPhaseDistribution(
            BaseWeeks: baseWeeks,
            BuildWeeks: buildWeeks,
            PeakWeeks: peakWeeks,
            TaperWeeks: _parameters.TaperWeeks
        );

        return phaseDistribution.ToDictionary();
    }

    private List<WeeklyMileageData> CalculateWeeklyMileages(Dictionary<TrainingPhase, int> phaseWeeks)
    {
        // Extract constants for clarity
        const decimal BASE_TARGET_PERCENTAGE = 0.7m;
        const decimal BUILD_START_PERCENTAGE = 0.7m;
        const decimal BUILD_END_PERCENTAGE = 0.9m;
        const decimal PEAK_START_PERCENTAGE = 0.9m;
        const decimal PEAK_INCREMENT = 0.05m;
        const decimal TAPER_REDUCTION_RATE = 0.15m;
        const decimal RACE_WEEK_PERCENTAGE = 0.35m;
        const decimal BASE_STEPBACK_PERCENTAGE = 0.9m;

        List<WeeklyMileageData> mileageByWeek = [];

        // Create each training week
        int weekNumber = 1;

        foreach (var phase in phaseWeeks)
        {
            for (int i = 0; i < phase.Value; i++)
            {
                int phaseWeekNumber = i + 1;
                decimal weeklyMileage;

                switch (phase.Key)
                {
                    case TrainingPhase.Base:
                        weeklyMileage = CalculateBaseWeekMileage(
                            weekNumber,
                            phase.Value,
                            BASE_TARGET_PERCENTAGE,
                            BASE_STEPBACK_PERCENTAGE);

                        break;
                    case TrainingPhase.Build:
                        weeklyMileage = CalculateBuildWeekMileage(
                            weekNumber,
                            phase.Value,
                            phaseWeeks[TrainingPhase.Base],
                            BUILD_START_PERCENTAGE,
                            BUILD_END_PERCENTAGE);

                        break;
                    case TrainingPhase.Peak:
                        weeklyMileage = CalculatePeakWeekMileage(
                            weekNumber,
                            phase.Value,
                            phaseWeeks[TrainingPhase.Taper],
                            PEAK_START_PERCENTAGE,
                            PEAK_INCREMENT);

                        break;
                    case TrainingPhase.Taper:
                        weeklyMileage = CalculateTaperWeekMileage(weekNumber, phase.Value, TAPER_REDUCTION_RATE);

                        break;
                    case TrainingPhase.Race:
                        weeklyMileage = CalculateRaceWeekMileage(RACE_WEEK_PERCENTAGE);
                        weeklyMileage += _parameters.RaceDistance;

                        break;
                    default:
                        throw new InvalidTrainingPlanParametersException(
                            nameof(phase.Key),
                            phase.Key,
                            $"Unsupported training phase: {phase.Key}");
                }

                mileageByWeek.Add(
                    new WeeklyMileageData(
                        weekNumber,
                        phaseWeekNumber,
                        weeklyMileage,
                        phase.Key));

                weekNumber++;
            }
        }

        return mileageByWeek;
    }

    // Private helper methods
    decimal CalculateBaseWeekMileage(
        int currentWeek,
        int totalBaseWeeks,
        decimal targetPercentage,
        decimal reducePercentage)
    {
        var baseProgress = (decimal) currentWeek / totalBaseWeeks;

        var targetMileage = _parameters.CurrentWeeklyMileage +
                            ((_parameters.PeakWeeklyMileage * targetPercentage) -
                             _parameters.CurrentWeeklyMileage) *
                            baseProgress;

        return CalculateWeekMileageConsideringStepBackWeek(
            currentWeek,
            totalBaseWeeks,
            targetMileage,
            reducePercentage: reducePercentage);
    }

    decimal CalculateBuildWeekMileage(
        int currentWeek,
        int totalBuildWeeks,
        int basePhaseLength,
        decimal startPercentage,
        decimal endPercentage)
    {
        var buildWeekNumber = currentWeek - basePhaseLength;
        var buildProgress = (decimal) buildWeekNumber / totalBuildWeeks;
        var startMileage = _parameters.PeakWeeklyMileage * startPercentage;
        var endMileage = _parameters.PeakWeeklyMileage * endPercentage;
        var targetMileage = startMileage + ((endMileage - startMileage) * buildProgress);

        return CalculateWeekMileageConsideringStepBackWeek(
            buildWeekNumber,
            totalBuildWeeks,
            targetMileage);
    }

    decimal CalculatePeakWeekMileage(
        int currentWeek,
        int peakPhaseWeeks,
        int taperPhaseWeeks,
        decimal startPercentage,
        decimal incrementPerWeek)
    {
        int peakWeekNumber = currentWeek - (_parameters.PlanWeeks - peakPhaseWeeks - taperPhaseWeeks) + 1;
        var peakPercentage = startPercentage + (incrementPerWeek * (peakWeekNumber - 1));
        var targetMileage = _parameters.PeakWeeklyMileage * Math.Min(1m, peakPercentage);

        return CalculateWeekMileageConsideringStepBackWeek(
            peakWeekNumber,
            peakPhaseWeeks,
            targetMileage);
    }

    decimal CalculateTaperWeekMileage(int currentWeek, int taperPhaseWeeks, decimal reductionRate)
    {
        var taperReduction = (currentWeek - (_parameters.PlanWeeks - taperPhaseWeeks)) + 1;
        var taperPercent = 1m - (taperReduction * reductionRate);
        var taperWeekMileage = _parameters.PeakWeeklyMileage * taperPercent;

        return Math.Round(taperWeekMileage, 0, MidpointRounding.AwayFromZero);
    }

    decimal CalculateRaceWeekMileage(decimal raceWeekPercentage)
    {
        var raceWeekMileage = _parameters.CurrentWeeklyMileage * raceWeekPercentage;

        return Math.Round(raceWeekMileage, 0, MidpointRounding.AwayFromZero);
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
            return (42.2m, 0.75m); // Marathon distance
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

                return (_parameters.RunnerLevel is ExperienceLevel.Elite
                    ? Math.Min(calculatedDistanceBuild, 35m)
                    : Math.Min(calculatedDistanceBuild, 32m), longRunPercent);
            case TrainingPhase.Peak:
                longRunPercent = 0.4m;
                // Cap at 32km during build for most levels except elite
                var calculatedDistancePeak = weeklyMileage * longRunPercent;

                return (_parameters.RunnerLevel is ExperienceLevel.Elite
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
}
