using RunningPlanner.Core.Extensions.JackDaniels;
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
                GenerateWeek3()
            });
    }

    /// <summary>
    /// Week 1 of the Jack Daniels Novice Marathon plan (18 weeks until race)
    /// </summary>
    private TrainingWeek GenerateWeek1()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(1)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionA()) // First workout of the week
            .WithTuesday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionB()) // Optional repeat of A
            .WithWednesday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionC())
            .WithThursday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionD()) // Optional repeat of C
            .WithFriday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionE()) // Last workout of the week
            .WithSaturday(WorkoutExtensions.CreateRestWorkout())
            .WithSunday(WorkoutExtensions.CreateRestWorkout())
            .Build();
    }

    /// <summary>
    /// Week 2 of the Jack Daniels Novice Marathon plan (17 weeks until race)
    /// </summary>
    private TrainingWeek GenerateWeek2()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(2)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionA()) // Same sessions repeat in these early weeks
            .WithTuesday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionB())
            .WithWednesday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionC())
            .WithThursday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionD())
            .WithFriday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionE())
            .WithSaturday(WorkoutExtensions.CreateRestWorkout())
            .WithSunday(WorkoutExtensions.CreateRestWorkout())
            .Build();
    }

    /// <summary>
    /// Week 3 of the Jack Daniels Novice Marathon plan (16 weeks until race)
    /// </summary>
    private TrainingWeek GenerateWeek3()
    {
        return TrainingWeek.TrainingWeekBuilder
            .CreateBuilder()
            .WithWeekNumber(3)
            .WithTrainingPhase(TrainingPhase.Base)
            .WithMonday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionA()) // Same sessions repeat in these early weeks
            .WithTuesday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionB())
            .WithWednesday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionC())
            .WithThursday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionD())
            .WithFriday(JackDanielsMarathonNoviceSections18To16UntilRace.CreateSessionE())
            .WithSaturday(WorkoutExtensions.CreateRestWorkout())
            .WithSunday(WorkoutExtensions.CreateRestWorkout())
            .Build();
    }
}