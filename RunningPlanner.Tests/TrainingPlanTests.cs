using RunningPlanner.Core.TrainingPlans.HalHigdon;

namespace RunningPlanner.Tests;

public class TrainingPlanTests
{
    [Fact]
    public void GenerateDefaultTrainingPlan_WhenHalHigdonMarathonIntermediate2_ThenFirstWeekTotalKilometersIs41_9Km()
    {
        // Arrange
        const decimal expectedFirstWeekTotalKilometers = 41.9m;
        
        
        // Act
        var sut = new HalHigdonMarathonIntermediate2();
        
        // Assert
        var resultTrainingWeeks = sut.TrainingPlan.TrainingWeeks.First();
        Assert.Equal(expectedFirstWeekTotalKilometers, resultTrainingWeeks.TotalKilometerMileage, precision: 1);
    }
}