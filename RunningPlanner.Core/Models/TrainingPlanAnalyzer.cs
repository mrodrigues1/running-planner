namespace RunningPlanner.Core.Models;

public class TrainingPlanAnalyzer
{
    private readonly TrainingPlan _trainingPlan;

    public TrainingPlanAnalyzer(TrainingPlan trainingPlan)
    {
        _trainingPlan = trainingPlan ?? throw new ArgumentNullException(nameof(trainingPlan));
    }

    /// <summary>
    /// Gets the total number of weeks in the training plan
    /// </summary>
    public int TotalWeeks => _trainingPlan.TrainingWeeks.Count;

    /// <summary>
    /// Calculates the total distance across all training weeks
    /// </summary>
    public decimal TotalDistance => Math.Round(
        _trainingPlan.TrainingWeeks.Sum(week => week.TotalKilometerMileage),
        0,
        MidpointRounding.AwayFromZero);

    /// <summary>
    /// Calculates the average weekly distance
    /// </summary>
    public decimal AverageWeeklyDistance => Math.Round(TotalDistance / TotalWeeks, 0, MidpointRounding.AwayFromZero);

    /// <summary>
    /// Finds the week with the highest total distance
    /// </summary>
    public TrainingWeek PeakWeek => _trainingPlan.TrainingWeeks
        .OrderByDescending(week => week.TotalKilometerMileage)
        .First();

    /// <summary>
    /// Calculates the week-to-week distance progression as percentages
    /// </summary>
    private IEnumerable<decimal> WeeklyProgressionPercentages()
    {
        for (int i = 1; i < _trainingPlan.TrainingWeeks.Count; i++)
        {
            decimal previousDistance = _trainingPlan.TrainingWeeks[i - 1].TotalKilometerMileage;
            decimal currentDistance = _trainingPlan.TrainingWeeks[i].TotalKilometerMileage;

            if (previousDistance > 0)
            {
                var weeklyProgressionPercentages = (currentDistance - previousDistance) / previousDistance * 100;

                yield return Math.Round(weeklyProgressionPercentages, 0, MidpointRounding.AwayFromZero);
            }
            else
            {
                yield return 0;
            }
        }
    }

    /// <summary>
    /// Gets the average week-to-week progression percentage
    /// </summary>
    public decimal AverageProgressionPercentage =>
        Math.Round(WeeklyProgressionPercentages().DefaultIfEmpty(0).Average(), 1);

    /// <summary>
    /// Determines if the training plan follows the 10% rule
    /// (no week increases distance by more than 10% compared to the previous week)
    /// </summary>
    public bool FollowsTenPercentRule =>
        !WeeklyProgressionPercentages().Any(percentage => percentage > 10);

    /// <summary>
    /// Gets the percentage of each workout type based on total distance throughout the training plan.
    /// </summary>
    public Dictionary<WorkoutType, decimal> WorkoutTypeDistribution
    {
        get
        {
            var result = new Dictionary<WorkoutType, decimal>();

            // Collect all workouts from all training weeks
            var allWorkouts = _trainingPlan.TrainingWeeks
                .SelectMany(week => week.Workouts.Where(w => w.Type != WorkoutType.Rest))
                .ToList();

            if (allWorkouts.Count == 0)
            {
                return result;
            }

            // Group workouts by type and count them
            var workoutCounts = allWorkouts
                .GroupBy(workout => workout.Type)
                .ToDictionary(
                    group => group.Key,
                    group => group.Count()
                );

            // Calculate percentages
            decimal totalWorkouts = allWorkouts.Count;

            foreach (var kvp in workoutCounts)
            {
                decimal percentage = (kvp.Value / totalWorkouts) * 100;
                result.Add(kvp.Key, Math.Round(percentage, 0, MidpointRounding.AwayFromZero));
            }

            return result;
        }
    }

