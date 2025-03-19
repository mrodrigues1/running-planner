namespace RunningPlanner.Core.Models;

public record PhaseAnalysis(
    int WeekCount,
    decimal PercentageOfTotalPlan,
    decimal TotalKilometers,
    decimal AverageWeeklyKilometers,
    decimal PercentageOfTotalDistance,
    decimal HighestWeekKilometers,
    decimal LowestWeekKilometers);