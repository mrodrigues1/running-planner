using RunningPlanner.Core.Models;
using RunningPlanner.Core.PlanGenerator.Marathon;

namespace RunningPlanner.Tests;

public class MarathonPlanGeneratorTests
{
    [Fact]
    public void Test1()
    {
        var futureDate = DateTime.Now.AddDays(16 * 7);
        var daysUntilSunday = ((int) DayOfWeek.Sunday - (int) futureDate.DayOfWeek + 7) % 7;
        var raceDateOnSunday = futureDate.AddDays(daysUntilSunday);

        var marathonPlanGeneratorParameters = new MarathonPlanGeneratorParameters
        {
            RaceDate = raceDateOnSunday,
            RaceDistance = 42.2m,
            GoalTime = TimeSpan.FromHours(3).Add(TimeSpan.FromMinutes(59)),
            WeeklyRunningDays =
                [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday],
            QualityWorkoutDays = [DayOfWeek.Wednesday],
            PreferredWorkoutTypes = [WorkoutType.Intervals],
            IncludeMidWeekMediumRun = true,
            CurrentWeeklyMileage = 45m,
            PeakWeeklyMileage = 80m,
            RunnerLevel = ExperienceLevel.Intermediate,
            LongRunDay = DayOfWeek.Saturday
        };

        var sut = new MarathonPlanGenerator(marathonPlanGeneratorParameters);
        var trainingPlan = sut.Generate();

        Assert.NotNull(trainingPlan);
    }

    [Fact]
    public void WorkoutName()
    {
        // Arrange
        var futureDate = DateTime.Now.AddDays(16 * 7);
        var daysUntilSunday = ((int) DayOfWeek.Sunday - (int) futureDate.DayOfWeek + 7) % 7;
        var raceDateOnSunday = futureDate.AddDays(daysUntilSunday);

        var marathonPlanGeneratorParameters = new MarathonPlanGeneratorParameters
        {
            RaceDate = raceDateOnSunday,
            RaceDistance = 42.2m,
            GoalTime = TimeSpan.FromHours(3).Add(TimeSpan.FromMinutes(59)),
            WeeklyRunningDays =
                [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday],
            QualityWorkoutDays = [DayOfWeek.Wednesday],
            PreferredWorkoutTypes = [WorkoutType.Intervals],
            IncludeMidWeekMediumRun = true,
            CurrentWeeklyMileage = 45m,
            PeakWeeklyMileage = 80m,
            RunnerLevel = ExperienceLevel.Intermediate,
            LongRunDay = DayOfWeek.Saturday
        };

        var sut = new MarathonPlanGenerator(marathonPlanGeneratorParameters);

        // Act
        var trainingPlan = sut.Generate();

        // Assert
        var easyRunWithStrides = trainingPlan
            .TrainingWeeks
            .SelectMany(x => x.Workouts)
            .First(x => x.Type == WorkoutType.EasyRunWithStrides);
        Assert.NotNull(easyRunWithStrides.Name);
    }
}