    /// <summary>
    /// Analyzes whether the training plan has a proper taper period before the goal race
    /// </summary>
    public bool HasProperTaper
    {
        get
        {
            // Find all weeks marked as Taper phase
            var taperWeeks = _trainingPlan.TrainingWeeks
                .Where(w => w.TrainingPhase == TrainingPhase.Taper)
                .ToList();

            if (taperWeeks.Count == 0)
                return false; // No taper weeks defined

            // Find pre-taper weeks to establish baseline
            var preTaperWeeks = _trainingPlan.TrainingWeeks
                .Where(w => w.TrainingPhase != TrainingPhase.Taper)
                .ToList();

            if (preTaperWeeks.Count == 0)
                return false; // No pre-taper weeks to compare against

            // Calculate average distance of pre-taper weeks
            var preTaperAvgDistance = preTaperWeeks
                .Average(w => w.TotalKilometerMileage);

            // Check if taper has progressive reduction
            bool hasProgression = true;
            decimal lastWeekDistance = decimal.MaxValue;

            // Order taper weeks chronologically and check for progressive reduction
            foreach (var week in taperWeeks.OrderBy(w => w.WeekNumber))
            {
                if (week.TotalKilometerMileage > lastWeekDistance)
                {
                    hasProgression = false;

                    break;
                }

                lastWeekDistance = week.TotalKilometerMileage;
            }

            // Final race week should have significant reduction (at least 30%)
            var finalTaperWeek = taperWeeks.OrderBy(w => w.WeekNumber).Last();
            var finalWeekReduction = 1 - (finalTaperWeek.TotalKilometerMileage / preTaperAvgDistance);

            // A proper taper should have:
            // 1. At least one week marked as Taper
            // 2. Progressive reduction in volume
            // 3. Final week should have at least 30% reduction from pre-taper average
            return hasProgression && finalWeekReduction >= 0.3m;
        }
    }

