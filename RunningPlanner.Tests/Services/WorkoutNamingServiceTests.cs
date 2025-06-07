using RunningPlanner.Core.Models;
using RunningPlanner.Core.Services.WorkoutNaming;

namespace RunningPlanner.Tests.Services;

public class WorkoutNamingServiceTests
{
    private readonly IWorkoutNamingService _namingService = new WorkoutNamingService();

    [Fact]
    public void GenerateWorkoutName_NullWorkout_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _namingService.GenerateWorkoutName(null!));
    }

    [Fact]
    public void GenerateWorkoutName_RestWorkout_ReturnsRestDescription()
    {
        // Arrange
        var workout = Workout.Create(WorkoutType.Rest, []);

        // Act
        var name = _namingService.GenerateWorkoutName(workout);

        // Assert
        Assert.Equal("Rest Day", name);
    }

    [Fact]
    public void GenerateWorkoutName_EasyRun_ReturnsCorrectFormat()
    {
        // Arrange
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            10.0m,
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(6)));

        var workout = Workout.Create(WorkoutType.EasyRun, [step]);

        // Act
        var name = _namingService.GenerateWorkoutName(workout);

        // Assert
        Assert.Contains("Easy Run", name);
        Assert.Contains("10 km", name);
        Assert.Contains("@5:00~6:00 min/km", name);
    }

    [Fact]
    public void GenerateWorkoutName_LongRun_ReturnsCorrectFormat()
    {
        // Arrange
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            20.0m,
            IntensityTarget.Pace(
                TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)),
                TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30))));

        var workout = Workout.Create(WorkoutType.LongRun, [step]);

        // Act
        var name = _namingService.GenerateWorkoutName(workout);

        // Assert
        Assert.Contains("Long Run", name);
        Assert.Contains("20 km", name);
        Assert.Contains("@5:30~6:30 min/km", name);
    }

    [Fact]
    public void GenerateWorkoutName_TempoRun_ReturnsCorrectFormat()
    {
        // Arrange
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            8.0m,
            IntensityTarget.Pace(
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(15)),
                TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30))));

        var workout = Workout.Create(WorkoutType.TempoRun, [step]);

        // Act
        var name = _namingService.GenerateWorkoutName(workout);

        // Assert
        Assert.Contains("Tempo Run", name);
        Assert.Contains("8 km", name);
        Assert.Contains("8km@4:15~4:30 min/km", name);
    }

    [Fact]
    public void GenerateWorkoutName_Intervals_ReturnsCorrectFormat()
    {
        // Arrange
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

        var steps = new List<Step>();

        // Add 6 intervals (6 x run + 6 x recover = 12 steps total)
        for (int i = 0; i < 6; i++)
        {
            steps.Add(runStep);
            steps.Add(recoverStep);
        }

        var workout = Workout.Create(WorkoutType.Intervals, steps);

        // Act
        var name = _namingService.GenerateWorkoutName(workout);

        // Assert
        Assert.Contains("Intervals", name);
        Assert.Contains("6 x (400m@3:45~4:00 min/km + 200m Recover)", name);
    }

    [Fact]
    public void GenerateWorkoutName_HillRepeats_ReturnsCorrectFormat()
    {
        // Arrange
        var runStep = SimpleStep.CreateWithKilometers(
            StepType.Run,
            0.2m, // 200m hill repeat
            IntensityTarget.Pace(TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(45)), TimeSpan.FromMinutes(5)));

        var recoverStep = SimpleStep.CreateWithKilometers(
            StepType.Walk,
            0.2m, // 200m walk recovery
            IntensityTarget.Pace(TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)), TimeSpan.FromMinutes(6)));

        var steps = new List<Step>();

        // Add 8 hill repeats
        for (int i = 0; i < 8; i++)
        {
            steps.Add(runStep);
            steps.Add(recoverStep);
        }

        var workout = Workout.Create(WorkoutType.HillRepeat, steps);

        // Act
        var name = _namingService.GenerateWorkoutName(workout);

        // Assert
        Assert.Contains("Hill Repeat", name);
        Assert.Contains("8 x (200m", name);
        Assert.Contains("+ 200m Walk)", name);
    }

    [Fact]
    public void GenerateWorkoutName_EasyRunWithStrides_ReturnsCorrectFormat()
    {
        // Arrange
        var easyRunStep = SimpleStep.CreateWithKilometers(
            StepType.Run,
            8.0m,
            IntensityTarget.Pace(TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)), TimeSpan.FromMinutes(6)));

        var strideStep = SimpleStep.CreateWithKilometers(
            StepType.Run,
            0.1m, // 100m stride
            IntensityTarget.Pace(TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(45)), TimeSpan.FromMinutes(5)));

        var recoverStep = SimpleStep.CreateWithKilometers(
            StepType.Walk,
            0.1m, // 100m walk
            IntensityTarget.Pace(TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)), TimeSpan.FromMinutes(6)));

        var steps = new List<Step> {easyRunStep};

        // Add 4 strides
        for (int i = 0; i < 4; i++)
        {
            steps.Add(strideStep);
            steps.Add(recoverStep);
        }

        var workout = Workout.Create(WorkoutType.EasyRunWithStrides, steps);

        // Act
        var name = _namingService.GenerateWorkoutName(workout);

        // Assert
        Assert.Contains("Easy Run w/ Strides", name);
        Assert.Contains("4 x (100m", name);
        Assert.Contains("+ 100m Walk)", name);
    }

    [Theory]
    [InlineData(WorkoutType.Cross)]
    [InlineData(WorkoutType.Fartlek)]
    [InlineData(WorkoutType.Race)]
    public void GenerateWorkoutName_UnsupportedWorkoutTypes_ReturnsDefaultFormat(WorkoutType workoutType)
    {
        // Arrange
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            5.0m,
            IntensityTarget.Pace(TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(45)), TimeSpan.FromMinutes(6)));

        var workout = Workout.Create(workoutType, [step]);

        // Act
        var name = _namingService.GenerateWorkoutName(workout);

        // Assert
        Assert.Contains(workoutType.ToString(), name); // Should contain the workout type
        Assert.Contains("5 km", name); // Should contain the distance
    }

    [Fact]
    public void GenerateWorkoutName_Consistency_ReturnsIdenticalResults()
    {
        // Arrange
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            10.0m,
            IntensityTarget.Pace(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(6)));

        var workout = Workout.Create(WorkoutType.EasyRun, [step]);

        // Act
        var name1 = _namingService.GenerateWorkoutName(workout);
        var name2 = _namingService.GenerateWorkoutName(workout);
        var name3 = _namingService.GenerateWorkoutName(workout);

        // Assert
        Assert.Equal(name1, name2);
        Assert.Equal(name2, name3);
    }
}
