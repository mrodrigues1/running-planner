using RunningPlanner.Core.Extensions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public class EasyRunWithStridesStrategy : IWorkoutStrategy
{
    private const decimal StrideDistanceMeters = 100m;

    public Workout Generate(WorkoutParameters parameters)
    {
        // Base strides based on experience level
        int baseStrides = parameters.ExperienceLevel switch
        {
            ExperienceLevel.Beginner => 4,
            ExperienceLevel.Novice => 6,
            ExperienceLevel.Intermediate => 8,
            ExperienceLevel.Advanced => 10,
            ExperienceLevel.Elite => 12,
            _ => 6
        };

        // Progressive strides based on phase and week
        int strides = parameters.Phase switch
        {
            TrainingPhase.Base => baseStrides + (parameters.PhaseWeekNumber / 2),
            TrainingPhase.Build => baseStrides + (parameters.PhaseWeekNumber / 2),
            TrainingPhase.Peak => baseStrides,
            TrainingPhase.Taper => Math.Max(baseStrides - 2, baseStrides - parameters.PhaseWeekNumber),
            _ => baseStrides
        };

        // Cap strides based on experience level
        int maxStrides = parameters.ExperienceLevel switch
        {
            ExperienceLevel.Beginner => 8,
            ExperienceLevel.Novice => 10,
            ExperienceLevel.Intermediate => 12,
            ExperienceLevel.Advanced => 16,
            ExperienceLevel.Elite => 20,
            _ => 10
        };

        strides = Math.Min(strides, maxStrides);

        var strideDistancePlusRecovery = StrideDistanceMeters * 2;

        var strideTotalDistance = strides * strideDistancePlusRecovery / 1000m;

        var currentTotalDistance = parameters.TotalDistance - strideTotalDistance;

        while (currentTotalDistance < 0m)
        {
            strideTotalDistance = strides * strideDistancePlusRecovery / 1000m;
            currentTotalDistance = parameters.TotalDistance - strideTotalDistance;

            strides--;
        }

        var steps = new List<SimpleStep>();

        for (var i = 0; i < strides; i++)
        {
            var runSimpleStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Run,
                    StrideDistanceMeters / 1000m,
                    IntensityTarget.Pace(parameters.Paces.RepetitionPace.Min, parameters.Paces.RepetitionPace.Max));

            var restSimpleStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Recover,
                    StrideDistanceMeters / 1000m,
                    IntensityTarget.Pace(parameters.Paces.EasyPace.Min, parameters.Paces.EasyPace.Max));

            steps.Add(runSimpleStep);
            steps.Add(restSimpleStep);
        }

        var totalRepeats = strides * 2;

        var repeatStep = Repeat.Create(totalRepeats, steps);

        var runStep = SimpleStep.CreateWithKilometers(
            StepType.Run,
            parameters.TotalDistance - repeatStep.Steps.StepsDistance(),
            IntensityTarget.Pace(parameters.Paces.EasyPace.Min, parameters.Paces.EasyPace.Max));

        return Workout.Create(
            WorkoutType.EasyRunWithStrides,
            new List<Step>
            {
                runStep,
                repeatStep
            });
    }
}
