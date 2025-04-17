using RunningPlanner.Core.Models;
using RunningPlanner.Core.Models.Paces;

namespace RunningPlanner.Core.PlanGenerator.Marathon;

public class WorkoutGenerator
{
    private readonly TrainingPhase _phase;
    private readonly ExperienceLevel _experienceLevel;
    private readonly TrainingPaces _paces;
    private readonly int _weekNumber;
    private readonly int _phaseWeekNumber; // Current week within the phase
    private const decimal StrideDistanceMeters = 100m;

    public WorkoutGenerator(
        TrainingPhase phase,
        ExperienceLevel experienceLevel,
        TrainingPaces paces,
        int weekNumber,
        int phaseWeekNumber)
    {
        _phase = phase;
        _experienceLevel = experienceLevel;
        _paces = paces;
        _weekNumber = weekNumber;
        _phaseWeekNumber = phaseWeekNumber;
    }

    public Workout GenerateWorkout(WorkoutType type, decimal totalDistance)
    {
        return type switch
        {
            WorkoutType.EasyRun => GenerateEasyRun(totalDistance),
            WorkoutType.TempoRun => GenerateTempoRun(totalDistance),
            WorkoutType.Intervals => GenerateIntervals(totalDistance),
            WorkoutType.HillRepeat => GenerateHillRepeats(totalDistance),
            WorkoutType.LongRun => GenerateLongRun(totalDistance),
            WorkoutType.EasyRunWithStrides => GenerateStrides(totalDistance),
            WorkoutType.RacePace => GenerateRacePace(totalDistance),
            WorkoutType.Race => GenerateRace(totalDistance),
            WorkoutType.Rest => Workout.CreateRest(),
            _ => throw new ArgumentException($"Workout type {type} not supported")
        };
    }

    private Workout GenerateTempoRun(decimal totalDistance)
    {
        // Base tempo minutes based on experience level
        int baseTempoMinutes = _experienceLevel switch
        {
            ExperienceLevel.Beginner => 10,
            ExperienceLevel.Novice => 15,
            ExperienceLevel.Intermediate => 20,
            ExperienceLevel.Advanced => 25,
            ExperienceLevel.Elite => 30,
            _ => 15
        };

        // Progressive tempo minutes based on phase and week number
        int tempoMinutes = _phase switch
        {
            TrainingPhase.Base => baseTempoMinutes + (_phaseWeekNumber * 2 / 3), // Gentle progression
            TrainingPhase.Build => baseTempoMinutes + 5 + (_phaseWeekNumber), // Steeper progression
            TrainingPhase.Peak => baseTempoMinutes + 10 + (_phaseWeekNumber / 2), // Maintain high volume
            TrainingPhase.Taper => Math.Max(
                baseTempoMinutes / 2,
                baseTempoMinutes - (_phaseWeekNumber * 3)), // Reduction
            _ => baseTempoMinutes
        };

        // Cap tempo minutes based on experience level
        int maxTempoMinutes = _experienceLevel switch
        {
            ExperienceLevel.Beginner => 20,
            ExperienceLevel.Novice => 30,
            ExperienceLevel.Intermediate => 40,
            ExperienceLevel.Advanced => 50,
            ExperienceLevel.Elite => 50,
            _ => 30
        };

        tempoMinutes = Math.Min(tempoMinutes, maxTempoMinutes);

        return Workout.CreateTempo(
            tempoMinutes,
            totalDistance,
            _paces.EasyPace,
            _paces.ThresholdPace,
            CalculateWarmupDistance(totalDistance),
            CalculateCooldownDistance(totalDistance));
    }

