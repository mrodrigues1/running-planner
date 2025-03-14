namespace RunningPlanner.Core.Models;

public class Repeat
{
    private Repeat()
    {
    }

    public int Count { get; private set; }
    public List<SimpleStep> Steps { get; private set; }

    public class RepeatBuilder
    {
        private readonly Repeat _repeat;

        private RepeatBuilder()
        {
            _repeat = new Repeat
            {
                Steps = []
            };
        }

        public RepeatBuilder WithCount(int count)
        {
            _repeat.Count = count;

            return this;
        }

        public RepeatBuilder WithStep(SimpleStep step)
        {
            _repeat.Steps.Add(step);

            return this;
        }

        public Repeat Build()
        {
            if (_repeat.Count < 1)
            {
                throw new ArgumentException("Repeat count must be greater than 1.");
            }

            if (_repeat.Steps.Count < 1)
            {
                throw new ArgumentException("Repeat must contain at least one step.");
            }

            return _repeat;
        }

        public static RepeatBuilder CreateBuilder() => new();
    }
}