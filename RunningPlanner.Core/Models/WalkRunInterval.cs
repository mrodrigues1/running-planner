namespace RunningPlanner.Core.Models;

public record WalkRunInterval(
    int RepeatCount,
    TimeSpan RunDuration,
    TimeSpan WalkDuration,
    TimeSpan ContinuousEasyDuration = default,
    TimeSpan FinalWalkDuration = default);