    private Workout GenerateIntervals(decimal totalDistance)
    {
        // Base interval parameters based on experience level
        (int baseRepeats, int baseMeters) = _experienceLevel switch
        {
            ExperienceLevel.Beginner => (3, 400),
            ExperienceLevel.Novice => (4, 400),
            ExperienceLevel.Intermediate => (5, 400),
            ExperienceLevel.Advanced => (4, 800),
            ExperienceLevel.Elite => (5, 800),
            _ => (4, 400)
        };

        // Progressive interval repeats based on phase and week
        int repeats = _phase switch
        {
            TrainingPhase.Base => baseRepeats + (_phaseWeekNumber / 2),
            TrainingPhase.Build => baseRepeats + 2 + (_phaseWeekNumber / 2),
            TrainingPhase.Peak => baseRepeats + 3 + (_phaseWeekNumber / 3),
            TrainingPhase.Taper => Math.Max(baseRepeats - 2, baseRepeats - _phaseWeekNumber),
            _ => baseRepeats
        };

        // Progressive interval distance based on phase
        int meters = _phase switch
        {
            TrainingPhase.Base => baseMeters,
            TrainingPhase.Build => IsEvenWeek() ? baseMeters : baseMeters * 2, // Alternate between shorter/longer
            TrainingPhase.Peak => baseMeters * 2, // Longer intervals at peak
            TrainingPhase.Taper => baseMeters, // Back to shorter intervals during taper
            _ => baseMeters
        };

        // Cap repeats based on experience level
        int maxRepeats = _experienceLevel switch
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
        int recoveryMeters = CalculateRecoveryMeters(meters);

        var currentTotalDistance = repeats * (meters + recoveryMeters) / 1000m;
        var remainingDistance = totalDistance - currentTotalDistance;
        var warmupDistance = 0m;
        var cooldownDistance = 0m;

        if (remainingDistance >= 2m)
        {
            warmupDistance = remainingDistance / 2;
            cooldownDistance = remainingDistance / 2;
        }
        else
        {
            for (int i = repeats - 1; i > 0; i--)
            {
                var currentTotalDistance2 = i * (meters + recoveryMeters) / 1000m;
                var remainingDistance2 = totalDistance - currentTotalDistance2;

                if (remainingDistance2 < 2m) continue;

                warmupDistance = remainingDistance2 / 2;
                cooldownDistance = remainingDistance2 / 2;

                break;
            }
        }

        return Workout.CreateInterval(
            repeats,
            meters,
            recoveryMeters,
            totalDistance,
            _paces.EasyPace,
            _paces.IntervalPace,
            _paces.EasyPace,
            warmupDistance,
            cooldownDistance);
    }

    private Workout GenerateHillRepeats(decimal totalDistance)
    {
        // Base hill parameters based on experience level
        int baseRepeats = _experienceLevel switch
        {
            ExperienceLevel.Beginner => 4,
            ExperienceLevel.Novice => 5,
            ExperienceLevel.Intermediate => 6,
            ExperienceLevel.Advanced => 8,
            ExperienceLevel.Elite => 10,
            _ => 6
        };

        // Progressive hill repeats based on phase and week
        int repeats = _phase switch
        {
            TrainingPhase.Base => baseRepeats + (_phaseWeekNumber / 2),
            TrainingPhase.Build => baseRepeats + 2 + (_phaseWeekNumber / 2),
            TrainingPhase.Peak => baseRepeats + 4,
            TrainingPhase.Taper => Math.Max(baseRepeats - 2, baseRepeats - _phaseWeekNumber),
            _ => baseRepeats
        };

        // Cap repeats based on experience level
        int maxRepeats = _experienceLevel switch
        {
            ExperienceLevel.Beginner => 8,
            ExperienceLevel.Novice => 10,
            ExperienceLevel.Intermediate => 12,
            ExperienceLevel.Advanced => 15,
            ExperienceLevel.Elite => 20,
            _ => 10
        };

        repeats = Math.Min(repeats, maxRepeats);

        return Workout.CreateHillRepeat(
            repeats,
            totalDistance,
            _paces.EasyPace,
            _paces.IntervalPace, // Use interval pace for hills
            _paces.EasyPace,
            CalculateWarmupDistance(totalDistance),
            CalculateCooldownDistance(totalDistance));
    }

    private Workout GenerateLongRun(decimal totalDistance)
    {
        // Long runs should be at easy pace, focusing on distance
        return Workout.CreateLongRun(totalDistance, _paces.EasyPace);
    }

    private Workout GenerateEasyRun(decimal totalDistance)
    {
        return Workout.CreateEasyRun(totalDistance, _paces.EasyPace);
    }

