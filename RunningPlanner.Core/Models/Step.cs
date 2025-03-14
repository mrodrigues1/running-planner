namespace RunningPlanner.Core.Models;

public class Step
{
    private Step()
    {
    }

    public SimpleStep? SimpleStep { get; private set; }
    public Repeat? Repeat { get; private set; }

    public class StepBuilder
    {
        private readonly Step _step;

        private StepBuilder()
        {
            _step = new Step();
        }

        public StepBuilder WithSimpleStep(SimpleStep simpleStep)
        {
            _step.SimpleStep = simpleStep;

            return this;
        }

        public StepBuilder WithRepeat(Repeat repeat)
        {
            _step.Repeat = repeat;

            return this;
        }

        public Step Build()
        {
            if (_step.SimpleStep is not null && _step.Repeat is not null)
            {
                throw new ArgumentException("Step cannot contain both simple step and repeat.");
            }

            if (_step.SimpleStep is null && _step.Repeat is null)
            {
                throw new ArgumentException("Step must contain either simple step or repeat.");
            }

            return _step;
        }

        public static StepBuilder CreateBuilder() => new();
    }
}