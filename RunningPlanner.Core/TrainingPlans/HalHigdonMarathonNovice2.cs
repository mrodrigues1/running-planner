using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.TrainingPlans;

public class HalHigdonMarathonNovice2
{
    public TrainingPlan TrainingPlan { get; private set; }

    public HalHigdonMarathonNovice2()
    {
        TrainingPlan = GenerateDefaultTrainingPlan();
    }

    private TrainingPlan GenerateDefaultTrainingPlan()
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
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithWednesday(CreateWorkout(WorkoutType.RacePace, 8.1m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 12.9m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek2()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(2)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 14.5m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek3()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(3)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithWednesday(CreateWorkout(WorkoutType.RacePace, 8.1m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 9.7m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek4()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(4)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithWednesday(CreateWorkout(WorkoutType.RacePace, 9.7m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 17.7m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek5()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(5)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 9.7m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 19.3m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek6()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(6)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithWednesday(CreateWorkout(WorkoutType.RacePace, 9.7m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 14.5m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek7()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(7)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithWednesday(CreateWorkout(WorkoutType.RacePace, 11.3m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 22.5m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek8()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(8)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 11.3m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 24.1m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek9()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(9)
            .WithTrainingPhase(TrainingPhase.TuneUpRace)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithWednesday(CreateWorkout(WorkoutType.RacePace, 11.3m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.Rest, null))
            .WithSunday(CreateWorkout(WorkoutType.Race, 21.1m)) // Half marathon
            .Build();
    }

    private TrainingWeek GenerateWeek10()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(10)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithWednesday(CreateWorkout(WorkoutType.RacePace, 12.9m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 27.4m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek11()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(11)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 12.9m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 29.0m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek12()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(12)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithWednesday(CreateWorkout(WorkoutType.RacePace, 12.9m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 21.0m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek13()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(13)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithWednesday(CreateWorkout(WorkoutType.RacePace, 8.1m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 30.6m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek14()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(14)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 12.9m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 19.3m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek15()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(15)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithWednesday(CreateWorkout(WorkoutType.RacePace, 8.1m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 32.2m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek16()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(16)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithWednesday(CreateWorkout(WorkoutType.RacePace, 6.4m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 8.1m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 19.3m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek17()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(17)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithWednesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
            .WithThursday(CreateWorkout(WorkoutType.EasyRun, 6.4m))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.LongRun, 12.9m))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek18()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(18)
            .WithTrainingPhase(TrainingPhase.Race)
            .WithMonday(CreateWorkout(WorkoutType.Rest, null))
            .WithTuesday(CreateWorkout(WorkoutType.EasyRun, 4.8m))
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

    private static Workout CrossTrainingDay()
    {
        // This is a placeholder for a cross-training day
        // In a real implementation, you might want to create a specific cross-training workout type
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.Rest)
            .BuildRestWorkout();
    }

    // Pace calculation methods for Novice 2 level runners
    private static (TimeSpan min, TimeSpan max) EasyPaceRange()
    {
        // Define easy pace range for Novice 2 runners (per kilometer)
        return (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)),
            TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(15)));
    }

    private static (TimeSpan min, TimeSpan max) RacePaceRange()
    {
        // Define race pace range for Novice 2 runners (per kilometer)
        return (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)),
            TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(15)));
    }
}
