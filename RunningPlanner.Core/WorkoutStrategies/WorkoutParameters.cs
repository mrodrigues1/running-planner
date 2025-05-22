using RunningPlanner.Core.Models;
using RunningPlanner.Core.Models.Paces;

namespace RunningPlanner.Core.WorkoutStrategies;

public record WorkoutParameters(
    TrainingPhase Phase,
    ExperienceLevel ExperienceLevel,
    TrainingPaces Paces,
    int WeekNumber,
    int PhaseWeekNumber,
    decimal TotalDistance,
    decimal? RestBeforeStartIntervalDistance);
