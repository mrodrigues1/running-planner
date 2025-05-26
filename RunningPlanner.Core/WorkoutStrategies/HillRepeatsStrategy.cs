using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public class HillRepeatsStrategy : IWorkoutStrategy
{
    public Workout Generate(WorkoutParameters parameters)
    {
        // Base hill parameters based on experience level
        int baseRepeats = parameters.ExperienceLevel switch
        {
            ExperienceLevel.Beginner => 4,
            ExperienceLevel.Novice => 5,
            ExperienceLevel.Intermediate => 6,
            ExperienceLevel.Advanced => 8,
            ExperienceLevel.Elite => 10,
            _ => 6
        };
        
        // hill distance parameters based on experience level
        decimal hillDistance = parameters.ExperienceLevel switch
        {
            ExperienceLevel.Beginner => 100m,
            ExperienceLevel.Novice => 200m,
            ExperienceLevel.Intermediate => 300m,
            ExperienceLevel.Advanced => 400m,
            ExperienceLevel.Elite => 400m,
            _ => 200m
        };

        // Progressive hill repeats based on phase and week
        int repeats = parameters.Phase switch
        {
            TrainingPhase.Base => baseRepeats + (parameters.PhaseWeekNumber / 2),
            TrainingPhase.Build => baseRepeats + 2 + (parameters.PhaseWeekNumber / 2),
            TrainingPhase.Peak => baseRepeats + 4,
            TrainingPhase.Taper => Math.Max(baseRepeats - 2, baseRepeats - parameters.PhaseWeekNumber),
            _ => baseRepeats
        };

        // Cap repeats based on experience level
        int maxRepeats = parameters.ExperienceLevel switch
        {
            ExperienceLevel.Beginner => 4,
            ExperienceLevel.Novice => 6,
            ExperienceLevel.Intermediate => 8,
            ExperienceLevel.Advanced => 10,
            ExperienceLevel.Elite => 12,
            _ => 10
        };

        repeats = Math.Min(repeats, maxRepeats);

        var currentRepeatsTotalDistance = repeats * (hillDistance + hillDistance) / 1000m;
        var remainingDistance = parameters.TotalDistance - currentRepeatsTotalDistance;
        var warmupCooldownDistance = 0m;

        if (remainingDistance >= 2m)
        {
            warmupCooldownDistance = remainingDistance / 2;
        }
        else
        {
            while (remainingDistance < 2m)
            {
                currentRepeatsTotalDistance = repeats * (hillDistance + hillDistance) / 1000m;
                remainingDistance = parameters.TotalDistance - currentRepeatsTotalDistance;
                warmupCooldownDistance = remainingDistance / 2;

                repeats--;
            }
        }

        remainingDistance = parameters.TotalDistance - (warmupCooldownDistance + warmupCooldownDistance);

        // Each hill repeat is approximate:
        decimal totalRepeatDistance = repeats * (hillDistance + hillDistance);

        // If there's additional distance to cover, we'll add it as an easy run
        decimal additionalEasyDistance = Math.Max(0, remainingDistance - totalRepeatDistance);

        var warmUpStep = SimpleStep.CreateWithKilometers(
            StepType.WarmUp,
            warmupCooldownDistance,
            IntensityTarget.Pace(parameters.Paces.EasyPace));

        var steps = new List<SimpleStep>();

        for (var i = 0; i < repeats; i++)
        {
            var runStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Run,
                    hillDistance,
                    IntensityTarget.Pace(parameters.Paces.IntervalPace));

            var recoverStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Recover,
                    hillDistance,
                    IntensityTarget.Pace(parameters.Paces.RecoveryPace));

            steps.Add(runStep);
            steps.Add(recoverStep);
        }

        var totalRepeats = repeats * 2;

        var repeatStep = Repeat.Create(totalRepeats, steps);

        var coolDownStep = SimpleStep.CreateWithKilometers(
            StepType.CoolDown,
            warmupCooldownDistance + additionalEasyDistance,
            IntensityTarget.Pace(parameters.Paces.EasyPace));

        return Workout.Create(
            WorkoutType.HillRepeat,
            new List<Step>
            {
                warmUpStep,
                repeatStep,
                coolDownStep
            });
    }
}
