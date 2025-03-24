using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.PlanGenerator.Test2;

public class MarathonPlanParameters
{
    // Required parameters
    public DateTime RaceDate { get; set; }
    public ExperienceLevel RunnerLevel { get; set; }
    public int WeeklyRunningDays { get; set; } = 4; // Default value
    
    // Optional parameters with sensible defaults
    public int QualityWorkoutDays { get; set; } = 2;
    public DayOfWeek LongRunDay { get; set; } = DayOfWeek.Sunday;
    public decimal CurrentWeeklyMileage { get; set; } = 20m; // in km
    public decimal PeakWeeklyMileage { get; set; } = 50m; // in km
    public int TaperWeeks { get; set; } = 3;
    public bool IncludeSpeedWork { get; set; } = true;
    public bool IncludeTempoRuns { get; set; } = true;
    public bool IncludeHillWork { get; set; } = false;
    public bool IncludeRacePaceRuns { get; set; } = true;
    public List<WorkoutType> PreferredWorkoutTypes { get; set; } = new();
    
    // Derived properties
    public int PlanWeeks => CalculatePlanWeeks();
    
    private int CalculatePlanWeeks()
    {
        // Default to 18 weeks if race is far enough, otherwise adjust accordingly
        var weeksUntilRace = (int)Math.Ceiling((RaceDate - DateTime.Today).TotalDays / 7.0);
        return Math.Min(weeksUntilRace, 18);
    }
}