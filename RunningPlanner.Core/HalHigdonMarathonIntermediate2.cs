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
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 8.1m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 8.1m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 16.1m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
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
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 17.7m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
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
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 9.7m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 12.9m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
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
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 9.7m))
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
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 11.3m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.EasyRun, 11.3m))
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
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 11.3m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 11.3m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 16.1m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek7()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(7)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 12.9m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 12.9m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 16.1m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek8()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(8)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 12.9m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 12.9m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 27.4m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek9()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(9)
            .WithTrainingPhase(TrainingPhase.TuneUpRace)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 14.5m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
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
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 14.5m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 14.5m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 30.6m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek11()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(11)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 16.1m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.MediumRun, 16.1m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 32.2m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek12()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(12)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 9.7m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 9.7m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 19.3m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek13()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(13)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 16.1m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 16.1m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 32.2m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
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
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.MediumRun, 9.7m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 19.3m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
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
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 16.1m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 32.2m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
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
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.RacePace, 6.4m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 19.3m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
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
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.MediumRun, 6.4m))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 12.9m))
            .WithSunday(CreateWorkout(WorkoutType.Rest, null))
            .Build();
    }

    private TrainingWeek GenerateWeek18()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(18)
            .WithTrainingPhase(TrainingPhase.Race)
            .WithMonday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithTuesday(CreateWorkout(WorkoutType.MediumRun, 6.4m))
            .WithWednesday(CreateWorkout(WorkoutType.Rest, null))
            .WithThursday(CreateWorkout(WorkoutType.Rest, null))
            .WithFriday(CreateWorkout(WorkoutType.EasyRun, 3.2m))
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

        SimpleStep? simpleStep;

        Step? step;

        if (workoutType is WorkoutType.Race)
        {
            simpleStep = SimpleStep.SimpleStepBuilder
                .CreateBuilder()
                .WithType(StepType.Run)
                .WithKilometers(distance.Value)
                .WithNoTargetPaceRange()
                .Build();

            step = Step.StepBuilder
                .CreateBuilder()
                .WithSimpleStep(simpleStep)
                .Build();

            return Workout.WorkoutBuilder
                .CreateBuilder()
                .WithType(workoutType)
                .WithStep(step)
                .BuildRaceWorkout();
        }

        var pace = workoutType is WorkoutType.RacePace ? RacePaceRange() : EasyPaceRange();

        simpleStep = SimpleStep.SimpleStepBuilder
            .CreateBuilder()
            .WithType(StepType.Run)
            .WithKilometers(distance.Value)
            .WithPaceRange(pace)
            .Build();

        step = Step.StepBuilder
            .CreateBuilder()
            .WithSimpleStep(simpleStep)
            .Build();

        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(workoutType)
            .WithStep(step)
            .BuildSimpleWorkout();
    }

    private static (TimeSpan min, TimeSpan max) EasyPaceRange()
    {
        return (new TimeSpan(0, 6, 10), new TimeSpan(0, 6, 35));
    }

    private static (TimeSpan min, TimeSpan max) RacePaceRange()
    {
        return (new TimeSpan(0, 5, 37), new TimeSpan(0, 5, 45));
    }
}