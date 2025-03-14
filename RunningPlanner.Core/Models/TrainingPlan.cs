namespace RunningPlanner.Core.Models;

public class TrainingPlan
{
    private TrainingPlan()
    {
    }

    public List<TrainingWeek> TrainingWeeks { get; private set; }

    public class TrainingPlanBuilder
    {
        private readonly TrainingPlan _trainingPlan;

        private TrainingPlanBuilder()
        {
            _trainingPlan = new TrainingPlan
            {
                TrainingWeeks = []
            };
        }

        public TrainingPlanBuilder WithTrainingWeek(TrainingWeek trainingWeek)
        {
            _trainingPlan.TrainingWeeks.Add(trainingWeek);

            return this;
        }

        public TrainingPlan Build()
        {
            if (_trainingPlan.TrainingWeeks.Count < 1)
            {
                throw new ArgumentException("Training plan must contain at least one week.");
            }

            return _trainingPlan;
        }

        public static TrainingPlanBuilder CreateBuilder() => new();
    }
}