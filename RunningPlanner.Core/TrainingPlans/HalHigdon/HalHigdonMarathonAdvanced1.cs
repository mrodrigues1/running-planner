using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.TrainingPlans.HalHigdon;

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
        return TrainingWeek.Create(
            weekNumber: 1,
            trainingPhase: TrainingPhase.Base,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            wednesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            thursday: Workout.CreateHillRepeatWorkout(
                3,
                8.0m,
                EasyPaceRange(),
                HillPaceRange(),
                RecoveryPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            sunday: Workout.CreateLongRun(16.1m, EasyPaceRange()));
    }

    private TrainingWeek GenerateWeek2()
    {
        return TrainingWeek.Create(
            weekNumber: 2,
            trainingPhase: TrainingPhase.Base,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            wednesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                30,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            sunday: Workout.CreateLongRun(17.7m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek3()
    {
        return TrainingWeek.Create(
            weekNumber: 3,
            trainingPhase: TrainingPhase.Base,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateEasyRun(9.7m, EasyPaceRange()),
            wednesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            thursday: Workout.CreateInterval(
                4,
                800,
                400,
                8.0m,
                EasyPaceRange(),
                IntervalPaceRange(),
                RecoveryPaceRange(),
                restBeforeStartIntervalDistance: 0.5m),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRacePace(9.7m, RacePaceRange()),
            sunday: Workout.CreateLongRun(12.9m, EasyPaceRange()));
    }

    private TrainingWeek GenerateWeek4()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(4)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(9.7m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateHillWorkout(
                    4,
                    8.0m,
                    EasyPaceRange(),
                    HillPaceRange(),
                    RecoveryPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(9.7m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(21.0m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek5()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(5)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateMediumRunWorkout(11.3m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateTempoWorkout(
                    35,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateMediumRunWorkout(11.3m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(22.5m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek6()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(6)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateMediumRunWorkout(11.3m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateIntervalWorkout(
                    5,
                    800,
                    8.0m,
                    EasyPaceRange(),
                    IntervalPaceRange(),
                    RecoveryPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(11.3m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(16.1m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek7()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(7)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateMediumRunWorkout(12.9m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateHillWorkout(
                    5,
                    8.0m,
                    EasyPaceRange(),
                    HillPaceRange(),
                    RecoveryPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(12.9m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(25.6m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek8()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(8)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateMediumRunWorkout(12.9m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateTempoWorkout(
                    40,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateMediumRunWorkout(12.9m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(27.4m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek9()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(9)
            .WithTrainingPhase(TrainingPhase.TuneUpRace)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateMediumRunWorkout(14.5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateIntervalWorkout(
                    6,
                    800,
                    8.0m,
                    EasyPaceRange(),
                    IntervalPaceRange(),
                    RecoveryPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRestWorkout())
            .WithSunday(WorkoutExtensions.CreateRaceWorkout(21.1m, RacePaceRange())) // Half Marathon
            .Build();
    }

    private TrainingWeek GenerateWeek10()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(10)
            .WithTrainingPhase(TrainingPhase.Build)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateMediumRunWorkout(14.5m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateHillWorkout(
                    6,
                    8.0m,
                    EasyPaceRange(),
                    HillPaceRange(),
                    RecoveryPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(14.5m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(30.6m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek11()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(11)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateMediumRunWorkout(16.1m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateTempoWorkout(
                    45,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateMediumRunWorkout(16.1m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(32.2m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek12()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(12)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(9.7m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateIntervalWorkout(
                    7,
                    800,
                    8.0m,
                    EasyPaceRange(),
                    IntervalPaceRange(),
                    RecoveryPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(9.7m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(19.3m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek13()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(13)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateMediumRunWorkout(16.1m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateHillWorkout(
                    7,
                    8.0m,
                    EasyPaceRange(),
                    HillPaceRange(),
                    RecoveryPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(16.1m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(32.2m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek14()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(14)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateEasyRunWorkout(9.7m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateTempoWorkout(
                    45,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateEasyRunWorkout(9.7m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(19.3m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek15()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(15)
            .WithTrainingPhase(TrainingPhase.Peak)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithTuesday(WorkoutExtensions.CreateMediumRunWorkout(16.1m, EasyPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateIntervalWorkout(
                    8,
                    800,
                    8.0m,
                    EasyPaceRange(),
                    IntervalPaceRange(),
                    RecoveryPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(16.1m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(32.2m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek16()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(16)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithTuesday(
                WorkoutExtensions.CreateHillWorkout(
                    6,
                    8.0m,
                    EasyPaceRange(),
                    HillPaceRange(),
                    RecoveryPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(8.1m, EasyPaceRange()))
            .WithThursday(
                WorkoutExtensions.CreateTempoWorkout(
                    30,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateRacePaceWorkout(6.4m, RacePaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(19.3m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek17()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(17)
            .WithTrainingPhase(TrainingPhase.Taper)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithTuesday(
                WorkoutExtensions.CreateTempoWorkout(
                    30,
                    8.0m,
                    EasyPaceRange(),
                    TempoPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateRacePaceWorkout(6.4m, RacePaceRange()))
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateEasyRunWorkout(6.4m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateLongRunWorkout(12.9m, EasyPaceRange()))
            .Build();
    }

    private TrainingWeek GenerateWeek18()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(18)
            .WithTrainingPhase(TrainingPhase.Race)
            .WithMonday(WorkoutExtensions.CreateEasyRunWorkout(4.8m, EasyPaceRange()))
            .WithTuesday(
                WorkoutExtensions.CreateIntervalWorkout(
                    4,
                    400,
                    6.0m,
                    EasyPaceRange(),
                    IntervalPaceRange(),
                    RecoveryPaceRange()))
            .WithWednesday(WorkoutExtensions.CreateEasyRunWorkout(3.2m, EasyPaceRange()))
            .WithThursday(WorkoutExtensions.CreateRestWorkout())
            .WithFriday(WorkoutExtensions.CreateRestWorkout())
            .WithSaturday(WorkoutExtensions.CreateEasyRunWorkout(3.2m, EasyPaceRange()))
            .WithSunday(WorkoutExtensions.CreateRaceWorkout(42.2m, RacePaceRange())) // Marathon
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
