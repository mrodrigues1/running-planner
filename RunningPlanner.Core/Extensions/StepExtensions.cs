using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Extensions;

public static class StepExtensions
{
    /// <summary>
    /// Calculates the total distance for a collection of steps.
    /// </summary>
    /// <param name="steps">An enumerable collection of <see cref="SimpleStep"/> objects.</param>
    /// <returns>The total distance of all steps in the collection as a <see cref="decimal"/>. If the collection is null, returns 0.</returns>
    public static decimal StepsDistance(this IEnumerable<SimpleStep> steps)
    {
        return steps?.Sum(x => x.TotalDistance.DistanceValue) ?? 0;
    }
}