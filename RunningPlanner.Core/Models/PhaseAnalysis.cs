namespace RunningPlanner.Core.Models;

/// <summary>
/// Contains duration, percentage, and mileage information for a training phase
/// </summary>
public class PhaseAnalysis
{
    public int WeekCount { get; set; }
    public decimal PercentageOfTotalPlan { get; set; }
    public decimal TotalKilometers { get; set; }
    public decimal AverageWeeklyKilometers { get; set; }
    public decimal PercentageOfTotalDistance { get; set; }
    public decimal HighestWeekKilometers { get; set; }
    public decimal LowestWeekKilometers { get; set; }
}


