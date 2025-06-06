using RunningPlanner.Core.Exceptions;
using RunningPlanner.Core.Models;
using RunningPlanner.Core.PlanGenerator.Marathon;
using RunningPlanner.Core.WorkoutStrategies;

namespace RunningPlanner.Tests;

public class CustomExceptionsTests
{
    [Fact]
    public void MarathonPlanGenerator_InvalidPlanWeeks_ThrowsInvalidTrainingPlanParametersException()
    {
        // Arrange
        var parameters = new MarathonPlanGeneratorParameters
        {
            RaceDate = DateTime.Now.AddDays(5 * 7), // Only 5 weeks (too short)
            RaceDistance = 42.2m,
            GoalTime = TimeSpan.FromHours(4),
            WeeklyRunningDays = [DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Sunday],
            CurrentWeeklyMileage = 30m,
            PeakWeeklyMileage = 60m,
            RunnerLevel = ExperienceLevel.Beginner
        };

        var generator = new MarathonPlanGenerator(parameters);

        // Act & Assert
        var exception = Assert.Throws<InvalidTrainingPlanParametersException>(() => generator.Generate());
        
        Assert.Equal(nameof(parameters.PlanWeeks), exception.ParameterName);
        Assert.Equal(6, exception.ProvidedValue);
        Assert.Contains("Plan weeks must be between", exception.Message);
    }

    [Fact]
    public void MarathonPlanGenerator_TooFewRunningDays_ThrowsInvalidTrainingPlanParametersException()
    {
        // Arrange
        var parameters = new MarathonPlanGeneratorParameters
        {
            RaceDate = DateTime.Now.AddDays(16 * 7),
            RaceDistance = 42.2m,
            GoalTime = TimeSpan.FromHours(4),
            WeeklyRunningDays = [DayOfWeek.Monday, DayOfWeek.Wednesday], // Only 2 days (too few)
            CurrentWeeklyMileage = 30m,
            PeakWeeklyMileage = 60m,
            RunnerLevel = ExperienceLevel.Beginner
        };

        var generator = new MarathonPlanGenerator(parameters);

        // Act & Assert
        var exception = Assert.Throws<InvalidTrainingPlanParametersException>(() => generator.Generate());
        
        Assert.Equal(nameof(parameters.WeeklyRunningDays), exception.ParameterName);
        Assert.Equal(2, exception.ProvidedValue);
        Assert.Contains("At least 4 weekly running days must be specified", exception.Message);
    }

    [Fact]
    public void MarathonPlanGenerator_TooManyQualityWorkoutDays_ThrowsInvalidTrainingPlanParametersException()
    {
        // Arrange
        var parameters = new MarathonPlanGeneratorParameters
        {
            RaceDate = DateTime.Now.AddDays(16 * 7),
            RaceDistance = 42.2m,
            GoalTime = TimeSpan.FromHours(4),
            WeeklyRunningDays = [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Sunday],
            QualityWorkoutDays = [DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday], // 4 days (too many)
            CurrentWeeklyMileage = 30m,
            PeakWeeklyMileage = 60m,
            RunnerLevel = ExperienceLevel.Beginner
        };

        var generator = new MarathonPlanGenerator(parameters);

        // Act & Assert
        var exception = Assert.Throws<InvalidTrainingPlanParametersException>(() => generator.Generate());
        
        Assert.Equal(nameof(parameters.QualityWorkoutDays), exception.ParameterName);
        Assert.Equal(4, exception.ProvidedValue);
        Assert.Contains("At most 3 quality workout days can be specified", exception.Message);
    }

    [Fact]
    public void VdotCalculationException_HasCorrectProperties()
    {
        // Arrange
        var distance = 5m;
        var time = TimeSpan.FromMinutes(20);
        var message = "Test message";

        // Act
        var exception = new VdotCalculationException(distance, time, message);

        // Assert
        Assert.Equal(distance, exception.Distance);
        Assert.Equal(time, exception.Time);
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void WorkoutGenerationException_HasCorrectProperties()
    {
        // Arrange
        var workoutType = WorkoutType.Intervals;
        var message = "Test workout generation error";

        // Act
        var exception = new WorkoutGenerationException(workoutType, message);

        // Assert
        Assert.Equal(workoutType, exception.WorkoutType);
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void WorkoutGenerator_Integration_ThrowsCustomException()
    {
        // Arrange
        var generator = new WorkoutGenerator();

        // Act & Assert - Test that our WorkoutGenerator properly throws custom exceptions
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            generator.GenerateWorkout(WorkoutType.EasyRun, null!));
        
        Assert.Contains("WorkoutParameters cannot be null", exception.Message);
    }
}