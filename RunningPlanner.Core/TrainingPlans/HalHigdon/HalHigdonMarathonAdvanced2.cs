using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.TrainingPlans.HalHigdon;

public class HalHigdonMarathonAdvanced2
{
    public TrainingPlan TrainingPlan { get; private set; }

    public HalHigdonMarathonAdvanced2()
    {
        TrainingPlan = GenerateDefaultTrainingPlan();
    }

    private TrainingPlan GenerateDefaultTrainingPlan()
    {
        return TrainingPlan.Create(
            new List<TrainingWeek>
            {
                GenerateWeek1(),
                GenerateWeek2(),
                GenerateWeek3(),
                GenerateWeek4(),
                GenerateWeek5(),
                GenerateWeek6(),
                GenerateWeek7(),
                GenerateWeek8(),
                GenerateWeek9(),
                GenerateWeek10(),
                GenerateWeek11(),
                GenerateWeek12(),
                GenerateWeek13(),
                GenerateWeek14(),
                GenerateWeek15(),
                GenerateWeek16(),
                GenerateWeek17(),
                GenerateWeek18()
            });
    }

    private TrainingWeek GenerateWeek1()
    {
        return TrainingWeek.Create(
            weekNumber: 1,
            trainingPhase: TrainingPhase.Base,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateHillRepeat(
                3,
                8.0m,
                EasyPaceRange(),
                HillPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                30,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRacePace(8.1m, RacePaceRange()),
            sunday: Workout.CreateLongRun(16.1m, EasyPaceRange()));
    }

    private TrainingWeek GenerateWeek2()
    {
        return TrainingWeek.Create(
            weekNumber: 2,
            trainingPhase: TrainingPhase.Base,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateTempo(
                30,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            wednesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            thursday: Workout.CreateRacePace(4.8m, RacePaceRange()),
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
            tuesday: Workout.CreateInterval(
                4,
                800,
                400,
                8.0m,
                EasyPaceRange(),
                IntervalPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                30,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRacePace(9.7m, RacePaceRange()),
            sunday: Workout.CreateLongRun(12.9m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek4()
    {
        return TrainingWeek.Create(
            weekNumber: 4,
            trainingPhase: TrainingPhase.Build,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateHillRepeat(
                4,
                8.0m,
                EasyPaceRange(),
                HillPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                35,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRacePace(9.7m, RacePaceRange()),
            sunday: Workout.CreateLongRun(21.0m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek5()
    {
        return TrainingWeek.Create(
            weekNumber: 5,
            trainingPhase: TrainingPhase.Build,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateTempo(
                35,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            wednesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            thursday: Workout.CreateRacePace(4.8m, RacePaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateEasyRun(11.3m, EasyPaceRange()),
            sunday: Workout.CreateLongRun(22.5m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek6()
    {
        return TrainingWeek.Create(
            weekNumber: 6,
            trainingPhase: TrainingPhase.Build,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateInterval(
                5,
                800,
                400,
                8.0m,
                EasyPaceRange(),
                IntervalPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                35,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRacePace(11.3m, RacePaceRange()),
            sunday: Workout.CreateLongRun(16.1m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek7()
    {
        return TrainingWeek.Create(
            weekNumber: 7,
            trainingPhase: TrainingPhase.Build,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateHillRepeat(
                5,
                8.0m,
                EasyPaceRange(),
                HillPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                40,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRacePace(12.9m, RacePaceRange()),
            sunday: Workout.CreateLongRun(25.6m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek8()
    {
        return TrainingWeek.Create(
            weekNumber: 8,
            trainingPhase: TrainingPhase.Build,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateTempo(
                40,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            wednesday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            thursday: Workout.CreateRacePace(4.8m, RacePaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateEasyRun(12.9m, EasyPaceRange()),
            sunday: Workout.CreateLongRun(27.4m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek9()
    {
        return TrainingWeek.Create(
            weekNumber: 9,
            trainingPhase: TrainingPhase.TuneUpRace,
            monday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            tuesday: Workout.CreateInterval(
                6,
                800,
                400,
                8.0m,
                EasyPaceRange(),
                IntervalPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                40,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRest(),
            sunday: Workout.CreateRace(21.1m, RacePaceRange()) // Half Marathon
        );
    }

    private TrainingWeek GenerateWeek10()
    {
        return TrainingWeek.Create(
            weekNumber: 10,
            trainingPhase: TrainingPhase.Build,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateHillRepeat(
                6,
                8.0m,
                EasyPaceRange(),
                HillPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                45,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRacePace(14.5m, RacePaceRange()),
            sunday: Workout.CreateLongRun(30.6m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek11()
    {
        return TrainingWeek.Create(
            weekNumber: 11,
            trainingPhase: TrainingPhase.Peak,
            monday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            tuesday: Workout.CreateTempo(
                45,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            wednesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            thursday: Workout.CreateRacePace(6.4m, RacePaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateEasyRun(16.1m, EasyPaceRange()),
            sunday: Workout.CreateLongRun(32.2m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek12()
    {
        return TrainingWeek.Create(
            weekNumber: 12,
            trainingPhase: TrainingPhase.Peak,
            monday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            tuesday: Workout.CreateInterval(
                7,
                800,
                400,
                8.0m,
                EasyPaceRange(),
                IntervalPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                45,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRacePace(9.7m, RacePaceRange()),
            sunday: Workout.CreateLongRun(19.3m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek13()
    {
        return TrainingWeek.Create(
            weekNumber: 13,
            trainingPhase: TrainingPhase.Peak,
            monday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            tuesday: Workout.CreateHillRepeat(
                7,
                8.0m,
                EasyPaceRange(),
                HillPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                50,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRacePace(16.1m, RacePaceRange()),
            sunday: Workout.CreateLongRun(32.2m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek14()
    {
        return TrainingWeek.Create(
            weekNumber: 14,
            trainingPhase: TrainingPhase.Peak,
            monday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            tuesday: Workout.CreateTempo(
                45,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            wednesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            thursday: Workout.CreateRacePace(8.1m, RacePaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateEasyRun(9.7m, EasyPaceRange()),
            sunday: Workout.CreateLongRun(19.3m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek15()
    {
        return TrainingWeek.Create(
            weekNumber: 15,
            trainingPhase: TrainingPhase.Peak,
            monday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            tuesday: Workout.CreateInterval(
                8,
                800,
                400,
                8.0m,
                EasyPaceRange(),
                IntervalPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                40,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRacePace(16.1m, RacePaceRange()),
            sunday: Workout.CreateLongRun(32.2m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek16()
    {
        return TrainingWeek.Create(
            weekNumber: 16,
            trainingPhase: TrainingPhase.Taper,
            monday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            tuesday: Workout.CreateHillRepeat(
                6,
                8.0m,
                EasyPaceRange(),
                HillPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            thursday: Workout.CreateTempo(
                30,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRacePace(6.4m, RacePaceRange()),
            sunday: Workout.CreateLongRun(19.3m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek17()
    {
        return TrainingWeek.Create(
            weekNumber: 17,
            trainingPhase: TrainingPhase.Taper,
            monday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            tuesday: Workout.CreateTempo(
                30,
                8.0m,
                EasyPaceRange(),
                TempoPaceRange()),
            wednesday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            thursday: Workout.CreateRacePace(6.4m, RacePaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            sunday: Workout.CreateLongRun(12.9m, EasyPaceRange())
        );
    }

    private TrainingWeek GenerateWeek18()
    {
        return TrainingWeek.Create(
            weekNumber: 18,
            trainingPhase: TrainingPhase.Race,
            monday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            tuesday: Workout.CreateInterval(
                4,
                400,
                400,
                6.0m,
                EasyPaceRange(),
                IntervalPaceRange(),
                RecoveryPaceRange()),
            wednesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            thursday: Workout.CreateRest(),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateEasyRun(3.2m, EasyPaceRange()),
            sunday: Workout.CreateRace(42.2m, RacePaceRange()) // Marathon
        );
    }

    // Pace calculation methods adjusted for Advanced 2 level runners
    private static (TimeSpan min, TimeSpan max) EasyPaceRange()
    {
        // Define easy pace range for Advanced 2 runners (per kilometer)
        return (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(15)),
            TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)));
    }

    private static (TimeSpan min, TimeSpan max) RacePaceRange()
    {
        // Define race pace range for Advanced 2 runners (per kilometer)
        return (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(15)),
            TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30)));
    }

    private static (TimeSpan min, TimeSpan max) TempoPaceRange()
    {
        // Define tempo pace range for Advanced 2 runners (per kilometer)
        return (TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(0)),
            TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(20)));
    }

    private static (TimeSpan min, TimeSpan max) IntervalPaceRange()
    {
        // Define interval pace range for Advanced 2 runners (per kilometer)
        return (TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(30)),
            TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(50)));
    }

    private static (TimeSpan min, TimeSpan max) HillPaceRange()
    {
        // Define hill repeat pace range for Advanced 2 runners (per kilometer)
        // Hills are typically done at hard effort, similar to intervals
        return (TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(35)),
            TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(55)));
    }

    private static (TimeSpan min, TimeSpan max) RecoveryPaceRange()
    {
        // Define recovery pace range for Advanced 2 runners (per kilometer)
        return (TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(0)),
            TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(0)));
    }
}
