﻿using RunningPlanner.Core.Models;

namespace RunningPlanner.Tests;

public class WorkoutTests
{
    [Fact]
    public void Workout_WhenCreatedWithRepeats_ThenTotalDistanceIs8_7Km()
    {
        // Arrange
        var easyPace = (TimeSpan.FromMinutes(6), TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)));

        var intervalPace = (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(15)),
            TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(35)));

        var restPace = (TimeSpan.FromMinutes(7), TimeSpan.FromMinutes(10));

        // Act
        var workout = Workout.CreateInterval(
            3,
            1000,
            400,
            8.7m,
            easyPace,
            intervalPace,
            restPace,
            2m,
            2m,
            0.5m);

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
        var workout = Workout
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
        var workout = Workout
            .CreateRunWalkWorkout(
                easyPace,
                restPace,
                new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
                new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2)));

        // Assert
        Assert.Equal(workout.EstimatedTime, TimeSpan.FromMinutes(30));
    }
}
