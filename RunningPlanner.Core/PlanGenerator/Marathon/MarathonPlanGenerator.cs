using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.PlanGenerator.Marathon;

public class MarathonPlanGeneratorParameters
{
    // Required parameters
    public DateTime RaceDate { get; set; }
    public ExperienceLevel RunnerLevel { get; set; }
    public int WeeklyRunningDays { get; set; }
    public decimal CurrentWeeklyMileage { get; set; }
    public decimal RaceDistance { get; set; }
    public decimal PeakWeeklyMileage { get; set; }

    // Optional parameters with sensible defaults
    public int QualityWorkoutDays { get; set; } = 2;
    public List<DayOfWeek> QualityWorkoutDaysTest { get; set; } = [];
    public DayOfWeek LongRunDay { get; set; } = DayOfWeek.Saturday;
    public int TaperWeeks { get; set; } = 3;
    public List<WorkoutType> PreferredWorkoutTypes { get; set; } = [];

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

        var phaseWeeks = GeneratePhaseWeeks(planWeeks);

        var weeklyMileages = CalculateWeeklyMileage(phaseWeeks);

        return null!;
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

    private List<(int week, decimal weeklyMileage, TrainingPhase trainingPhase)?> CalculateWeeklyMileage(
        Dictionary<TrainingPhase, int> phaseWeeks)
    {
        List<(int week, decimal weeklyMileage, TrainingPhase trainingPhase)?> mileageByWeek = [];

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
                            _parameters.CurrentWeeklyMileage * 0.5m; // Race week is typically around 50% volume

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
}
