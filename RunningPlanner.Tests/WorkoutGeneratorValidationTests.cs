using RunningPlanner.Core.Exceptions;
using RunningPlanner.Core.Models;
using RunningPlanner.Core.Models.Paces;
using RunningPlanner.Core.WorkoutStrategies;

namespace RunningPlanner.Tests;

public class WorkoutGeneratorValidationTests
{
    private readonly WorkoutGenerator _workoutGenerator = new();
    
    private static WorkoutParameters CreateValidParameters() => new(
        Phase: TrainingPhase.Base,
        ExperienceLevel: ExperienceLevel.Intermediate,
        Paces: new TrainingPaces(50, "5:00-5:30", "4:30", "4:15", "3:55", "3:40"),
        WeekNumber: 1,
        PhaseWeekNumber: 1,
        TotalDistance: 10.0m
    );

    [Fact]
    public void GenerateWorkout_ValidParameters_GeneratesWorkout()
    {
        // Arrange
        var parameters = CreateValidParameters();

        // Act
        var result = _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(WorkoutType.EasyRun, result.Type);
    }

    [Fact]
    public void GenerateWorkout_InvalidWorkoutType_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters();
        var invalidType = (WorkoutType)999;

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(invalidType, parameters));
        
        Assert.Equal(invalidType, exception.WorkoutType);
        Assert.Contains("Invalid workout type", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_InvalidWorkoutType_ThrowsWorkoutGenerationException2()
    {
        // Arrange
        var parameters = CreateValidParameters();

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.Invalid, parameters));
        
        Assert.Equal(WorkoutType.Invalid, exception.WorkoutType);
        Assert.Contains("Cannot generate workout for 'Invalid' workout type", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_UnsupportedWorkoutType_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters();

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.Cross, parameters)); // Cross training not supported
        
        Assert.Equal(WorkoutType.Cross, exception.WorkoutType);
        Assert.Contains("No strategy found for workout type", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_NullParameters_ThrowsWorkoutGenerationException()
    {
        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, null!));
        
        Assert.Contains("WorkoutParameters cannot be null", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_InvalidTrainingPhase_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters() with { Phase = (TrainingPhase)999 };

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters));
        
        Assert.Contains("Invalid training phase", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_InvalidExperienceLevel_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters() with { ExperienceLevel = (ExperienceLevel)999 };

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters));
        
        Assert.Contains("Invalid experience level", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_NullPaces_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters() with { Paces = null! };

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters));
        
        Assert.Contains("TrainingPaces cannot be null", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_ZeroWeekNumber_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters() with { WeekNumber = 0 };

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters));
        
        Assert.Contains("WeekNumber must be positive", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_NegativeWeekNumber_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters() with { WeekNumber = -1 };

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters));
        
        Assert.Contains("WeekNumber must be positive", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_ZeroPhaseWeekNumber_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters() with { PhaseWeekNumber = 0 };

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters));
        
        Assert.Contains("PhaseWeekNumber must be positive", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_ZeroTotalDistance_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters() with { TotalDistance = 0 };

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters));
        
        Assert.Contains("TotalDistance must be positive", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_NegativeTotalDistance_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters() with { TotalDistance = -5.0m };

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters));
        
        Assert.Contains("TotalDistance must be positive", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_NegativeRestDistance_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters() with { RestBeforeStartIntervalDistance = -1.0m };

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters));
        
        Assert.Contains("RestBeforeStartIntervalDistance cannot be negative", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_RestDistanceExceedsTotalDistance_ThrowsWorkoutGenerationException()
    {
        // Arrange
        var parameters = CreateValidParameters() with 
        { 
            TotalDistance = 10.0m,
            RestBeforeStartIntervalDistance = 15.0m // Greater than total distance
        };

        // Act & Assert
        var exception = Assert.Throws<WorkoutGenerationException>(() => 
            _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters));
        
        Assert.Contains("RestBeforeStartIntervalDistance", exception.Message);
        Assert.Contains("cannot exceed TotalDistance", exception.Message);
    }

    [Fact]
    public void GenerateWorkout_ValidRestDistance_GeneratesWorkout()
    {
        // Arrange
        var parameters = CreateValidParameters() with 
        { 
            TotalDistance = 10.0m,
            RestBeforeStartIntervalDistance = 2.0m // Valid rest distance
        };

        // Act
        var result = _workoutGenerator.GenerateWorkout(WorkoutType.EasyRun, parameters);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(WorkoutType.EasyRun, result.Type);
    }

    [Theory]
    [InlineData(WorkoutType.EasyRun)]
    [InlineData(WorkoutType.LongRun)]
    [InlineData(WorkoutType.TempoRun)]
    [InlineData(WorkoutType.Intervals)]
    [InlineData(WorkoutType.HillRepeat)]
    [InlineData(WorkoutType.RacePace)]
    [InlineData(WorkoutType.EasyRunWithStrides)]
    [InlineData(WorkoutType.Race)]
    public void GenerateWorkout_SupportedWorkoutTypes_GeneratesWorkout(WorkoutType workoutType)
    {
        // Arrange
        var parameters = CreateValidParameters();

        // Act
        var result = _workoutGenerator.GenerateWorkout(workoutType, parameters);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(workoutType, result.Type);
    }
}