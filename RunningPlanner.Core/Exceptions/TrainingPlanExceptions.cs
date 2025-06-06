using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Exceptions;

/// <summary>
/// Base exception for all training plan related errors.
/// </summary>
public abstract class TrainingPlanException : Exception
{
    protected TrainingPlanException(string message) : base(message)
    {
    }

    protected TrainingPlanException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when training plan parameters are invalid.
/// </summary>
public class InvalidTrainingPlanParametersException : TrainingPlanException
{
    public string ParameterName { get; }
    public object? ProvidedValue { get; }

    public InvalidTrainingPlanParametersException(string parameterName, object? providedValue, string message) 
        : base(message)
    {
        ParameterName = parameterName;
        ProvidedValue = providedValue;
    }

    public InvalidTrainingPlanParametersException(string parameterName, object? providedValue, string message, Exception innerException) 
        : base(message, innerException)
    {
        ParameterName = parameterName;
        ProvidedValue = providedValue;
    }
}

/// <summary>
/// Exception thrown when workout generation fails.
/// </summary>
public class WorkoutGenerationException : TrainingPlanException
{
    public WorkoutType? WorkoutType { get; }

    public WorkoutGenerationException(string message) : base(message)
    {
    }

    public WorkoutGenerationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public WorkoutGenerationException(WorkoutType workoutType, string message) : base(message)
    {
        WorkoutType = workoutType;
    }

    public WorkoutGenerationException(WorkoutType workoutType, string message, Exception innerException) 
        : base(message, innerException)
    {
        WorkoutType = workoutType;
    }
}

/// <summary>
/// Exception thrown when VDOT calculations fail.
/// </summary>
public class VdotCalculationException : TrainingPlanException
{
    public decimal? Distance { get; }
    public TimeSpan? Time { get; }

    public VdotCalculationException(string message) : base(message)
    {
    }

    public VdotCalculationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public VdotCalculationException(decimal distance, TimeSpan time, string message) : base(message)
    {
        Distance = distance;
        Time = time;
    }

    public VdotCalculationException(decimal distance, TimeSpan time, string message, Exception innerException) 
        : base(message, innerException)
    {
        Distance = distance;
        Time = time;
    }
}