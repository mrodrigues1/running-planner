using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.WorkoutStrategies;

public class TempoRunStrategy : IWorkoutStrategy
{
    public Workout Generate(WorkoutParameters parameters)
    {
        // Base tempo minutes based on experience level
        int baseTempoMinutes = parameters.ExperienceLevel switch
        {
            ExperienceLevel.Beginner => 10,
            ExperienceLevel.Novice => 15,
            ExperienceLevel.Intermediate => 20,
            ExperienceLevel.Advanced => 25,
            ExperienceLevel.Elite => 30,
            _ => 15
        };

        // Progressive tempo minutes based on phase and week number
        int tempoMinutes = parameters.Phase switch
        {
            TrainingPhase.Base => baseTempoMinutes + (parameters.PhaseWeekNumber * 2 / 3), // Gentle progression
            TrainingPhase.Build => baseTempoMinutes + 5 + (parameters.PhaseWeekNumber), // Steeper progression
            TrainingPhase.Peak => baseTempoMinutes + 10 + (parameters.PhaseWeekNumber / 2), // Maintain high volume
            TrainingPhase.Taper => Math.Max(
                baseTempoMinutes / 2,
                baseTempoMinutes - (parameters.PhaseWeekNumber * 3)), // Reduction
            _ => baseTempoMinutes
        };

        // Cap tempo minutes based on experience level
        int maxTempoMinutes = parameters.ExperienceLevel switch
        {
            ExperienceLevel.Beginner => 20,
            ExperienceLevel.Novice => 30,
            ExperienceLevel.Intermediate => 40,
            ExperienceLevel.Advanced => 50,
            ExperienceLevel.Elite => 50,
            _ => 30
        };

        tempoMinutes = Math.Min(tempoMinutes, maxTempoMinutes);


        var tempoDistance = CalculateDistanceBasedOnDuration(
            TimeSpan.FromMinutes(tempoMinutes),
            parameters.Paces.ThresholdPace);

        var remainingDistance = parameters.TotalDistance - tempoDistance;

        while (remainingDistance <= 0)
        {
            tempoDistance = CalculateDistanceBasedOnDuration(
                TimeSpan.FromMinutes(tempoMinutes),
                parameters.Paces.ThresholdPace);
            remainingDistance = parameters.TotalDistance - tempoDistance;
            tempoMinutes--;
        }

        var warmupCooldownDistance = remainingDistance / 2;

        var warmUpStep = SimpleStep.CreateWithKilometers(
            StepType.WarmUp,
            warmupCooldownDistance,
            IntensityTarget.Pace(parameters.Paces.EasyPace.Min, parameters.Paces.EasyPace.Max));

        var tempoStep = SimpleStep.CreateWithKilometers(
            StepType.Run,
            tempoDistance,
            IntensityTarget.Pace(parameters.Paces.ThresholdPace.Min, parameters.Paces.ThresholdPace.Max));

        var coolDown = SimpleStep.CreateWithKilometers(
            StepType.CoolDown,
            warmupCooldownDistance,
            IntensityTarget.Pace(parameters.Paces.EasyPace.Min, parameters.Paces.EasyPace.Max));

        return Workout.Create(
            WorkoutType.TempoRun,
            new List<Step>
            {
                warmUpStep,
                tempoStep,
                coolDown
            });
    }

    /// <summary>
    /// Calculates the distance covered based on the duration and specified pace range.
    /// </summary>
    /// <param name="duration">The time duration for which the distance is to be calculated.</param>
    /// <param name="paceRange">The pace range (minimum and maximum pace) used for the calculation.</param>
    /// <returns>The calculated distance based on the given duration and pace range.</returns>
    private static decimal CalculateDistanceBasedOnDuration(TimeSpan duration, (TimeSpan min, TimeSpan max) paceRange)
    {
        // Calculate average tempo pace in ticks per km, then convert to minutes
        decimal avgTempoTicksPerKm = (decimal) (paceRange.min.Ticks + paceRange.max.Ticks) / 2;
        decimal avgMinutesPerKm = avgTempoTicksPerKm / (decimal) TimeSpan.TicksPerMinute;

        decimal minutes = (decimal) duration.Ticks / TimeSpan.TicksPerMinute;

        // Calculate the distance covered during the tempo portion
        decimal distance = minutes / avgMinutesPerKm;

        return Math.Round(distance, 2, MidpointRounding.AwayFromZero);
    }
}
