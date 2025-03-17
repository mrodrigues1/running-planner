using RunningPlanner.Core.Models;

namespace RunningPlanner.Core;

public class HalHigdonMarathonAdvanced1
{
    public TrainingPlan TrainingPlan { get; private set; }

    public HalHigdonMarathonAdvanced1()
    {
        TrainingPlan = GenerateDefaultTrainingPlan();
    }

    public TrainingPlan GenerateDefaultTrainingPlan()
    {
        return TrainingPlan.TrainingPlanBuilder
            .CreateBuilder()
            .WithTrainingWeek(GenerateWeek1())
            .WithTrainingWeek(GenerateWeek2())
            .WithTrainingWeek(GenerateWeek3())
            .WithTrainingWeek(GenerateWeek4())
            .WithTrainingWeek(GenerateWeek5())
            .WithTrainingWeek(GenerateWeek6())
            .WithTrainingWeek(GenerateWeek7())
            .WithTrainingWeek(GenerateWeek8())
            .WithTrainingWeek(GenerateWeek9())
            .WithTrainingWeek(GenerateWeek10())
            .WithTrainingWeek(GenerateWeek11())
            .WithTrainingWeek(GenerateWeek12())
            .WithTrainingWeek(GenerateWeek13())
            .WithTrainingWeek(GenerateWeek14())
            .WithTrainingWeek(GenerateWeek15())
            .WithTrainingWeek(GenerateWeek16())
            .WithTrainingWeek(GenerateWeek17())
            .WithTrainingWeek(GenerateWeek18())
            .Build();
    }

