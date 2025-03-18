using RunningPlanner.Core.Extensions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.TrainingPlans.HalHigdon;

public class HalHigdonMarathonIntermediate1
{
    public TrainingPlan TrainingPlan { get; private set; }

    public HalHigdonMarathonIntermediate1()
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
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(8m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek2()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(2)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(14.5m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek3()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(3)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(8m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(9m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek4()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(4)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(10m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateMediumRunWorkout(10m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateEasyRunWorkout(18m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek5()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(5)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(10m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateEasyRunWorkout(10m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(19.5m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek6()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(6)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(10m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(14.5m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek7()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(7)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(6.5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(11.5m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(6.5m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(11.5m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(22.5m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek8()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(8)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(6.5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(11.5m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(6.5m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateMediumRunWorkout(11.5m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(24m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek9()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(9)
            .WithTrainingPhase(TrainingPhase.TuneUpRace)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(6.5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(6.5m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRestWorkout())
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(21.1m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek10()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(10)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(6.5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(13m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(6.5m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(13m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(27.5m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek11()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(11)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(13m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateMediumRunWorkout(13m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(29m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek12()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(12)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(13m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(21m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek13()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(13)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(13m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(8m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(32m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek14()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(14)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateMediumRunWorkout(13m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(19.5m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek15()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(15)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(13m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(8m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(32m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek16()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(16)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(10m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(6.5m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(19.5m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek17()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(17)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(6.5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(8m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(6.5m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(13m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek18()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(18)
            .WithTrainingPhase(TrainingPhase.Race)
            .WithMonday(CrossTrainingDay())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(6.5m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateRestWorkout())
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateEasyRunWorkout(3.5m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateRaceWorkout(42.2m, RacePaceRange()))
            .Build();
    }

    private static Workout CrossTrainingDay()
    {
        // This is a placeholder for a cross-training day
        // In a real implementation, you might want to create a specific cross-training workout type
        return WorkoutExtensions.CreateRestWorkout();
    }

    // Pace calculation methods
    private static (TimeSpan min, TimeSpan max) EasyPaceRange()
    {
        // Define easy pace range for Intermediate 1 runners (per kilometer)
        return (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(50)),
            TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)));
    }

    private static (TimeSpan min, TimeSpan max) RacePaceRange()
    {
        // Define race pace range for Intermediate 1 runners (per kilometer)
        return (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(45)),
            TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(35)));
    }
}
