using RunningPlanner.Core.Models;

namespace RunningPlanner.UnitTests;

public class SimpleStepTests
{
    [Fact]
    public void SimpleStep_WhenDistanceIs10Km_ThenTotalDistanceIs10Km()
    {
        // Arrange
        var duration = Duration.DurationBuilder
            .CreateBuilder()
            .WithKilometers(10)
            .WithType(DurationType.Distance)
            .Build();
        
        var intensityTarget = IntensityTarget.IntensityTargetBuilder
            .CreateBuilder()
            .WithPaceRange(new TimeSpan(0, 6, 0), new TimeSpan(0, 6, 30))
            .Build();
        
        var step = SimpleStep.SimpleStepBuilder
            .CreateBuilder()
            .WithType(StepType.Run)
            .WithDuration(duration)
            .WithIntensityTarget(intensityTarget)
            .Build();
        
        // Act & Assert
        Assert.Equal(10, step.TotalDistance.DistanceValue);
    }
    
    [Fact]
    public void SimpleStep_WhenDistanceIs10Km_ThenEstimatedTimeIs1H2M30S()
    {
        // Arrange
        var duration = Duration.DurationBuilder
            .CreateBuilder()
            .WithKilometers(10)
            .WithType(DurationType.Distance)
            .Build();
        
        var intensityTarget = IntensityTarget.IntensityTargetBuilder
            .CreateBuilder()
            .WithType(IntensityTargetType.Pace)
            .WithPaceRange(new TimeSpan(0, 6, 0), new TimeSpan(0, 6, 30))
            .Build();
        
        var step = SimpleStep.SimpleStepBuilder
            .CreateBuilder()
            .WithType(StepType.Run)
            .WithDuration(duration)
            .WithIntensityTarget(intensityTarget)
            .Build();
        
        // Act & Assert
        Assert.Equal(new TimeSpan(1, 2, 30), step.EstimatedTime);
    }
}