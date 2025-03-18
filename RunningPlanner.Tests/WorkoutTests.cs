using RunningPlanner.Core.Extensions;
using RunningPlanner.Core.Models;

namespace RunningPlanner.Tests;

public class WorkoutTests
{
    [Fact]
    public void Workout_WhenCreatedWithRepeats_ThenTotalDistanceIs8_7Km()
    {
        // Arrange
        var easyPace = (TimeSpan.FromMinutes(6), TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)));

        var restPace = (TimeSpan.FromMinutes(7), TimeSpan.FromMinutes(10));

        var workoutBuilder = Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.Intervals)
            .WithSimpleStep(StepType.WarmUp, 2m, easyPace)
            .WithSimpleStep(StepType.Rest, 0.5m, restPace)
            .WithRepeatStep(
                3,
                1m,
                0.4m,
                (
                    TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(15)),
                    TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(35))),
                restPace)
            .WithSimpleStep(StepType.CoolDown, 2m, easyPace);


        // Act
        var workout = workoutBuilder.Build();

        // Assert
        Assert.Equal(8.7m, workout.TotalDistance.DistanceValue);
    }

    [Fact]
    public void Workout_WhenCreatedWithWalkRunIntervals_ThenEstimatedTimeIs18M()
    {
        // Arrange
        var easyPace = (TimeSpan.FromMinutes(6), TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)));

        var restPace = (TimeSpan.FromMinutes(7), TimeSpan.FromMinutes(10));

        // Act
        var workout = WorkoutExtensions
            .CreateRunWalkWorkout(
                easyPace,
                restPace,
                [new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))]);

        // Assert
        Assert.Equal(workout.EstimatedTime, TimeSpan.FromMinutes(18));
    }
    
    [Fact]
    public void Workout_WhenCreatedWithMultipleWalkRunIntervals_ThenEstimatedTimeIs30M()
    {
        // Arrange
        var easyPace = (TimeSpan.FromMinutes(6), TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)));

        var restPace = (TimeSpan.FromMinutes(7), TimeSpan.FromMinutes(10));

        // Act
        var workout = WorkoutExtensions
            .CreateRunWalkWorkout(
                easyPace,
                restPace,
                new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2)));

        // Assert
        Assert.Equal(workout.EstimatedTime, TimeSpan.FromMinutes(30));
    }
}
