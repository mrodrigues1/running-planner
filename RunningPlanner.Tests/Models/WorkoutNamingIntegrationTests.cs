using System.Diagnostics;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Tests.Models;

/// <summary>
/// Integration tests to ensure Workout.Name property works correctly after refactoring.
/// </summary>
public class WorkoutNamingIntegrationTests
{
    [Fact]
    public void Workout_Name_Property_ReturnsCorrectName()
    {
        // Arrange
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            10.0m,
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(6)));

        var workout = Workout.Create(WorkoutType.EasyRun, [step]);

        // Act
        var name = workout.Name;

        // Assert
        Assert.NotNull(name);
        Assert.NotEmpty(name);
        Assert.Contains("Easy Run", name);
        Assert.Contains("10 km", name);
        Assert.Contains("@5:00~6:00 min/km", name);
    }

    [Fact]
    public void Workout_Name_Property_IsConsistent()
    {
        // Arrange
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            15.0m,
            IntensityTarget.Pace(
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(5)));

        var workout = Workout.Create(WorkoutType.LongRun, [step]);

        // Act
        var name1 = workout.Name;
        var name2 = workout.Name;
        var name3 = workout.Name;

        // Assert
        Assert.Equal(name1, name2);
        Assert.Equal(name2, name3);
    }

    [Fact]
    public void Workout_Name_Property_HandlesComplexWorkouts()
    {
        // Arrange - Create an interval workout
        var runStep = SimpleStep.CreateWithKilometers(
            StepType.Run,
            0.8m, // 800m
            IntensityTarget.Pace(
                TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(20)),
                TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(30))));

        var recoverStep = SimpleStep.CreateWithKilometers(
            StepType.Recover,
            0.4m, // 400m
            IntensityTarget.Pace(TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)), TimeSpan.FromMinutes(6)));

        var steps = new List<Step>();

        for (int i = 0; i < 5; i++)
        {
            steps.Add(runStep);
            steps.Add(recoverStep);
        }

        var workout = Workout.Create(WorkoutType.Intervals, steps);

        // Act
        var name = workout.Name;

        // Assert
        Assert.NotNull(name);
        Assert.Contains("Intervals", name);
        Assert.Contains("5 x (800m", name);
        Assert.Contains("+ 400m Recover)", name);
    }

    [Fact]
    public void Workout_Name_Property_HandlesRestWorkouts()
    {
        // Arrange
        var workout = Workout.Create(WorkoutType.Rest, []);

        // Act
        var name = workout.Name;

        // Assert
        Assert.Equal("Rest Day", name);
    }

    [Fact]
    public void Workout_Name_Property_ThreadSafe()
    {
        // Arrange
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            12.0m,
            IntensityTarget.Pace(
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(15)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45))));

        var workout = Workout.Create(WorkoutType.EasyRun, [step]);

        // Act - Access Name property from multiple threads
        var tasks = new List<Task<string>>();

        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(() => workout.Name));
        }

        Task.WaitAll(tasks.ToArray());

        // Assert - All results should be identical
        var firstResult = tasks[0].Result;
        Assert.All(tasks, task => Assert.Equal(firstResult, task.Result));
    }

    [Fact]
    public void Workout_Name_Property_PerformanceTest()
    {
        // Arrange
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            8.0m,
            IntensityTarget.Pace(
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(45)),
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(15))));

        var workout = Workout.Create(WorkoutType.TempoRun, [step]);

        // Act - Measure time for multiple Name property accesses
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < 1000; i++)
        {
            var name = workout.Name;
            Assert.NotNull(name);
        }

        stopwatch.Stop();

        // Assert - Should complete quickly (under 50ms for 1000 calls)
        Assert.True(
            stopwatch.ElapsedMilliseconds < 50,
            $"Name property access took {stopwatch.ElapsedMilliseconds}ms for 1000 calls, expected < 50ms");
    }

    [Fact]
    public void Workout_Name_Property_BackwardCompatibility()
    {
        // This test ensures the refactoring doesn't break existing functionality
        // by testing all major workout types

        var testCases = new[]
        {
            (WorkoutType.EasyRun, "Easy Run"),
            (WorkoutType.LongRun, "Long Run"),
            (WorkoutType.MediumRun, "Mid Distance Run"),
            (WorkoutType.TempoRun, "Tempo Run"),
            (WorkoutType.Threshold, "Threshold"),
            (WorkoutType.Rest, "Rest Day")
        };

        foreach (var (workoutType, expectedDescription) in testCases)
        {
            // Arrange
            var steps = workoutType == WorkoutType.Rest
                ? new List<Step>()
                : new List<Step>
                {
                    SimpleStep.CreateWithKilometers(
                        StepType.Run,
                        5.0m,
                        IntensityTarget.Pace(
                            TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)),
                            TimeSpan.FromMinutes(6)))
                };

            var workout = Workout.Create(workoutType, steps);

            // Act
            var name = workout.Name;

            // Assert
            Assert.Contains(expectedDescription, name);
        }
    }
}
