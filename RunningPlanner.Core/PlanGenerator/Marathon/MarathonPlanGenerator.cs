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

        var basePercentage = 0.15m;
        var buildPercentage = 0.5m;
        var peakPercentage = 0.2m;
        var taperPlusRacePercentage = 0.15m;

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

        List<(int week, decimal weeklyMileages, TrainingPhase trainingPhase)> mileageByWeek = [];

        // Create each training week
        int weekNumber = 1;

        foreach (var phase in phaseWeeks)
        {
            for (int i = 0; i < phase.Value; i++)
            {
                if (phase.Key == TrainingPhase.Base)
                {
                    // Linear progression from current to 75% of peak during base phase
                    var basePhaseWeeks = phase.Value;
                    var baseProgress = (decimal) weekNumber / basePhaseWeeks;

                    var baseWeekMileage = _parameters.CurrentWeeklyMileage +
                                          ((_parameters.PeakWeeklyMileage * 0.75m) - _parameters.CurrentWeeklyMileage) *
                                          baseProgress;
                    mileageByWeek.Add((weekNumber, baseWeekMileage, TrainingPhase.Base));
                    weekNumber++;

                    continue;
                }

                if (phase.Key == TrainingPhase.Build)
                {
                    // Build phase: progress from 75% to 100% of peak
                    var totalBuildWeeks = phase.Value;
                    var basePhaseLength = phaseWeeks[TrainingPhase.Base];
                    var buildWeekNumber = weekNumber - basePhaseLength;
                    var buildProgress = (decimal) buildWeekNumber / totalBuildWeeks;

                    // Every third week is a step-back week (except near the peak)
                    bool isStepBackWeek = buildWeekNumber % 3 == 0 && buildWeekNumber < totalBuildWeeks - 1;

                    var targetMileage = (_parameters.PeakWeeklyMileage * 0.75m) +
                                        ((_parameters.PeakWeeklyMileage - _parameters.PeakWeeklyMileage * 0.75m) *
                                         buildProgress);

                    var buildWeekMileage = isStepBackWeek ? targetMileage * 0.80m : targetMileage;

                    mileageByWeek.Add((weekNumber, buildWeekMileage, TrainingPhase.Build));

                    weekNumber++;

                    continue;
                }

                if (phase.Key == TrainingPhase.Taper)
                {
                    var taperReduction = (decimal) _parameters.TaperWeeks -
                                         (weekNumber - (_parameters.PlanWeeks - _parameters.TaperWeeks)) +
                                         1;
                    var taperPercent = 1m - (taperReduction * 0.15m); // Reduce by ~15% each taper week

                    var taperWeekMileage = _parameters.PeakWeeklyMileage * taperPercent;

                    mileageByWeek.Add((weekNumber, taperWeekMileage, TrainingPhase.Taper));
                    weekNumber++;

                    continue;
                }

                // Calculate progressive weekly mileage
                if (phase.Key == TrainingPhase.Race)
                {
                    var raceWeekMileage =
                        _parameters.CurrentWeeklyMileage * 0.5m; // Race week is typically around 50% volume
                    mileageByWeek.Add((weekNumber, raceWeekMileage, TrainingPhase.Race));
                    weekNumber++;

                    continue;
                }
            }
        }


        return null!;
    }
}
