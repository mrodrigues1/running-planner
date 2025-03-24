using RunningPlanner.Core;
using RunningPlanner.Core.Models;
using RunningPlanner.Core.PlanGenerator.Test2;

namespace RunningPlanner.Tests;

public class CustomMarathonPlanGenerator2Tests
{
    [Fact]
    public void Test1()
    {
        var planParameters = new MarathonPlanParameters
        {
            RaceDate = DateTime.Today.AddDays(126), // about 18 weeks out
            RunnerLevel = ExperienceLevel.Intermediate,
            WeeklyRunningDays = 5,
            QualityWorkoutDays = 2,
            LongRunDay = DayOfWeek.Saturday,
            CurrentWeeklyMileage = 40m,
            PeakWeeklyMileage = 80m,
            TaperWeeks = 3,
            IncludeSpeedWork = true,
            IncludeTempoRuns = true,
            IncludeHillWork = false,
            IncludeRacePaceRuns = true,
            PreferredWorkoutTypes = [
                WorkoutType.TempoRun,
                WorkoutType.Intervals,
                WorkoutType.EasyRunWithStrides,
                WorkoutType.RacePace]
        };

        var customPlan = CreateCustomPlan(planParameters);
    }

    public TrainingPlan CreateCustomPlan(MarathonPlanParameters parameters)
    {
        var generator = new CustomMarathonPlanGenerator2(parameters);
        var plan = generator.Generate();

        // You can now use TrainingPlanAnalyzer to analyze this plan if needed
        var analyzer = new TrainingPlanAnalyzer(plan);

        // Return the plan
        return plan;
    }
}
