using RunningPlanner.Core.Models;

namespace RunningPlanner.Tests;

public class WorkoutCalculationIntegrationTests
{
    [Fact]
    public void Workout_CalculationProperties_UseCalculationService()
    {
        // Arrange
        var step1 = SimpleStep.Create(
            StepType.Run,
            Duration.ForKilometers(5),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));

        var step2 = SimpleStep.Create(
            StepType.Run,
            Duration.ForTime(TimeSpan.FromMinutes(30)),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        
        // Act
        var workout = Workout.Create(WorkoutType.EasyRun, step1, step2);

        // Assert
        Assert.Equal(Distance.Kilometers(11), workout.TotalDistance);
        Assert.Equal(Distance.Kilometers(11), workout.EstimatedDistance);
        Assert.Equal(TimeSpan.FromMinutes(55), workout.TotalTime);
        Assert.Equal(TimeSpan.FromMinutes(55), workout.EstimatedTime);
    }

    [Fact]
    public void Workout_WithComplexRepeats_CalculatesCorrectly()
    {
        // Arrange
        var warmup = SimpleStep.Create(
            StepType.WarmUp,
            Duration.ForKilometers(2),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));

        var intervalStep = SimpleStep.Create(
            StepType.Run,
            Duration.ForKilometers(1),
            IntensityTarget.Pace(TimeSpan.FromMinutes(4), TimeSpan.FromMinutes(4)));

        var recoveryStep = SimpleStep.Create(
            StepType.Recover,
            Duration.ForTime(TimeSpan.FromMinutes(2)),
            IntensityTarget.Pace(TimeSpan.FromMinutes(7), TimeSpan.FromMinutes(7)));
        var intervals = Repeat.Create(4, intervalStep, recoveryStep);

        var cooldown = SimpleStep.Create(
            StepType.CoolDown,
            Duration.ForKilometers(1),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));

        // Act
        var workout = Workout.Create(
            WorkoutType.Intervals,
            warmup,
            intervals,
            cooldown);

        // Assert
        Assert.Equal(Distance.Kilometers(8.2m), workout.TotalDistance);
        Assert.Equal(TimeSpan.FromMinutes(39), workout.TotalTime);
    }

    [Fact]
    public void Workout_Properties_AreConsistentAcrossMultipleCalls()
    {
        // Arrange
        var step = SimpleStep.Create(
            StepType.Run,
            Duration.ForKilometers(5),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var workout = Workout.Create(WorkoutType.LongRun, step);

        // Act
        var firstCallTotalTime = workout.TotalTime;
        var secondCallTotalTime = workout.TotalTime;
        var firstCallTotalDistance = workout.TotalDistance;
        var secondCallTotalDistance = workout.TotalDistance;

        // Assert
        Assert.Equal(firstCallTotalTime, secondCallTotalTime);
        Assert.Equal(firstCallTotalDistance, secondCallTotalDistance);
    }
}
