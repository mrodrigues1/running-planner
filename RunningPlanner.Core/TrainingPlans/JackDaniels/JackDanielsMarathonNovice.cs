using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.TrainingPlans.JackDaniels;

/// <summary>
/// Implementation of Jack Daniels' Novice Marathon Training Plan.
/// </summary>
public class JackDanielsMarathonNovice
{
    public TrainingPlan TrainingPlan { get; private set; }

    public JackDanielsMarathonNovice()
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
                // Add remaining weeks here as needed
            });
    }

    /// <summary>
    /// Week 1 of the Jack Daniels Novice Marathon plan (18 weeks until race)
    /// </summary>
    private TrainingWeek GenerateWeek1()
    {
        return TrainingWeek.Create(
            weekNumber: 1,
            trainingPhase: TrainingPhase.Base,
            monday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
            tuesday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
            wednesday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
            thursday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
            friday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new WalkRunInterval(2, TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(3))),
            saturday: Workout.CreateRest(),
            sunday: Workout.CreateRest());
    }

    /// <summary>
    /// Week 2 of the Jack Daniels Novice Marathon plan (17 weeks until race)
    /// </summary>
    private TrainingWeek GenerateWeek2()
    {
        return TrainingWeek.Create(
            weekNumber: 2,
            trainingPhase: TrainingPhase.Base,
            monday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
            tuesday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
            wednesday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
            thursday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
            friday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new WalkRunInterval(2, TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(3))),
            saturday: Workout.CreateRest(),
            sunday: Workout.CreateRest());
    }

    /// <summary>
    /// Week 3 of the Jack Daniels Novice Marathon plan (16 weeks until race)
    /// </summary>
    private TrainingWeek GenerateWeek3()
    {
        return TrainingWeek.Create(
            weekNumber: 3,
            trainingPhase: TrainingPhase.Base,
            monday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
            tuesday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
            wednesday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
            thursday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
            friday: Workout.CreateRunWalkWorkout(
                EasyPaceRange(),
                WalkPaceRange(),
                new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new WalkRunInterval(2, TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(3))),
            saturday: Workout.CreateRest(),
            sunday: Workout.CreateRest());
    }

    // Pace calculation methods for Novice runners
    private static (TimeSpan min, TimeSpan max) EasyPaceRange()
    {
        // Define easy pace range for novice runners (per kilometer)
        return (TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(30)),
            TimeSpan.FromMinutes(9).Add(TimeSpan.FromSeconds(0)));
    }

    private static (TimeSpan min, TimeSpan max) WalkPaceRange()
    {
        // Define walking pace range for recovery segments (per kilometer)
        return (TimeSpan.FromMinutes(9).Add(TimeSpan.FromSeconds(30)),
            TimeSpan.FromMinutes(12).Add(TimeSpan.FromSeconds(0)));
    }
}