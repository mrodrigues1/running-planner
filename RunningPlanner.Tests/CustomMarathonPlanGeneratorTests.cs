using RunningPlanner.Core;
using RunningPlanner.Core.Models;
using RunningPlanner.Core.PlanGenerator.Test1;

namespace RunningPlanner.Tests;

public class CustomMarathonPlanGeneratorTests
{
    [Fact]
    public void Test1()
    {
        var planGenerator = new CustomMarathonPlanGenerator();

        var parameters = new CustomMarathonPlanGenerator.PlanParameters
        {
            RaceDate = new DateTime(2025, 10, 1),
            RunningDaysPerWeek = 5,
            QualityWorkoutDays = [DayOfWeek.Tuesday, DayOfWeek.Thursday],
            LongRunDay = DayOfWeek.Sunday,
            RunnerLevel = ExperienceLevel.Intermediate,
            WorkoutTypesToInclude =
            [
                WorkoutType.TempoRun,
                WorkoutType.Intervals,
                WorkoutType.EasyRunWithStrides,
                WorkoutType.RacePace
            ],
            CurrentWeeklyMileage = 40,
            TargetPeakWeeklyMileage = 80,
            PlanWeeks = 18
        };

        var trainingPlan = planGenerator.GenerateTrainingPlan(parameters);
        var analyzer = new TrainingPlanAnalyzer(trainingPlan);
        
    }
}

