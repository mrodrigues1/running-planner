namespace RunningPlanner.Core.Models;

public record WeeklyMileageData(
    int Week,
    int PhaseWeek,
    decimal WeeklyMileage,
    TrainingPhase TrainingPhase);
