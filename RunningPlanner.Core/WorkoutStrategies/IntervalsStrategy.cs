using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public class IntervalsStrategy : IWorkoutStrategy
{
    public Workout Generate(WorkoutParameters parameters)
    {
        // Base interval parameters based on experience level
        (int baseRepeats, int baseMeters) = parameters.ExperienceLevel switch
        {
            ExperienceLevel.Beginner => (3, 400),
            ExperienceLevel.Novice => (4, 400),
            ExperienceLevel.Intermediate => (5, 400),
            ExperienceLevel.Advanced => (4, 800),
            ExperienceLevel.Elite => (5, 800),
            _ => (4, 400)
        };

        // Progressive interval repeats based on phase and week
        int repeats = parameters.Phase switch
        {
            TrainingPhase.Base => baseRepeats + (parameters.PhaseWeekNumber / 2),
            TrainingPhase.Build => baseRepeats + 2 + (parameters.PhaseWeekNumber / 2),
            TrainingPhase.Peak => baseRepeats + 3 + (parameters.PhaseWeekNumber / 3),
            TrainingPhase.Taper => Math.Max(baseRepeats - 2, baseRepeats - parameters.PhaseWeekNumber),
            _ => baseRepeats
        };

        // Progressive interval distance based on phase
        int meters = parameters.Phase switch
        {
            TrainingPhase.Base => baseMeters,
            TrainingPhase.Build => IsEvenWeek(parameters.WeekNumber)
                ? baseMeters
                : baseMeters * 2, // Alternate between shorter/longer
            TrainingPhase.Peak => baseMeters * 2, // Longer intervals at peak
            TrainingPhase.Taper => baseMeters, // Back to shorter intervals during taper
            _ => baseMeters
        };

        // Cap repeats based on experience level
        int maxRepeats = parameters.ExperienceLevel switch
        {
            ExperienceLevel.Beginner => 6,
            ExperienceLevel.Novice => 8,
            ExperienceLevel.Intermediate => 10,
            ExperienceLevel.Advanced => 12,
            ExperienceLevel.Elite => 16,
            _ => 8
        };

        repeats = Math.Min(repeats, maxRepeats);

        // Calculate recovery distance based on interval distance and phase
        int recoveryMeters = CalculateRecoveryMeters(meters, parameters.Phase);

        var currentWorkoutRepeatsTotalDistance = repeats * (meters + recoveryMeters) / 1000m;
        var remainingDistance = parameters.TotalDistance - currentWorkoutRepeatsTotalDistance;
        var warmupCooldownDistance = 0m;

        if (remainingDistance >= 2m)
        {
            warmupCooldownDistance = remainingDistance / 2;
        }
        else
        {
            while (remainingDistance < 2m)
            {
                currentWorkoutRepeatsTotalDistance = repeats * (meters + recoveryMeters) / 1000m;
                remainingDistance = parameters.TotalDistance - currentWorkoutRepeatsTotalDistance;
                warmupCooldownDistance = remainingDistance / 2;

                repeats--;
            }
        }

        // Convert meters to kilometers for intervals
        decimal intervalDistance = meters / 1000.0m;
        decimal recoveryDistance = recoveryMeters / 1000.0m;

        // Total distance for intervals and recovery
        decimal intervalTotalDistance = repeats * (intervalDistance + recoveryDistance);

        var currentWorkoutTotalDistance = warmupCooldownDistance +
                                          intervalTotalDistance +
                                          warmupCooldownDistance +
                                          (parameters.RestBeforeStartIntervalDistance ?? 0.0m);

        // Additional easy distance if needed to match total
        remainingDistance = currentWorkoutTotalDistance - parameters.TotalDistance;
        decimal additionalEasyDistance = Math.Max(0, remainingDistance);

        var warmUpStep = SimpleStep.CreateWithKilometers(
            StepType.WarmUp,
            warmupCooldownDistance,
            IntensityTarget.Pace(parameters.Paces.EasyPace.Min, parameters.Paces.EasyPace.Max));

        var steps = new List<SimpleStep>();

        for (var i = 0; i < repeats; i++)
        {
            var runStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Run,
                    intervalDistance,
                    IntensityTarget.Pace(parameters.Paces.IntervalPace.Min, parameters.Paces.IntervalPace.Max));

            var recoveryStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Recover,
                    recoveryDistance,
                    IntensityTarget.Pace(parameters.Paces.RecoveryPace.Min, parameters.Paces.RecoveryPace.Max));

            steps.Add(runStep);
            steps.Add(recoveryStep);
        }

        var totalRepeats = repeats * 2;

        var repeatStep = Repeat.Create(totalRepeats, steps);

        var coolDownStep = SimpleStep.CreateWithKilometers(
            StepType.CoolDown,
            warmupCooldownDistance + additionalEasyDistance,
            IntensityTarget.Pace(parameters.Paces.EasyPace.Min, parameters.Paces.EasyPace.Max));

        var workoutSteps = new List<Step>();
        workoutSteps.Add(warmUpStep);

        if (parameters.RestBeforeStartIntervalDistance.HasValue)
        {
            var restStep = SimpleStep.CreateWithKilometers(
                StepType.Rest,
                parameters.RestBeforeStartIntervalDistance.Value,
                IntensityTarget.Pace(parameters.Paces.EasyPace.Min, parameters.Paces.EasyPace.Max));
            workoutSteps.Add(restStep);
        }

        workoutSteps.Add(repeatStep);
        workoutSteps.Add(coolDownStep);

        return
            Workout.Create(
                WorkoutType.Intervals,
                workoutSteps);
    }

    private bool IsEvenWeek(int weekNumber) => weekNumber % 2 is 0;

    private int CalculateRecoveryMeters(int intervalMeters, TrainingPhase phase)
    {
        // In Base/Taper: longer recovery, in Build/Peak: shorter recovery
        decimal recoveryRatio = phase switch
        {
            TrainingPhase.Base => 1.0m,
            TrainingPhase.Build => 0.75m,
            TrainingPhase.Peak => 0.5m,
            TrainingPhase.Taper => 1.0m,
            _ => 0.75m
        };

        return (int) (intervalMeters * recoveryRatio);
    }
}
