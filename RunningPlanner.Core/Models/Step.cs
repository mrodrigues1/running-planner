namespace RunningPlanner.Core.Models;


/// <summary>
/// Base class for workout steps.
/// </summary>
public abstract record Step
{
    // Common properties or methods that apply to all steps could go here
}


// /// <summary>
// /// Represents a workout step that can either be a simple step or a repeated sequence.
// /// </summary>
// public record Step
// {
//     /// <summary>
//     /// Gets the simple step if this is a single-step configuration.
//     /// </summary>
//     public SimpleStep? SimpleStep { get; }
//
//     /// <summary>
//     /// Gets the repeat if this is a repeating-step configuration.
//     /// </summary>
//     public Repeat? Repeat { get; }
//
//     private Step(SimpleStep? simpleStep, Repeat? repeat)
//     {
//         if (simpleStep is not null && repeat is not null)
//         {
//             throw new ArgumentException("Step cannot contain both simple step and repeat.");
//         }
//
//         if (simpleStep is null && repeat is null)
//         {
//             throw new ArgumentException("Step must contain either simple step or repeat.");
//         }
//
//         SimpleStep = simpleStep;
//         Repeat = repeat;
//     }
//
//     /// <summary>
//     /// Creates a step containing a simple step.
//     /// </summary>
//     /// <param name="simpleStep">The simple step to include.</param>
//     /// <returns>A new Step instance.</returns>
//     /// <exception cref="ArgumentNullException">Thrown when simpleStep is null.</exception>
//     public static Step FromSimpleStep(SimpleStep simpleStep)
//     {
//         ArgumentNullException.ThrowIfNull(simpleStep);
//
//         return new Step(simpleStep, null);
//     }
//
//     /// <summary>
//     /// Creates a step containing a repeat.
//     /// </summary>
//     /// <param name="repeat">The repeat to include.</param>
//     /// <returns>A new Step instance.</returns>
//     /// <exception cref="ArgumentNullException">Thrown when repeat is null.</exception>
//     public static Step FromRepeat(Repeat repeat)
//     {
//         ArgumentNullException.ThrowIfNull(repeat);
//
//         return new Step(null, repeat);
//     }
//     
//     public static Step FromRepeat(int count, params SimpleStep[] steps)
//     {
//         return FromRepeat(Repeat.Create(count, steps));
//     }
//
// }
