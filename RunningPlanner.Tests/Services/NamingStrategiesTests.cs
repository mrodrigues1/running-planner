using RunningPlanner.Core.Models;
using RunningPlanner.Core.Services.WorkoutNaming.Strategies;

namespace RunningPlanner.Tests.Services;

public class NamingStrategiesTests
{
    [Fact]
    public void SimpleRunNamingStrategy_SupportedWorkoutTypes_ContainsExpectedTypes()
    {
        // Arrange
        var strategy = new SimpleRunNamingStrategy();

        // Act & Assert
        Assert.Contains(WorkoutType.EasyRun, strategy.SupportedWorkoutTypes);
        Assert.Contains(WorkoutType.MediumRun, strategy.SupportedWorkoutTypes);
        Assert.Contains(WorkoutType.LongRun, strategy.SupportedWorkoutTypes);
        Assert.Equal(3, strategy.SupportedWorkoutTypes.Count);
    }

    [Fact]
    public void IntervalNamingStrategy_SupportedWorkoutTypes_ContainsExpectedTypes()
    {
        // Arrange
        var strategy = new IntervalNamingStrategy();

        // Act & Assert
        Assert.Contains(WorkoutType.HillRepeat, strategy.SupportedWorkoutTypes);
        Assert.Contains(WorkoutType.Intervals, strategy.SupportedWorkoutTypes);
        Assert.Contains(WorkoutType.Repetition, strategy.SupportedWorkoutTypes);
        Assert.Contains(WorkoutType.TempoRunRepeat, strategy.SupportedWorkoutTypes);
        Assert.Contains(WorkoutType.ThresholdRepeat, strategy.SupportedWorkoutTypes);
        Assert.Equal(5, strategy.SupportedWorkoutTypes.Count);
    }

    [Fact]
    public void TempoRunNamingStrategy_SupportedWorkoutTypes_ContainsExpectedTypes()
    {
        // Arrange
        var strategy = new TempoRunNamingStrategy();

        // Act & Assert
        Assert.Contains(WorkoutType.TempoRun, strategy.SupportedWorkoutTypes);
        Assert.Contains(WorkoutType.Threshold, strategy.SupportedWorkoutTypes);
        Assert.Equal(2, strategy.SupportedWorkoutTypes.Count);
    }

    [Fact]
    public void EasyRunWithStridesNamingStrategy_SupportedWorkoutTypes_ContainsExpectedTypes()
    {
        // Arrange
        var strategy = new EasyRunWithStridesNamingStrategy();

        // Act & Assert
        Assert.Contains(WorkoutType.EasyRunWithStrides, strategy.SupportedWorkoutTypes);
        Assert.Single(strategy.SupportedWorkoutTypes);
    }

    [Fact]
    public void DefaultNamingStrategy_SupportedWorkoutTypes_IsEmpty()
    {
        // Arrange
        var strategy = new DefaultNamingStrategy();

        // Act & Assert
        Assert.Empty(strategy.SupportedWorkoutTypes);
    }

    [Fact]
    public void SimpleRunNamingStrategy_GenerateNameComponents_ReturnsExpectedComponents()
    {
        // Arrange
        var strategy = new SimpleRunNamingStrategy();

        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            10.0m,
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(6)));
        var workout = Workout.Create(WorkoutType.EasyRun, [step]);

        // Act
        var components = strategy.GenerateNameComponents(workout).ToList();

        // Assert
        Assert.Equal(3, components.Count);
        Assert.Contains("Easy Run", components);
        Assert.Contains("10 km", components);
        Assert.Contains("@5:00~6:00 min/km", components);
    }

    [Fact]
    public void IntervalNamingStrategy_GenerateNameComponents_ReturnsExpectedComponents()
    {
        // Arrange
        var strategy = new IntervalNamingStrategy();

        var runStep = SimpleStep.CreateWithKilometers(
            StepType.Run,
            0.4m, // 400m
            IntensityTarget.Pace(
                TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(4)));

        var recoverStep = SimpleStep.CreateWithKilometers(
            StepType.Recover,
            0.2m, // 200m
            IntensityTarget.Pace(TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)), TimeSpan.FromMinutes(6)));

        var steps = new List<Step> {runStep, recoverStep, runStep, recoverStep}; // 2 intervals
        var workout = Workout.Create(WorkoutType.Intervals, steps);

        // Act
        var components = strategy.GenerateNameComponents(workout).ToList();

        // Assert
        Assert.Equal(3, components.Count);
        Assert.Contains("Intervals", components);
        Assert.Contains("2 x (400m@3:45~4:00 min/km + 200m Recover)", components);
    }

    [Fact]
    public void TempoRunNamingStrategy_GenerateNameComponents_ReturnsExpectedComponents()
    {
        // Arrange
        var strategy = new TempoRunNamingStrategy();

        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            8.0m,
            IntensityTarget.Pace(
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(15)),
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30))));
        var workout = Workout.Create(WorkoutType.TempoRun, [step]);

        // Act
        var components = strategy.GenerateNameComponents(workout).ToList();

        // Assert
        Assert.Equal(3, components.Count);
        Assert.Contains("Tempo Run", components);
        Assert.Contains("8km@4:15~4:30 min/km", components);
    }

    [Fact]
    public void DefaultNamingStrategy_RestWorkout_ReturnsOnlyDescription()
    {
        // Arrange
        var strategy = new DefaultNamingStrategy();
        var workout = Workout.Create(WorkoutType.Rest, []);

        // Act
        var components = strategy.GenerateNameComponents(workout).ToList();

        // Assert
        Assert.Single(components);
        Assert.Contains("Rest Day", components);
    }
}
