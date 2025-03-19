namespace RunningPlanner.Core.Models;

/// <summary>
/// Represents a sequence of steps that should be executed multiple times.
/// </summary>
public record Repeat
{
    /// <summary>
    /// Gets the number of times the steps should be repeated.
    /// </summary>
    public int RepetitionCount { get; }

    /// <summary>
    /// Gets the collection of steps to be repeated.
    /// </summary>
    public IReadOnlyList<SimpleStep> RepeatableSteps { get; }

    private Repeat(int repetitionCount, IEnumerable<SimpleStep> steps)
    {
        if (repetitionCount < 1)
        {
            throw new ArgumentException("Repeat count must be at least 1.");
        }

        var stepsList = steps?.ToList() ?? new List<SimpleStep>();

        if (stepsList.Count < 1)
        {
            throw new ArgumentException("Repeat must contain at least one step.");
        }

        // Ensure no null steps
        if (stepsList.Any(step => step == null))
        {
            throw new ArgumentNullException(nameof(steps), "Steps collection cannot contain null elements.");
        }

        RepetitionCount = repetitionCount;
        RepeatableSteps = stepsList.AsReadOnly();
    }

    /// <summary>
    /// Creates a new Repeat with the specified count and steps.
    /// </summary>
    /// <param name="count">The number of times to repeat the steps.</param>
    /// <param name="steps">The steps to repeat.</param>
    /// <returns>A new Repeat instance.</returns>
    public static Repeat Create(int count, params SimpleStep[] steps) =>
        new(count, steps);

    /// <summary>
    /// Creates a new Repeat with the specified count and collection of steps.
    /// </summary>
    /// <param name="count">The number of times to repeat the steps.</param>
    /// <param name="steps">The steps to repeat.</param>
    /// <returns>A new Repeat instance.</returns>
    public static Repeat Create(int count, IEnumerable<SimpleStep> steps) =>
        new(count, steps);
}
