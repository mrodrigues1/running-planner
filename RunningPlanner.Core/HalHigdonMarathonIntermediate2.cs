using RunningPlanner.Core.Models;

namespace RunningPlanner.Core;

public class HalHigdonMarathonIntermediate2
{
    public TrainingPlan TrainingPlan { get; private set; }

    public HalHigdonMarathonIntermediate2()
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
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 8m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 8m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 16m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek2()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(2)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 8m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 18m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek3()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(3)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 10m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 10m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 13m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek4()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(4)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 10m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 10m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 21m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek5()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(5)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 11.5m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.EasyRun, 11.5m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 22.5m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek6()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(6)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 11.5m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 11.5m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 16m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek7()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(7)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 13m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.5m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 13m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 16m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek8()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(8)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 13m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.5m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 13m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 27.5m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek9()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(9)
            .WithTrainingPhase(TrainingPhase.TuneUpRace)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 14.5m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.5m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.Rest, null))
            .WithSunday(CreateWorkout(WorkoutType.Race, 21.1m))
            .Build();
    }

    private TrainingWeek GenerateWeek10()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(10)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 14.5m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.5m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 14.5m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 30.5m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek11()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(11)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 16m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.MediumRun, 16m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 32m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek12()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(12)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 10m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 10m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 19.5m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek13()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(13)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 16m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 16m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 32m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek14()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(14)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 10m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.MediumRun, 10m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 19.5m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek15()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(15)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 16m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 16m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 32m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek16()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(16)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 13m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 6.5m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 19.5m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek17()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(17)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 10m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.5m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.MediumRun, 6.5m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 13m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek18()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(18)
            .WithTrainingPhase(TrainingPhase.Race)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 5m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 6.5m))
            .WithWednesday(CreateWorkout(WorkoutType.Rest, null))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.EasyRun, 3m))
            .WithSaturday(CreateWorkout(WorkoutType.Rest, null))
            .WithSunday(CreateWorkout(WorkoutType.Race, 42.2m))
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
    
    private static Workout CreateComplexWorkout(
        WorkoutType workoutType, 
        decimal distance,
        int repeats,
        decimal repeatDistance, 
        decimal restDistance)
    {
        var fullIntervalDistance = repeats * (repeatDistance + restDistance);
        
        var easyRunDistance = distance - fullIntervalDistance;

        var easyRunStepDistance = Math.Round(easyRunDistance / 2, 1, MidpointRounding.AwayFromZero);
        
        
        (TimeSpan min, TimeSpan max) pace;

        switch (workoutType)
        {
            case WorkoutType.TempoRun:
                pace = TempoPaceRange();

                break;
            case WorkoutType.Intervals:
            case WorkoutType.Repetition:
                pace = IntervalPaceRange();

                break;
            case WorkoutType.Threshold:
                pace = TempoPaceRange();

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(workoutType), workoutType, null);
        }
        
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(workoutType)
            .WithSimpleRunStep(easyRunStepDistance, EasyPaceRange())
            .WithSimpleStep(StepType.Rest, 0.3m, RestPaceRange())
            .WithRepeatStep(repeats, repeatDistance, restDistance, pace, RestPaceRange())
            .WithSimpleRunStep(easyRunStepDistance, EasyPaceRange())
            .BuildRepeatWorkout();
    }

    private static (TimeSpan min, TimeSpan max) EasyPaceRange()
    {
        return (new TimeSpan(0, 6, 10), new TimeSpan(0, 6, 35));
    }

    private static (TimeSpan min, TimeSpan max) RacePaceRange()
    {
        return (new TimeSpan(0, 5, 37), new TimeSpan(0, 5, 45));
    }
    
    private static (TimeSpan min, TimeSpan max) TempoPaceRange()
    {
        // Define tempo pace range for Advanced 2 runners (per kilometer)
        return (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30)), 
            TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(50)));
    }

    private static (TimeSpan min, TimeSpan max) IntervalPaceRange()
    {
        // Define interval pace range for Advanced 2 runners (per kilometer)
        return (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(0)), 
            TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(20)));
    }
    
    private static (TimeSpan min, TimeSpan max) RestPaceRange()
    {
        // Define interval pace range for Advanced 2 runners (per kilometer)
        return (TimeSpan.FromMinutes(7), TimeSpan.FromMinutes(10));
    }
}