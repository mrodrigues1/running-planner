namespace RunningPlanner.Core.Models;

public class WorkoutBuilder
{
    private readonly List<Step> _steps = new();
    private WorkoutType _type = WorkoutType.Invalid;

    public WorkoutBuilder WithType(WorkoutType type)
    {
        _type = type;

        return this;
    }

    public WorkoutBuilder AddStep(SimpleStep step)
    {
        _steps.Add(step);

        return this;
    }

    public WorkoutBuilder AddRepeat(int count, params SimpleStep[] steps)
    {
        _steps.AddRange(steps);

        return this;
    }
    
    public WorkoutBuilder AddRepeat(int count, Repeat repeat)
    {
        _steps.Add(repeat);

        return this;
    }

    private WorkoutBuilder()
    {
    }
    
    public static WorkoutBuilder CreateBuilder() => new();
}
