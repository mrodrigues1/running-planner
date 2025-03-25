using RunningPlanner.Core;
using RunningPlanner.Core.PlanGenerator.Marathon;

namespace RunningPlanner.Tests;

public class MarathonPlanGeneratorTests
{
    [Fact]
    public void Test1()
    {
        var marathonPlanGeneratorParameters = new MarathonPlanGeneratorParameters
        {
            RaceDate = new DateTime(2025, 7, 6),
            RaceDistance = 42.2m,
            GoalTime = TimeSpan.FromHours(3).Add(TimeSpan.FromMinutes(59)),
            WeeklyRunningDays =
                [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday],
            CurrentWeeklyMileage = 45m,
            PeakWeeklyMileage = 80m,
            RunnerLevel = ExperienceLevel.Intermediate,
            LongRunDay = DayOfWeek.Saturday
        };

        var sut = new MarathonPlanGenerator(marathonPlanGeneratorParameters);
        sut.Generate();
    }
}
