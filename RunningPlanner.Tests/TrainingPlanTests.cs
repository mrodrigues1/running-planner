using RunningPlanner.Core;

namespace RunningPlanner.Tests;

public class TrainingPlanTests
{
    [Fact]
    public void GenerateDefaultTrainingPlan_WhenHalHigdonMarathonIntermediate2_ThenFirstWeekTotalKilometersIs41_9Km()
    {
        // Arrange
        const decimal expectedFirstWeekTotalKilometers = 41.9m;
        var sut = new HalHigdonMarathonIntermediate2();
        
        // Act
        var result = sut.GenerateDefaultTrainingPlan();
        
        // Assert
        var resultTrainingWeeks = result.TrainingWeeks.First();
        Assert.Equal(expectedFirstWeekTotalKilometers, resultTrainingWeeks.TotalKilometerMileage, precision: 1);
    }
}