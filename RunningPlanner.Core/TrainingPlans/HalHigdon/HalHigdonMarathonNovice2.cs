using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.TrainingPlans.HalHigdon;

public class HalHigdonMarathonNovice2
{
    public TrainingPlan TrainingPlan { get; private set; }
    public TrainingPlanAnalyzer TrainingPlanAnalyzer { get; private set; }

    public HalHigdonMarathonNovice2()
    {
        TrainingPlan = GenerateDefaultTrainingPlan();
        TrainingPlanAnalyzer = new TrainingPlanAnalyzer(TrainingPlan);
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
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            wednesday: Workout.CreateRacePace(8.1m, RacePaceRange()),
            thursday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(12.9m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek2()
    {
        return TrainingWeek.Create(
            weekNumber: 2,
            trainingPhase: TrainingPhase.Base,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            wednesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            thursday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(14.5m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek3()
    {
        return TrainingWeek.Create(
            weekNumber: 3,
            trainingPhase: TrainingPhase.Base,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            wednesday: Workout.CreateRacePace(8.1m, RacePaceRange()),
            thursday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(9.7m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek4()
    {
        return TrainingWeek.Create(
            weekNumber: 4,
            trainingPhase: TrainingPhase.Base,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            wednesday: Workout.CreateRacePace(9.7m, RacePaceRange()),
            thursday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(17.7m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek5()
    {
        return TrainingWeek.Create(
            weekNumber: 5,
            trainingPhase: TrainingPhase.Build,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            wednesday: Workout.CreateEasyRun(9.7m, EasyPaceRange()),
            thursday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(19.3m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek6()
    {
        return TrainingWeek.Create(
            weekNumber: 6,
            trainingPhase: TrainingPhase.Build,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            wednesday: Workout.CreateRacePace(9.7m, RacePaceRange()),
            thursday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(14.5m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek7()
    {
        return TrainingWeek.Create(
            weekNumber: 7,
            trainingPhase: TrainingPhase.Build,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            wednesday: Workout.CreateRacePace(11.3m, RacePaceRange()),
            thursday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(22.5m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek8()
    {
        return TrainingWeek.Create(
            weekNumber: 8,
            trainingPhase: TrainingPhase.Build,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            wednesday: Workout.CreateEasyRun(11.3m, EasyPaceRange()),
            thursday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(24.1m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek9()
    {
        return TrainingWeek.Create(
            weekNumber: 9,
            trainingPhase: TrainingPhase.TuneUpRace,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            wednesday: Workout.CreateRacePace(11.3m, RacePaceRange()),
            thursday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateRest(),
            sunday: Workout.CreateRace(21.1m, RacePaceRange()) // Half Marathon
        );
    }

    private TrainingWeek GenerateWeek10()
    {
        return TrainingWeek.Create(
            weekNumber: 10,
            trainingPhase: TrainingPhase.Peak,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            wednesday: Workout.CreateRacePace(12.9m, RacePaceRange()),
            thursday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(27.4m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek11()
    {
        return TrainingWeek.Create(
            weekNumber: 11,
            trainingPhase: TrainingPhase.Peak,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            wednesday: Workout.CreateEasyRun(12.9m, EasyPaceRange()),
            thursday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(29.0m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek12()
    {
        return TrainingWeek.Create(
            weekNumber: 12,
            trainingPhase: TrainingPhase.Peak,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            wednesday: Workout.CreateRacePace(12.9m, RacePaceRange()),
            thursday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(21.0m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek13()
    {
        return TrainingWeek.Create(
            weekNumber: 13,
            trainingPhase: TrainingPhase.Peak,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            wednesday: Workout.CreateRacePace(8.1m, RacePaceRange()),
            thursday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(30.6m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek14()
    {
        return TrainingWeek.Create(
            weekNumber: 14,
            trainingPhase: TrainingPhase.Peak,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            wednesday: Workout.CreateEasyRun(12.9m, EasyPaceRange()),
            thursday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(19.3m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek15()
    {
        return TrainingWeek.Create(
            weekNumber: 15,
            trainingPhase: TrainingPhase.Peak,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            wednesday: Workout.CreateRacePace(8.1m, RacePaceRange()),
            thursday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(32.2m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek16()
    {
        return TrainingWeek.Create(
            weekNumber: 16,
            trainingPhase: TrainingPhase.Taper,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            wednesday: Workout.CreateRacePace(6.4m, RacePaceRange()),
            thursday: Workout.CreateEasyRun(8.1m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(19.3m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek17()
    {
        return TrainingWeek.Create(
            weekNumber: 17,
            trainingPhase: TrainingPhase.Taper,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            wednesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            thursday: Workout.CreateEasyRun(6.4m, EasyPaceRange()),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateLongRun(12.9m, EasyPaceRange()),
            sunday: Workout.CreateRest()
        );
    }

    private TrainingWeek GenerateWeek18()
    {
        return TrainingWeek.Create(
            weekNumber: 18,
            trainingPhase: TrainingPhase.Race,
            monday: Workout.CreateRest(),
            tuesday: Workout.CreateEasyRun(4.8m, EasyPaceRange()),
            wednesday: Workout.CreateEasyRun(3.2m, EasyPaceRange()),
            thursday: Workout.CreateRest(),
            friday: Workout.CreateRest(),
            saturday: Workout.CreateEasyRun(3.2m, EasyPaceRange()),
            sunday: Workout.CreateRace(42.2m, RacePaceRange()) // Marathon
        );
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