    private TrainingWeek GenerateWeek1()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(1)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 8.1m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithThursday(CreateHillWorkout(3, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.RacePace, 8.1m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 16.1m))
            .Build();
    }

    private TrainingWeek GenerateWeek2()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(2)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 8.1m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithThursday(CreateTempoWorkout(30, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 17.7m))
            .Build();
    }

    private TrainingWeek GenerateWeek3()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(3)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 9.7m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithThursday(CreateIntervalWorkout(4, 800, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.RacePace, 9.7m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 12.9m))
            .Build();
    }

    private TrainingWeek GenerateWeek4()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(4)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 9.7m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithThursday(CreateHillWorkout(4, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.RacePace, 9.7m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 21.0m))
            .Build();
    }

    private TrainingWeek GenerateWeek5()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(5)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 11.3m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithThursday(CreateTempoWorkout(35, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.EasyRun, 11.3m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 22.5m))
            .Build();
    }

    private TrainingWeek GenerateWeek6()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(6)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 11.3m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithThursday(CreateIntervalWorkout(5, 800, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.RacePace, 11.3m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 16.1m))
            .Build();
    }

    private TrainingWeek GenerateWeek7()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(7)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 12.9m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithThursday(CreateHillWorkout(5, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.RacePace, 12.9m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 25.6m)) // Note: PDF has 1.6 which appears to be a typo. Should likely be 25.6
            .Build();
    }

    private TrainingWeek GenerateWeek8()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(8)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 12.9m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithThursday(CreateTempoWorkout(40, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.EasyRun, 12.9m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 27.4m))
            .Build();
    }

    private TrainingWeek GenerateWeek9()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(9)
            .WithTrainingPhase(TrainingPhase.TuneUpRace)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 14.5m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithThursday(CreateIntervalWorkout(6, 800, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.Rest, null))
            .WithSunday(CreateWorkout(WorkoutType.Race, 21.1m)) // Half Marathon
            .Build();
    }

    private TrainingWeek GenerateWeek10()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(10)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 14.5m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithThursday(CreateHillWorkout(6, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.RacePace, 14.5m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 30.6m))
            .Build();
    }

    private TrainingWeek GenerateWeek11()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(11)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 16.1m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithThursday(CreateTempoWorkout(45, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.MediumRun, 16.1m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 32.2m))
            .Build();
    }

    private TrainingWeek GenerateWeek12()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(12)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 9.7m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithThursday(CreateIntervalWorkout(7, 800, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.RacePace, 9.7m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 19.3m))
            .Build();
    }

    private TrainingWeek GenerateWeek13()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(13)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 16.1m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithThursday(CreateHillWorkout(7, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.RacePace, 16.1m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 32.2m))
            .Build();
    }

    private TrainingWeek GenerateWeek14()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(14)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 9.7m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithThursday(CreateTempoWorkout(45, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.MediumRun, 9.7m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 19.3m))
            .Build();
    }

    private TrainingWeek GenerateWeek15()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(15)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 16.1m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithThursday(CreateIntervalWorkout(8, 800, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.RacePace, 16.1m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 32.2m))
            .Build();
    }

    private TrainingWeek GenerateWeek16()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(16)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 12.9m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithThursday(CreateHillWorkout(6, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.RacePace, 6.4m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 19.3m))
            .Build();
    }

    private TrainingWeek GenerateWeek17()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(17)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 9.7m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithThursday(CreateTempoWorkout(30, 8.0m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithSunday(CreateWorkout(WorkoutType.LongRun, 12.9m))
            .Build();
    }

    private TrainingWeek GenerateWeek18()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(18)
            .WithTrainingPhase(TrainingPhase.Race)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateIntervalWorkout(4, 400, 6.0m)) // 4 x 400
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 3.2m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.EasyRun, 3.2m))
            .WithSunday(CreateWorkout(WorkoutType.Race, 42.2m)) // Marathon
            .Build();
    }

    private static Workout CreateWorkout(WorkoutType workoutType, decimal? distance)
    {
        if (workoutType is WorkoutType.Rest)
        {
            return Workout.WorkoutBuilder
                .CreateBuilder()
                .WithType(workoutType)
                .BuildRestWorkout();
        }

        if (!distance.HasValue)
        {
            throw new ArgumentNullException(nameof(distance));
        }
        
        if (workoutType is WorkoutType.Race)
        {
            return Workout.WorkoutBuilder
                .CreateBuilder()
                .WithType(workoutType)
                .WithRaceStep(distance.Value)
                .BuildRaceWorkout();
        }

        var pace = workoutType is WorkoutType.RacePace ? RacePaceRange() : EasyPaceRange();
        
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(workoutType)
            .WithSimpleRunStep(distance.Value, pace)
            .BuildSimpleWorkout();
    }

    /// <summary>
    /// Creates a hill repeat workout based on the format in the Hal Higdon Advanced 1 plan
    /// </summary>
    /// <param name="repeats">Number of hill repeats</param>
    /// <param name="totalDistance">Total workout distance in kilometers</param>
    /// <param name="warmupDistance">Distance for the warmup in kilometers (default 2.0)</param>
    /// <param name="cooldownDistance">Distance for the cooldown in kilometers (default 2.0)</param>
    private static Workout CreateHillWorkout(int repeats, decimal totalDistance, decimal warmupDistance = 2.0m, decimal cooldownDistance = 2.0m)
    {
        // For hill workouts:
        // - Warmup as specified (default 2km)
        // - Repeats
        // - Cooldown as specified (default 2km)
        decimal remainingDistance = totalDistance - (warmupDistance + cooldownDistance);
        
        // Each hill repeat is approximately:
        decimal hillUpDistance = 0.4m;  // ~400m uphill
        decimal hillDownDistance = 0.4m; // ~400m recovery (jog down)
        decimal totalRepeatDistance = repeats * (hillUpDistance + hillDownDistance);
        
        // If there's additional distance to cover, we'll add it as an easy run
        decimal additionalEasyDistance = Math.Max(0, remainingDistance - totalRepeatDistance);
        
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.Intervals) // Using Intervals type for hills
            .WithSimpleStep(StepType.WarmUp, warmupDistance, EasyPaceRange())
            .WithRepeatStep(repeats, hillUpDistance, hillDownDistance, HillPaceRange(), RecoveryPaceRange())
            .WithSimpleStep(StepType.CoolDown, cooldownDistance + additionalEasyDistance, EasyPaceRange())
            .Build();
    }

    /// <summary>
    /// Creates a tempo workout based on the format in the Hal Higdon Advanced 1 plan
    /// </summary>
    /// <param name="tempoMinutes">Duration of the tempo portion in minutes</param>
    /// <param name="totalDistance">Total workout distance in kilometers</param>
    /// <param name="warmupDistance">Distance for the warmup in kilometers (default 2.0)</param>
    /// <param name="cooldownDistance">Distance for the cooldown in kilometers (default 2.0)</param>
    private static Workout CreateTempoWorkout(int tempoMinutes, decimal totalDistance, decimal warmupDistance = 2.0m, decimal cooldownDistance = 2.0m)
    {
        // For tempo workouts:
        // - Warmup as specified (default 2km)
        // - Tempo section at faster pace (using the minutes specified)
        // - Cooldown as specified (default 2km)
        
        // Tempo distance is estimated based on the average tempo pace from TempoPaceRange() and minutes
        var tempoPaceRange = TempoPaceRange();
        
        // Calculate average tempo pace in ticks per km, then convert to minutes
        decimal avgTempoTicksPerKm = (decimal)(tempoPaceRange.min.Ticks + tempoPaceRange.max.Ticks) / 2;
        decimal avgTempoMinutesPerKm = avgTempoTicksPerKm / (decimal)TimeSpan.TicksPerMinute;
        
        // Calculate the distance covered during the tempo portion
        decimal tempoDistance = tempoMinutes / avgTempoMinutesPerKm;
        
        // Additional easy distance if needed to match total
        decimal remainingDistance = totalDistance - (warmupDistance + tempoDistance + cooldownDistance);
        decimal additionalEasyDistance = Math.Max(0, remainingDistance);
        
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.TempoRun)
            .WithSimpleStep(StepType.WarmUp, warmupDistance, EasyPaceRange())
            .WithSimpleStep(StepType.Run, tempoDistance, TempoPaceRange())
            .WithSimpleStep(StepType.CoolDown, cooldownDistance + additionalEasyDistance, EasyPaceRange())
            .Build();
    }

    /// <summary>
    /// Creates an interval workout based on the format in the Hal Higdon Advanced 1 plan
    /// </summary>
    /// <param name="repeats">Number of intervals</param>
    /// <param name="meters">Length of each interval in meters</param>
    /// <param name="totalDistance">Total workout distance in kilometers</param>
    /// <param name="warmupDistance">Distance for the warmup in kilometers (default 2.0)</param>
    /// <param name="cooldownDistance">Distance for the cooldown in kilometers (default 2.0)</param>
    private static Workout CreateIntervalWorkout(int repeats, int meters, decimal totalDistance, decimal warmupDistance = 2.0m, decimal cooldownDistance = 2.0m)
    {
        // For interval workouts:
        // - Warmup as specified (default 2km)
        // - Interval repeats (with recovery)
        // - Cooldown as specified (default 2km)
        
        // Convert meters to kilometers for intervals
        decimal intervalDistance = meters / 1000.0m;
        
        // Recovery distance is often equal to interval distance for many training plans
        decimal recoveryDistance = intervalDistance;
        
        // Total distance for intervals and recovery
        decimal intervalTotalDistance = repeats * (intervalDistance + recoveryDistance);
        
        // Additional easy distance if needed to match total
        decimal remainingDistance = totalDistance - (warmupDistance + intervalTotalDistance + cooldownDistance);
        decimal additionalEasyDistance = Math.Max(0, remainingDistance);
        
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.Intervals)
            .WithSimpleStep(StepType.WarmUp, warmupDistance, EasyPaceRange())
            .WithRepeatStep(repeats, intervalDistance, recoveryDistance, IntervalPaceRange(), RecoveryPaceRange())
            .WithSimpleStep(StepType.CoolDown, cooldownDistance + additionalEasyDistance, EasyPaceRange())
            .Build();
    }

    // Pace calculation methods adjusted for Advanced 1 level runners
    private static (TimeSpan min, TimeSpan max) EasyPaceRange()
    {
        // Define easy pace range for Advanced 1 runners (per kilometer)
        return (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)), 
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(0)));
    }

    private static (TimeSpan min, TimeSpan max) RacePaceRange()
    {
        // Define race pace range for Advanced 1 runners (per kilometer)
        return (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30)), 
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(45)));
    }
    
    private static (TimeSpan min, TimeSpan max) TempoPaceRange()
    {
        // Define tempo pace range for Advanced 1 runners (per kilometer)
        return (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(15)), 
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(35)));
    }

    private static (TimeSpan min, TimeSpan max) IntervalPaceRange()
    {
        // Define interval pace range for Advanced 1 runners (per kilometer)
        return (TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(45)), 
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(5)));
    }
    
    private static (TimeSpan min, TimeSpan max) HillPaceRange()
    {
        // Define hill repeat pace range for Advanced 1 runners (per kilometer)
        // Hills are typically done at hard effort, similar to intervals
        return (TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(50)), 
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(10)));
    }
    
    private static (TimeSpan min, TimeSpan max) RecoveryPaceRange()
    {
        // Define recovery pace range for Advanced 1 runners (per kilometer)
        return (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)), 
                TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(30)));
    }
}