    private Workout GenerateRacePace(decimal totalDistance)
    {
        // Base race pace segments based on experience and phase
        decimal racePaceDistance = _phase switch
        {
            TrainingPhase.Base => totalDistance * 0.2m,
            TrainingPhase.Build => totalDistance * (0.3m + (_phaseWeekNumber * 0.03m)),
            TrainingPhase.Peak => totalDistance * (0.5m + (_phaseWeekNumber * 0.05m)),
            TrainingPhase.Taper => totalDistance * Math.Max(0.2m, 0.4m - (_phaseWeekNumber * 0.1m)),
            _ => totalDistance * 0.25m
        };

        // Cap based on experience level
        decimal maxRacePacePercent = _experienceLevel switch
        {
            ExperienceLevel.Beginner => 0.3m,
            ExperienceLevel.Novice => 0.4m,
            ExperienceLevel.Intermediate => 0.6m,
            ExperienceLevel.Advanced => 0.7m,
            ExperienceLevel.Elite => 0.8m,
            _ => 0.5m
        };

        racePaceDistance = Math.Min(racePaceDistance, totalDistance * maxRacePacePercent);

        // Create segments: warm up, race pace, cool down
        var warmupDistance = (totalDistance - racePaceDistance) * 0.5m;
        var cooldownDistance = totalDistance - racePaceDistance - warmupDistance;

        // Build the workout with segments
        var warmupStep = SimpleStep.CreateWithKilometers(
            StepType.WarmUp,
            warmupDistance,
            _paces.EasyPace);

        var racePaceStep = SimpleStep.CreateWithKilometers(
            StepType.Run,
            racePaceDistance,
            _paces.MarathonPace);

        var cooldownStep = SimpleStep.CreateWithKilometers(
            StepType.CoolDown,
            cooldownDistance,
            _paces.EasyPace);

        return Workout.Create(
            WorkoutType.RacePace,
            new List<Step> {warmupStep, racePaceStep, cooldownStep});
    }

    private Workout GenerateStrides(decimal totalDistance)
    {
        // Base strides based on experience level
        int baseStrides = _experienceLevel switch
        {
            ExperienceLevel.Beginner => 4,
            ExperienceLevel.Novice => 6,
            ExperienceLevel.Intermediate => 8,
            ExperienceLevel.Advanced => 10,
            ExperienceLevel.Elite => 12,
            _ => 6
        };

        // Progressive strides based on phase and week
        int strides = _phase switch
        {
            TrainingPhase.Base => baseStrides + (_phaseWeekNumber / 2),
            TrainingPhase.Build => baseStrides + (_phaseWeekNumber / 2),
            TrainingPhase.Peak => baseStrides,
            TrainingPhase.Taper => Math.Max(baseStrides - 2, baseStrides - _phaseWeekNumber),
            _ => baseStrides
        };

        // Cap strides based on experience level
        int maxStrides = _experienceLevel switch
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
        
        var currentTotalDistance = totalDistance - strideTotalDistance;

        if (currentTotalDistance < 0m)
        {
            for (int i = strides - 1; i > 0; i--)
            {
                strideTotalDistance = i * strideDistancePlusRecovery / 1000m;
                currentTotalDistance = totalDistance - strideTotalDistance;

                if (currentTotalDistance > 0m)
                {
                    strides = i;
                    break;
                }
            }
        }

        return Workout.CreateStrideWorkout(
            totalDistance,
            strides,
            StrideDistanceMeters,
            _paces.EasyPace,
            _paces.RepetitionPace,
            _paces.EasyPace);
    }

    private Workout GenerateRace(decimal totalDistance)
    {
        return Workout.CreateRace(totalDistance);
    }

    // Helper methods
    private decimal CalculateWarmupDistance(decimal totalDistance) => Math.Round(
        totalDistance * 0.2m,
        2,
        MidpointRounding.AwayFromZero);

    private decimal CalculateCooldownDistance(decimal totalDistance) => Math.Round(
        totalDistance * 0.2m,
        2,
        MidpointRounding.AwayFromZero);

    private int CalculateRecoveryMeters(int intervalMeters)
    {
        // In Base/Taper: longer recovery, in Build/Peak: shorter recovery
        decimal recoveryRatio = _phase switch
        {
            TrainingPhase.Base => 1.0m,
            TrainingPhase.Build => 0.75m,
            TrainingPhase.Peak => 0.5m,
            TrainingPhase.Taper => 1.0m,
            _ => 0.75m
        };

        return (int) (intervalMeters * recoveryRatio);
    }

    private bool IsEvenWeek() => _weekNumber % 2 == 0;
}
