using RunningPlanner.Core.Models;

namespace RunningPlanner.Tests;

public class SimpleStepTests
{
    [Fact]
    public void SimpleStep_WhenDistanceIs10Km_ThenTotalDistanceIs10Km()
    {
        // Arrange
        var duration = Duration.ForKilometers(10);

        var intensityTarget = IntensityTarget.Pace(new TimeSpan(0, 6, 0), new TimeSpan(0, 6, 30));

        var step = SimpleStep.CreateWithKilometers(StepType.Run, 10, intensityTarget);

        // Act & Assert
        Assert.Equal(10, step.TotalDistance.DistanceValue);
    }

    [Fact]
    public void SimpleStep_WhenDistanceIs10Km_ThenEstimatedTimeIs1H2M30S()
    {
        // Arrange
        var duration = Duration.ForKilometers(10);

        var intensityTarget = IntensityTarget.Pace(new TimeSpan(0, 6, 0), new TimeSpan(0, 6, 30));

        var step = SimpleStep.CreateWithKilometers(StepType.Run, 10, intensityTarget);

        // Act & Assert
        Assert.Equal(new TimeSpan(1, 2, 30), step.EstimatedTime);
    }
}
