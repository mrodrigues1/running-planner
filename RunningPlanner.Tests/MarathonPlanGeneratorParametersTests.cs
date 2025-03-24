using RunningPlanner.Core.PlanGenerator.Marathon;

namespace RunningPlanner.Tests;

public class MarathonPlanGeneratorParametersTests
{
    [Fact]
    public void Test1()
    {
        var marathonPlanGeneratorParameters = new MarathonPlanGeneratorParameters
        {
            RaceDate = DateTime.Today.AddDays(120),
            RaceDistance = 42.1m,
            WeeklyRunningDays = 5,
            CurrentWeeklyMileage = 45m,
            PeakWeeklyMileage = 80m
        };
        
        var sut = new MarathonPlanGenerator(marathonPlanGeneratorParameters);
        sut.Generate();
    }
}
