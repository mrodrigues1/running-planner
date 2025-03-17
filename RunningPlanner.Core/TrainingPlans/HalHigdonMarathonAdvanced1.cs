using RunningPlanner.Core.Extensions;
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
            .WithThursday(
                WorkoutExtensions.CreateHillWorkout(
                    3,
                    8.0m,
                    EasyPaceRange(),
                    HillPaceRange(),
                    RecoveryPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateTempoWorkout(
                    30,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateIntervalWorkout(
                    4,
                    800,
                    8.0m,
                    EasyPaceRange(),
                    IntervalPaceRange(),
                    RecoveryPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateHillWorkout(
                    4,
                    8.0m,
                    EasyPaceRange(),
                    HillPaceRange(),
                    RecoveryPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateTempoWorkout(
                    35,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateIntervalWorkout(
                    5,
                    800,
                    8.0m,
                    EasyPaceRange(),
                    IntervalPaceRange(),
                    RecoveryPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateHillWorkout(
                    5,
                    8.0m,
                    EasyPaceRange(),
                    HillPaceRange(),
                    RecoveryPaceRange()))
            .WithFriday(CreateWorkout(WorkoutType.Rest, null))
            .WithSaturday(CreateWorkout(WorkoutType.RacePace, 12.9m))
            .WithSunday(
                CreateWorkout(
                    WorkoutType.LongRun,
                    25.6m)) // Note: PDF has 1.6 which appears to be a typo. Should likely be 25.6
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
            .WithThursday(
                WorkoutExtensions.CreateTempoWorkout(
                    40,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateIntervalWorkout(
                    6,
                    800,
                    8.0m,
                    EasyPaceRange(),
                    IntervalPaceRange(),
                    RecoveryPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateHillWorkout(
                    6,
                    8.0m,
                    EasyPaceRange(),
                    HillPaceRange(),
                    RecoveryPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateTempoWorkout(
                    45,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateIntervalWorkout(
                    7,
                    800,
                    8.0m,
                    EasyPaceRange(),
                    IntervalPaceRange(),
                    RecoveryPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateHillWorkout(
                    7,
                    8.0m,
                    EasyPaceRange(),
                    HillPaceRange(),
                    RecoveryPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateTempoWorkout(
                    45,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateIntervalWorkout(
                    8,
                    800,
                    8.0m,
                    EasyPaceRange(),
                    IntervalPaceRange(),
                    RecoveryPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateHillWorkout(
                    6,
                    8.0m,
                    EasyPaceRange(),
                    HillPaceRange(),
                    RecoveryPaceRange()))
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
            .WithThursday(
                WorkoutExtensions.CreateTempoWorkout(
                    30,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
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
            .WithTuesday(
                WorkoutExtensions.CreateIntervalWorkout(
                    4,
                    400,
                    6.0m,
                    EasyPaceRange(),
                    IntervalPaceRange(),
                    RecoveryPaceRange()))
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
