namespace RunningPlanner.Core.Models;

/// <summary>
/// Represents a sequence of steps that should be executed multiple times.
/// </summary>
public class Repeat
{
    public int RepetitionCount { get; private set; }

    /// <summary>
    /// Gets the collection of steps to be repeated.
    /// </summary>
    public IReadOnlyList<SimpleStep> RepeatableSteps => _steps.AsReadOnly();

    private readonly List<SimpleStep> _steps;


    private Repeat()
    {
        _steps = new List<SimpleStep>();
    }

    /// <summary>
    /// Validates that the repeat configuration is valid.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
    private void Validate()
    {
        if (RepetitionCount < 1)
        {
            throw new ArgumentException("Repeat count must be at least 1.");
        }

        if (_steps.Count < 1)
        {
            throw new ArgumentException("Repeat must contain at least one step.");
        }
    }

    public class RepeatBuilder
    {
        private readonly Repeat _repeat;

        private RepeatBuilder()
        {
            _repeat = new Repeat();
        }

        public RepeatBuilder WithCount(int count)
        {
            _repeat.RepetitionCount = count;

            return this;
        }

        /// <summary>
        /// Adds a step to be repeated.
        /// </summary>
        /// <param name="step">The step to add.</param>
        /// <returns>The builder instance for method chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when step is null.</exception>
        public RepeatBuilder WithStep(SimpleStep step)
        {
            ArgumentNullException.ThrowIfNull(step);

            _repeat._steps.Add(step);

            return this;
        }


        /// <summary>
        /// Builds and validates the Repeat instance.
        /// </summary>
        /// <returns>A configured Repeat instance.</returns>
        /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
        public Repeat Build()
        {
            _repeat.Validate();

            return _repeat;
        }


        public static RepeatBuilder CreateBuilder() => new();
    }
}
