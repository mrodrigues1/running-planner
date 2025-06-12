using RunningPlanner.Core.Models;
using RunningPlanner.Core.Services.WorkoutCalculation;

namespace RunningPlanner.Tests;

public class WorkoutCalculationServiceTests
{
    private readonly WorkoutCalculationService _calculationService = new();

    [Fact]
    public void CalculateTotalTime_WithSimpleSteps_ReturnsCorrectTotal()
    {
        // Arrange
        var step1 = SimpleStep.Create(
            StepType.Run,
            Duration.ForTime(TimeSpan.FromMinutes(10)),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var step2 = SimpleStep.Create(
            StepType.Run,
            Duration.ForTime(TimeSpan.FromMinutes(20)),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var workout = Workout.Create(WorkoutType.EasyRun, step1, step2);

        // Act
        var result = _calculationService.CalculateTotalTime(workout);

        // Assert
        Assert.Equal(TimeSpan.FromMinutes(30), result);
    }

    [Fact]
    public void CalculateEstimatedTime_WithSimpleSteps_ReturnsCorrectTotal()
    {
        // Arrange
        var step1 = SimpleStep.Create(
            StepType.Run,
            Duration.ForKilometers(1),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var step2 = SimpleStep.Create(
            StepType.Run,
            Duration.ForKilometers(2),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var workout = Workout.Create(WorkoutType.EasyRun, step1, step2);

        // Act
        var result = _calculationService.CalculateEstimatedTime(workout);

        // Assert
        Assert.Equal(TimeSpan.FromMinutes(15), result);
    }

    [Fact]
    public void CalculateTotalDistance_WithDistanceSteps_ReturnsCorrectTotal()
    {
        // Arrange
        var step1 = SimpleStep.Create(
            StepType.Run,
            Duration.ForKilometers(3),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var step2 = SimpleStep.Create(
            StepType.Run,
            Duration.ForKilometers(5),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var workout = Workout.Create(WorkoutType.EasyRun, step1, step2);

        // Act
        var result = _calculationService.CalculateTotalDistance(workout);

        // Assert
        Assert.Equal(Distance.Kilometers(8), result);
    }

    [Fact]
    public void CalculateEstimatedDistance_WithTimeSteps_ReturnsCorrectTotal()
    {
        // Arrange
        var step1 = SimpleStep.Create(
            StepType.Run,
            Duration.ForTime(TimeSpan.FromMinutes(30)),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var step2 = SimpleStep.Create(
            StepType.Run,
            Duration.ForTime(TimeSpan.FromMinutes(15)),
            IntensityTarget.Pace(TimeSpan.FromMinutes(4), TimeSpan.FromMinutes(4)));
        var workout = Workout.Create(WorkoutType.EasyRun, step1, step2);

        // Act
        var result = _calculationService.CalculateEstimatedDistance(workout);

        // Assert
        Assert.Equal(Distance.Kilometers(9.75m), result);
    }

    [Fact]
    public void CalculateTotalTime_WithRepeats_FlattersStepsCorrectly()
    {
        // Arrange
        var simpleStep = SimpleStep.Create(
            StepType.Run,
            Duration.ForTime(TimeSpan.FromMinutes(5)),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var repeat = Repeat.Create(2, simpleStep);
        var workout = Workout.Create(WorkoutType.Intervals, repeat);

        // Act
        var result = _calculationService.CalculateTotalTime(workout);

        // Assert
        Assert.Equal(TimeSpan.FromMinutes(10), result);
    }

    [Fact]
    public void CalculateEstimatedDistance_WithRepeats_FlattersStepsCorrectly()
    {
        // Arrange
        var simpleStep = SimpleStep.Create(
            StepType.Run,
            Duration.ForKilometers(1),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var repeat = Repeat.Create(3, simpleStep);
        var workout = Workout.Create(WorkoutType.Intervals, repeat);

        // Act
        var result = _calculationService.CalculateEstimatedDistance(workout);

        // Assert
        Assert.Equal(Distance.Kilometers(3), result);
    }

    [Fact]
    public void CalculateTotalDistance_RoundsToOneDecimalPlace()
    {
        var step = SimpleStep.Create(
            StepType.Run,
            Duration.ForKilometers(1.234m),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var workout = Workout.Create(WorkoutType.EasyRun, step);

        // Act
        var result = _calculationService.CalculateTotalDistance(workout);

        // Assert
        Assert.Equal(Distance.Kilometers(1.2m), result);
    }

    [Theory]
    [InlineData(1.15, 1.2)]
    [InlineData(1.14, 1.1)]
    [InlineData(1.25, 1.3)]
    [InlineData(1.24, 1.2)]
    public void CalculateTotalDistance_RoundsAwayFromZero(decimal input, decimal expected)
    {
        // Arrange
        var step = SimpleStep.Create(
            StepType.Run,
            Duration.ForKilometers(input),
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)));
        var workout = Workout.Create(WorkoutType.EasyRun, step);

        // Act
        var result = _calculationService.CalculateTotalDistance(workout);

        // Assert
        Assert.Equal(Distance.Kilometers(expected), result);
    }

    [Fact]
    public void CalculationMethods_WithNullWorkout_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _calculationService.CalculateTotalTime(null!));
        Assert.Throws<ArgumentNullException>(() => _calculationService.CalculateEstimatedTime(null!));
        Assert.Throws<ArgumentNullException>(() => _calculationService.CalculateTotalDistance(null!));
        Assert.Throws<ArgumentNullException>(() => _calculationService.CalculateEstimatedDistance(null!));
    }

    [Fact]
    public void CalculationMethods_WithRestWorkout_ReturnsZeroValues()
    {
        // Arrange
        var workout = Workout.Create(WorkoutType.Rest);

        // Act
        var totalTime = _calculationService.CalculateTotalTime(workout);
        var estimatedTime = _calculationService.CalculateEstimatedTime(workout);
        var totalDistance = _calculationService.CalculateTotalDistance(workout);
        var estimatedDistance = _calculationService.CalculateEstimatedDistance(workout);

        // Assert
        Assert.Equal(TimeSpan.Zero, totalTime);
        Assert.Equal(TimeSpan.Zero, estimatedTime);
        Assert.Equal(Distance.Kilometers(0), totalDistance);
        Assert.Equal(Distance.Kilometers(0), estimatedDistance);
    }
}