namespace RunningPlanner.Core.Models;

/// <summary>
/// Represents a sequence of steps that should be executed multiple times.
/// </summary>
public sealed record RepeatSample : Step
{
    /// <summary>
    /// Gets the number of times the steps should be SampleRepeated.
    /// </summary>
    public int RepetitionCount { get; }

    /// <summary>
    /// Gets the collection of steps to be SampleRepeated.
    /// </summary>
    public IReadOnlyList<SimpleStepSample> Steps { get; }

    private RepeatSample(int repetitionCount, IEnumerable<SimpleStepSample> steps)
    {
        if (repetitionCount < 1)
        {
            throw new ArgumentException("SampleRepeat count must be at least 1.");
        }

        var stepsList = steps?.ToList() ?? new List<SimpleStepSample>();

        if (stepsList.Count < 1)
        {
            throw new ArgumentException("SampleRepeat must contain at least one step.");
        }

        if (stepsList.Any(step => step == null))
        {
            throw new ArgumentNullException(nameof(steps), "Steps collection cannot contain null elements.");
        }

        RepetitionCount = repetitionCount;
        Steps = stepsList.AsReadOnly();
    }

    /// <summary>
    /// Creates a new SampleRepeatStep with the specified count and steps.
    /// </summary>
    public static RepeatSample Create(int count, params SimpleStepSample[] steps) =>
        new(count, steps);

    /// <summary>
    /// Creates a new SampleRepeatStep with the specified count and collection of steps.
    /// </summary>
    public static RepeatSample Create(int count, IEnumerable<SimpleStepSample> steps) =>
        new(count, steps);
}
