namespace RunningPlanner.Core.Models;

/// <summary>
/// Represents a complete training plan consisting of multiple training weeks.
/// </summary>
public record TrainingPlan
{
    /// <summary>
    /// Gets the list of training weeks in this plan.
    /// </summary>
    public IReadOnlyList<TrainingWeek> TrainingWeeks { get; }

    private TrainingPlan(IEnumerable<TrainingWeek> trainingWeeks)
    {
        var weeks = trainingWeeks?.ToList() ?? new List<TrainingWeek>();

        if (weeks.Count < 1)
        {
            throw new ArgumentException("Training plan must contain at least one week.");
        }

        // Ensure no null weeks
        if (weeks.Any(week => week == null))
        {
            throw new ArgumentNullException(
                nameof(trainingWeeks),
                "Training weeks collection cannot contain null elements.");
        }

        TrainingWeeks = weeks.AsReadOnly();
    }

    /// <summary>
    /// Creates a new training plan with the specified weeks.
    /// </summary>
    /// <param name="weeks">The training weeks to include in the plan.</param>
    /// <returns>A new TrainingPlan instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the collection is empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the collection contains null elements.</exception>
    public static TrainingPlan Create(params TrainingWeek[] weeks) =>
        new(weeks);

    /// <summary>
    /// Creates a new training plan with the specified collection of weeks.
    /// </summary>
    /// <param name="weeks">The training weeks to include in the plan.</param>
    /// <returns>A new TrainingPlan instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the collection is empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the collection contains null elements.</exception>
    public static TrainingPlan Create(IEnumerable<TrainingWeek> weeks) =>
        new(weeks);
}
