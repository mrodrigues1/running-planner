using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public class RacePaceStrategy : IWorkoutStrategy
{
    public Workout Generate(WorkoutParameters parameters)
    {
        // Base race pace segments based on experience and phase
        decimal racePaceDistance = parameters.Phase switch
        {
            TrainingPhase.Base => parameters.TotalDistance * 0.2m,
            TrainingPhase.Build => parameters.TotalDistance * (0.3m + (parameters.PhaseWeekNumber * 0.03m)),
            TrainingPhase.Peak => parameters.TotalDistance * (0.5m + (parameters.PhaseWeekNumber * 0.05m)),
            TrainingPhase.Taper => parameters.TotalDistance *
                                   Math.Max(0.2m, 0.4m - (parameters.PhaseWeekNumber * 0.1m)),
            _ => parameters.TotalDistance * 0.25m
        };

        // Cap based on experience level
        decimal maxRacePacePercent = parameters.ExperienceLevel switch
        {
            ExperienceLevel.Beginner => 0.3m,
            ExperienceLevel.Novice => 0.4m,
            ExperienceLevel.Intermediate => 0.6m,
            ExperienceLevel.Advanced => 0.7m,
            ExperienceLevel.Elite => 0.8m,
            _ => 0.5m
        };

        racePaceDistance = Math.Min(racePaceDistance, parameters.TotalDistance * maxRacePacePercent);

        // Create segments: warmup, race pace, cool down
        var warmupDistance = (parameters.TotalDistance - racePaceDistance) * 0.5m;
        var cooldownDistance = parameters.TotalDistance - racePaceDistance - warmupDistance;

        // Build the workout with segments
        var warmupStep = SimpleStep.CreateWithKilometers(
            StepType.WarmUp,
            warmupDistance,
            parameters.Paces.EasyPace);

        var racePaceStep = SimpleStep.CreateWithKilometers(
            StepType.Run,
            racePaceDistance,
            parameters.Paces.MarathonPace);

        var cooldownStep = SimpleStep.CreateWithKilometers(
            StepType.CoolDown,
            cooldownDistance,
            parameters.Paces.EasyPace);

        return Workout.Create(
            WorkoutType.RacePace,
            new List<Step> {warmupStep, racePaceStep, cooldownStep});
    }
}