    /// <summary>
    /// Calculates the percentage reduction in volume during the taper period
    /// </summary>
    public decimal TaperingReductionPercentage
    {
        get
        {
            var taperWeeks = _trainingPlan.TrainingWeeks
                .Where(w => w.TrainingPhase == TrainingPhase.Taper)
                .ToList();

            if (taperWeeks.Count == 0)
                return 0m;

            var preTaperWeeks = _trainingPlan.TrainingWeeks
                .Where(w => w.TrainingPhase != TrainingPhase.Taper)
                .ToList();

            if (preTaperWeeks.Count == 0)
                return 0m;

            var preTaperAvgDistance = preTaperWeeks
                .Average(w => w.TotalKilometerMileage);

            var taperAvgDistance = taperWeeks
                .Average(w => w.TotalKilometerMileage);

            // Calculate percentage reduction
            var taperingReductionPercentage = (1 - (taperAvgDistance / preTaperAvgDistance)) * 100m;

            return Math.Round(taperingReductionPercentage, 0, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// Calculates the percentage reduction in mileage for each week of the taper phase
    /// compared to the average baseline mileage of the preceding training phases.
    /// </summary>
    public Dictionary<string, decimal> TaperBreakdown
    {
        get
        {
            var result = new Dictionary<string, decimal>();

            var taperWeeks = _trainingPlan.TrainingWeeks
                .Where(w => w.TrainingPhase == TrainingPhase.Taper)
                .OrderBy(w => w.WeekNumber)
                .ToList();

            if (taperWeeks.Count == 0)
                return result;

            var preTaperWeeks = _trainingPlan.TrainingWeeks
                .Where(w => w.TrainingPhase != TrainingPhase.Taper)
                .ToList();

            if (preTaperWeeks.Count == 0)
                return result;

            var baselineVolume = preTaperWeeks.Average(w => w.TotalKilometerMileage);

            // Calculate reduction for each taper week
            for (int i = 0; i < taperWeeks.Count; i++)
            {
                var week = taperWeeks[i];
                var weekNumber = i + 1;
                var reduction = (1 - (week.TotalKilometerMileage / baselineVolume)) * 100m;
                result[$"Taper Week {weekNumber} Reduction"] = Math.Round(reduction, 0, MidpointRounding.AwayFromZero);
            }

            return result;
        }
    }

    /// <summary>
    /// Gets the duration and distribution of each training phase in the plan
    /// </summary>
    public Dictionary<TrainingPhase, PhaseAnalysis> PhaseDurations
    {
        get
        {
            var result = new Dictionary<TrainingPhase, PhaseAnalysis>();

            if (!_trainingPlan.TrainingWeeks.Any())
                return result;

            var totalWeeks = _trainingPlan.TrainingWeeks.Count;
            var totalPlanKilometers = _trainingPlan.TrainingWeeks.Sum(w => w.TotalKilometerMileage);

            // Group weeks by phase
            var phaseGroups = _trainingPlan.TrainingWeeks
                .GroupBy(w => w.TrainingPhase)
                .Where(g => g.Key != TrainingPhase.Invalid); // Exclude invalid phase

            foreach (var phase in phaseGroups)
            {
                var phaseWeekCount = phase.Count();
                var phaseKilometers = phase.Sum(w => w.TotalKilometerMileage);

                result[phase.Key] = new PhaseAnalysis
                {
                    WeekCount = phaseWeekCount,
                    PercentageOfTotalPlan = Math.Round((decimal) phaseWeekCount / totalWeeks * 100, 0,
                        MidpointRounding.AwayFromZero),
                    TotalKilometers = Math.Round(phaseKilometers, 0, MidpointRounding.AwayFromZero),
                    AverageWeeklyKilometers =
                        Math.Round(phaseKilometers / phaseWeekCount, 0, MidpointRounding.AwayFromZero),
                    PercentageOfTotalDistance = Math.Round((phaseKilometers / totalPlanKilometers) * 100, 0,
                        MidpointRounding.AwayFromZero),
                    HighestWeekKilometers = phase.Max(w => w.TotalKilometerMileage),
                    LowestWeekKilometers = phase.Min(w => w.TotalKilometerMileage)
                };
            }

            return result;
        }
    }

    /// <summary>
    /// Gets a summary of phase mileage progression
    /// </summary>
    public IEnumerable<string> PhaseMileageProgressionAnalysis
    {
        get
        {
            var analysis = new List<string>();
            var phaseDurations = PhaseDurations;

            if (phaseDurations.Count == 0)
                return analysis;

            foreach (var phase in phaseDurations)
            {
                string phaseAnalysis = string.Empty;
                phaseAnalysis += $"{phase.Key} Phase:{Environment.NewLine}";
                phaseAnalysis += $"- Weeks: {phase.Value.WeekCount}{Environment.NewLine}";
                phaseAnalysis += $"- Total Distance: {phase.Value.TotalKilometers:F1} km{Environment.NewLine}";
                phaseAnalysis += $"- Average Weekly: {phase.Value.AverageWeeklyKilometers:F1} km{Environment.NewLine}";

                phaseAnalysis +=
                    $"- Range: {phase.Value.LowestWeekKilometers:F1} - {phase.Value.HighestWeekKilometers:F1} km{Environment.NewLine}";

                phaseAnalysis +=
                    $"- Percentage of Total Plan Distance: {phase.Value.PercentageOfTotalDistance:F1}%{Environment.NewLine}";

                phaseAnalysis +=
                    $"- Percentage of Total Plan Duration: {phase.Value.PercentageOfTotalPlan:F1}%{Environment.NewLine}";
                analysis.Add(phaseAnalysis);
            }

            // Analyze progression between phases
            var orderedPhases = _trainingPlan.TrainingWeeks
                .GroupBy(w => w.TrainingPhase)
                .OrderBy(g => g.Min(w => _trainingPlan.TrainingWeeks.IndexOf(w)))
                .Select(g => g.Key)
                .Where(p => p != TrainingPhase.Invalid);

            (PhaseAnalysis Analysis, TrainingPhase TrainingPhase)? previousPhase = null;

            foreach (var phaseType in orderedPhases)
            {
                if (!phaseDurations.TryGetValue(phaseType, out var currentPhase)) continue;

                if (previousPhase != null)
                {
                    var averageChange =
                        ((currentPhase.AverageWeeklyKilometers - previousPhase.Value.Analysis.AverageWeeklyKilometers)
                         / previousPhase.Value.Analysis.AverageWeeklyKilometers) * 100;

                    analysis.Add(
                        $"Transition from {previousPhase.Value.TrainingPhase} to {phaseType}: " +
                        $"{averageChange:F1}% change in average weekly distance");
                }

                previousPhase = (currentPhase, phaseType);
            }

            return analysis;
        }
    }
}