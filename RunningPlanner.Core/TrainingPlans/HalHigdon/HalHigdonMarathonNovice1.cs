using RunningPlanner.Core.Extensions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.TrainingPlans.HalHigdon;

public class HalHigdonMarathonNovice1
{
    public TrainingPlan TrainingPlan { get; private set; }

    public HalHigdonMarathonNovice1()
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
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(9.7m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek2()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(2)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(11.3m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek3()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(3)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(8.1m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek4()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(4)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(14.5m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek5()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(5)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(16.1m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek6()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(6)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(11.3m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek7()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(7)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(9.7m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(19.3m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek8()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(8)
            .WithTrainingPhase(TrainingPhase.TuneUpRace)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(9.7m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRestWorkout())
            .WithSunday(WorkoutExtensions.CreateRaceWorkout(21.1m, RacePaceRange())) // Half Marathon
            .Build();
    }

    private TrainingWeek GenerateWeek9()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(9)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(11.3m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(16.1m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek10()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(10)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(11.3m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(24.1m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek11()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(11)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(12.9m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(25.7m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek12()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(12)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(12.9m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(19.3m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek13()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(13)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(14.5m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(29.0m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek14()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(14)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(14.5m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(22.5m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek15()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(15)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(16.1m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(32.2m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek16()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(16)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateMediumRunWorkout(12.9m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(19.3m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek17()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(17)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(9.7m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateLongRunWorkout(12.9m, EasyPaceRange()))
            .WithSunday(CrossTrainingDay())
            .Build();
    }

    private TrainingWeek GenerateWeek18()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(18)
            .WithTrainingPhase(TrainingPhase.Race)
            .WithMonday(WorkoutExtensions.CreateRestWorkout())
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateEasyRunWorkout(3.2m, EasyPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRestWorkout())
            .WithSunday(WorkoutExtensions.CreateRaceWorkout(42.2m, RacePaceRange())) // Marathon
            .Build();
    }

    private static Workout CrossTrainingDay()
    {
        // This is a placeholder for a cross-training day
        // In a real implementation, you might want to create a specific cross-training workout type
        return WorkoutExtensions.CreateRestWorkout();
    }

    // Pace calculation methods for Novice 1 level runners
    private static (TimeSpan min, TimeSpan max) EasyPaceRange()
    {
        // Define easy pace range for Novice 1 runners (per kilometer)
        // Novice 1 runners typically run at a more comfortable pace
        return (TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(0)),
            TimeSpan.FromMinutes(8).Add(TimeSpan.FromSeconds(0)));
    }

    private static (TimeSpan min, TimeSpan max) RacePaceRange()
    {
        // Define race pace range for Novice 1 runners (per kilometer)
        // Slightly faster than easy pace, but still conservative for beginners
        return (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)),
            TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(15)));
    }
}
