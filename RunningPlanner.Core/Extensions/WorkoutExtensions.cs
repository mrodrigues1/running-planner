using RunningPlanner.Core.Models;

namespace RunningPlanner.Core.Extensions;

public static class WorkoutExtensions
{
    /// <summary>
    /// Creates a rest workout with no steps and sets the type to rest.
    /// </summary>
    /// <returns>A rest workout object with the type set as Rest.</returns>
    public static Workout CreateRestWorkout()
    {
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.Rest)
            .BuildRestWorkout();
    }

    /// <summary>
    /// Creates a race workout with specified distance and pace range.
    /// </summary>
    /// <param name="distance">The total distance of the race workout in kilometers.</param>
    /// <param name="paceRange">The minimum and maximum pace range for the race workout.</param>
    /// <returns>A race workout object with the specified distance and pace range.</returns>
    public static Workout CreateRaceWorkout(decimal distance, (TimeSpan min, TimeSpan max) paceRange)
    {
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.Race)
            .WithSimpleRunStep(distance, paceRange)
            .BuildSimpleWorkout();
    }

    /// <summary>
    /// Creates an easy run workout with a given distance and pace range.
    /// </summary>
    /// <param name="distance">The distance of the easy run.</param>
    /// <param name="paceRange">The pace range for the run, represented as a tuple of minimum and maximum times.</param>
    /// <returns>An easy run workout object with the specified distance and pace range.</returns>
    public static Workout CreateEasyRunWorkout(decimal distance, (TimeSpan min, TimeSpan max) paceRange)
    {
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.EasyRun)
            .WithSimpleRunStep(distance, paceRange)
            .BuildSimpleWorkout();
    }

    /// <summary>
    /// Creates a medium run workout with a specified distance and pace range.
    /// </summary>
    /// <param name="distance">The distance of the medium run in kilometers.</param>
    /// <param name="paceRange">The range of pace, with minimum and maximum times per kilometer.</param>
    /// <returns>A workout object representing a medium run.</returns>
    public static Workout CreateMediumRunWorkout(decimal distance, (TimeSpan min, TimeSpan max) paceRange)
    {
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.MediumRun)
            .WithSimpleRunStep(distance, paceRange)
            .BuildSimpleWorkout();
    }

    /// <summary>
    /// Creates a long run workout and sets the type to LongRun.
    /// </summary>
    /// <param name="distance">The total distance of the long run in kilometers.</param>
    /// <param name="paceRange">The pace range for the long run, specified as a minimum and maximum time per kilometer.</param>
    /// <returns>A long run workout object with the specified distance and pace range.</returns>
    public static Workout CreateLongRunWorkout(decimal distance, (TimeSpan min, TimeSpan max) paceRange)
    {
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.LongRun)
            .WithSimpleRunStep(distance, paceRange)
            .BuildSimpleWorkout();
    }

    /// <summary>
    /// Creates a race pace workout with a specified distance and pace range.
    /// </summary>
    /// <param name="distance">The distance of the workout in kilometers.</param>
    /// <param name="paceRange">The target pace range for the workout, defined by a minimum and maximum time span.</param>
    /// <returns>A race pace workout object with the specified parameters.</returns>
    public static Workout CreateRacePaceWorkout(decimal distance, (TimeSpan min, TimeSpan max) paceRange)
    {
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.RacePace)
            .WithSimpleRunStep(distance, paceRange)
            .BuildSimpleWorkout();
    }

    /// <summary>
    /// Creates a hill workout consisting of repeats, warmup, and cooldown based on the specified parameters.
    /// </summary>
    /// <param name="repeats">Number of hill repeats in the workout.</param>
    /// <param name="totalDistance">Total workout distance in kilometers.</param>
    /// <param name="easyPaceRange">The pace range for the easy running segments during warmup, cooldown, and additional distance.</param>
    /// <param name="hillPaceRange">The pace range for the uphill portions of the hill repeats.</param>
    /// <param name="recoveryPaceRange">The pace range for the recovery portions (downhill) of the hill repeats.</param>
    /// <param name="warmupDistance">Distance for the warmup in kilometers (default is 2.0).</param>
    /// <param name="cooldownDistance">Distance for the cooldown in kilometers (default is 2.0).</param>
    /// <returns>A hill workout object containing the specified warmup, hill repeats, cooldown, and any necessary additional distance.</returns>
    public static Workout CreateHillWorkout(
        int repeats,
        decimal totalDistance,
        (TimeSpan min, TimeSpan max) easyPaceRange,
        (TimeSpan min, TimeSpan max) hillPaceRange,
        (TimeSpan min, TimeSpan max) recoveryPaceRange,
        decimal warmupDistance = 2.0m,
        decimal cooldownDistance = 2.0m)
    {
        // For hill workouts:
        // - Warmup as specified (default 2km)
        // - Repeats
        // - Cooldown as specified (default 2km)
        decimal remainingDistance = totalDistance - (warmupDistance + cooldownDistance);

        // Each hill repeat is approximately:
        decimal hillUpDistance = 0.4m; // ~400m uphill
        decimal hillDownDistance = 0.4m; // ~400m recovery (jog down)
        decimal totalRepeatDistance = repeats * (hillUpDistance + hillDownDistance);

        // If there's additional distance to cover, we'll add it as an easy run
        decimal additionalEasyDistance = Math.Max(0, remainingDistance - totalRepeatDistance);

        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.HillRepeat)
            .WithSimpleStep(StepType.WarmUp, warmupDistance, easyPaceRange)
            .WithRepeatStep(
                repeats,
                hillUpDistance,
                hillDownDistance,
                hillPaceRange,
                recoveryPaceRange)
            .WithSimpleStep(StepType.CoolDown, cooldownDistance + additionalEasyDistance, easyPaceRange)
            .Build();
    }

    /// <summary>
    /// Creates a tempo workout consisting of a warmup, a tempo section, a cooldown, and any necessary additional easy running distance based on the specified parameters.
    /// </summary>
    /// <param name="tempoMinutes">The duration of the tempo section in minutes.</param>
    /// <param name="totalDistance">The total distance of the workout in kilometers.</param>
    /// <param name="easyPaceRange">The pace range for the easy running segments during warmup, cooldown, and additional distance.</param>
    /// <param name="tempoPaceRange">The pace range for the tempo section of the workout.</param>
    /// <param name="warmupDistance">The distance for the warmup in kilometers (default is 2.0).</param>
    /// <param name="cooldownDistance">The distance for the cooldown in kilometers (default is 2.0).</param>
    /// <returns>A tempo workout object containing the specified warmup, tempo section, cooldown, and any necessary additional easy running distance.</returns>
    public static Workout CreateTempoWorkout(
        int tempoMinutes,
        decimal totalDistance,
        (TimeSpan min, TimeSpan max) easyPaceRange,
        (TimeSpan min, TimeSpan max) tempoPaceRange,
        decimal warmupDistance = 2.0m,
        decimal cooldownDistance = 2.0m)
    {
        // For tempo workouts:
        // - Warmup as specified (default 2km)
        // - Tempo section at faster pace (using the minutes specified)
        // - Cooldown as specified (default 2km)

        var tempoDistance = CalculateDistanceBasedOnDuration(TimeSpan.FromMinutes(tempoMinutes), tempoPaceRange);

        // Additional easy distance if needed to match total
        decimal remainingDistance = totalDistance - (warmupDistance + tempoDistance + cooldownDistance);
        decimal additionalEasyDistance = Math.Max(0, remainingDistance);

        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.TempoRun)
            .WithSimpleStep(StepType.WarmUp, warmupDistance, easyPaceRange)
            .WithSimpleStep(StepType.Run, tempoDistance, tempoPaceRange)
            .WithSimpleStep(StepType.CoolDown, cooldownDistance + additionalEasyDistance, easyPaceRange)
            .Build();
    }

    private static decimal CalculateDistanceBasedOnDuration(TimeSpan duration, (TimeSpan min, TimeSpan max) paceRange)
    {
        // Calculate average tempo pace in ticks per km, then convert to minutes
        decimal avgTempoTicksPerKm = (decimal) (paceRange.min.Ticks + paceRange.max.Ticks) / 2;
        decimal avgMinutesPerKm = avgTempoTicksPerKm / (decimal) TimeSpan.TicksPerMinute;

        decimal minutes = (decimal) duration.Ticks / TimeSpan.TicksPerMinute;

        // Calculate the distance covered during the tempo portion
        decimal distance = minutes / avgMinutesPerKm;

        return distance;
    }

    /// <summary>
    /// Creates an interval workout consisting of repeats, warmup, cooldown, and additional easy distance to match the total specified distance.
    /// </summary>
    /// <param name="repeats">Number of interval repeats in the workout.</param>
    /// <param name="meters">Distance of each interval in meters.</param>
    /// <param name="totalDistance">Total workout distance in kilometers.</param>
    /// <param name="easyPaceRange">The pace range for easy running segments, including warmup, cooldown, and additional distance.</param>
    /// <param name="intervalPaceRange">The pace range for the interval running portions.</param>
    /// <param name="recoveryPaceRange">The pace range for recovery portions following each interval.</param>
    /// <param name="warmupDistance">Distance for the warmup in kilometers (default is 2.0).</param>
    /// <param name="cooldownDistance">Distance for the cooldown in kilometers (default is 2.0).</param>
    /// <returns>An interval workout object containing the specified warmup, interval repeats, cooldown, and additional easy distance as needed.</returns>
    public static Workout CreateIntervalWorkout(
        int repeats,
        int meters,
        decimal totalDistance,
        (TimeSpan min, TimeSpan max) easyPaceRange,
        (TimeSpan min, TimeSpan max) intervalPaceRange,
        (TimeSpan min, TimeSpan max) recoveryPaceRange,
        decimal warmupDistance = 2.0m,
        decimal cooldownDistance = 2.0m)
    {
        // For interval workouts:
        // - Warmup as specified (default 2km)
        // - Interval repeats (with recovery)
        // - Cooldown as specified (default 2km)

        // Convert meters to kilometers for intervals
        decimal intervalDistance = meters / 1000.0m;

        // Recovery distance is often equal to interval distance for many training plans
        decimal recoveryDistance = intervalDistance;

        // Total distance for intervals and recovery
        decimal intervalTotalDistance = repeats * (intervalDistance + recoveryDistance);

        // Additional easy distance if needed to match total
        decimal remainingDistance = totalDistance - (warmupDistance + intervalTotalDistance + cooldownDistance);
        decimal additionalEasyDistance = Math.Max(0, remainingDistance);

        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.Intervals)
            .WithSimpleStep(StepType.WarmUp, warmupDistance, easyPaceRange)
            .WithRepeatStep(
                repeats,
                intervalDistance,
                recoveryDistance,
                intervalPaceRange,
                recoveryPaceRange)
            .WithSimpleStep(StepType.CoolDown, cooldownDistance + additionalEasyDistance, easyPaceRange)
            .Build();
    }

    /// <summary>
    /// Creates a stride workout that incorporates strides with defined pace ranges and recovery segments,
    /// along with warm-up and cool-down phases.
    /// </summary>
    /// <param name="distance">The total distance of the workout in kilometers.</param>
    /// <param name="strideCount">The number of strides to include in the workout.</param>
    /// <param name="easyPaceRange">The pace range for the easy run sections of the workout.</param>
    /// <param name="strideRange">The pace range for the strides.</param>
    /// <param name="recoveryPaceRange">The pace range for the recovery sections between strides.</param>
    /// <returns>A stride workout object with strides integrated into the workout flow.</returns>
    public static Workout CreateStrideWorkout(
        decimal distance,
        int strideCount,
        (TimeSpan min, TimeSpan max) easyPaceRange,
        (TimeSpan min, TimeSpan max) strideRange,
        (TimeSpan min, TimeSpan max) recoveryPaceRange)
    {
        // Create a workout with strides included in the middle or end
        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.EasyRun)
            .WithSimpleStep(StepType.WarmUp, distance * 0.2m, easyPaceRange)
            .WithSimpleStep(StepType.Run, distance * 0.4m, easyPaceRange)
            .WithRepeatStep(
                strideCount, // Number of strides
                0.1m, // About 100m per stride
                0.1m, // About 100m recovery
                strideRange,
                recoveryPaceRange)
            .WithSimpleStep(StepType.CoolDown, distance * 0.2m, easyPaceRange)
            .Build();
    }

    /// <summary>
    /// Creates a threshold workout with specified distances, paces, and warmup/cooldown segments.
    /// </summary>
    /// <param name="thresholdDistance">The distance for the threshold segment.</param>
    /// <param name="totalDistance">The total distance of the workout, including warmup, threshold, and cooldown segments.</param>
    /// <param name="easyPaceRange">The range of pace for the warmup and cooldown segments.</param>
    /// <param name="thresholdPaceRange">The range of pace for the threshold segment.</param>
    /// <returns>A threshold workout object with the configured steps and type.</returns>
    public static Workout CreateThresholdWorkout(
        decimal thresholdDistance,
        decimal totalDistance,
        (TimeSpan min, TimeSpan max) easyPaceRange,
        (TimeSpan min, TimeSpan max) thresholdPaceRange)
    {
        decimal warmupDistance = (totalDistance - thresholdDistance) * 0.5m;
        decimal cooldownDistance = totalDistance - thresholdDistance - warmupDistance;

        return Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.Threshold)
            .WithSimpleStep(StepType.WarmUp, warmupDistance, easyPaceRange)
            .WithSimpleStep(StepType.Run, thresholdDistance, thresholdPaceRange)
            .WithSimpleStep(StepType.CoolDown, cooldownDistance, easyPaceRange)
            .Build();
    }

    /// <summary>
    /// Creates a walk/run workout with specified intervals of running and walking.
    /// Each interval specifies the number of repetitions, run duration, and walk duration.
    /// The run and walk durations are paired with their respective pace ranges to calculate distances.
    /// </summary>
    /// <param name="runWalkIntervals">A list of tuples representing intervals for the workout, where each tuple contains
    /// the number of repetitions, run duration, and walk duration.</param>
    /// <param name="runPaceRange">The minimum and maximum paces for the running segments.</param>
    /// <param name="walkPaceRange">The minimum and maximum paces for the walking segments.</param>
    /// <returns>A workout object of type WalkRun configured with the specified intervals, paces, and steps.</returns>
    public static Workout CreateWalkRunWorkout(
        List<WalkRunInterval> runWalkIntervals,
        (TimeSpan min, TimeSpan max) runPaceRange,
        (TimeSpan min, TimeSpan max) walkPaceRange)
    {
        // Build the workout
        var workoutBuilder = Workout.WorkoutBuilder
            .CreateBuilder()
            .WithType(WorkoutType.WalkRun);

        foreach (var runWalkInterval in runWalkIntervals)
        {
            // Create a repeat for the walk/run intervals
            var repeatBuilder = Repeat.RepeatBuilder
                .CreateBuilder()
                .WithCount(runWalkInterval.Count * 2); // *2 because each interval has 2 steps (walk + run)

            var runDistance = CalculateDistanceBasedOnDuration(runWalkInterval.RunDuration, runPaceRange);
            var walkDistance = CalculateDistanceBasedOnDuration(runWalkInterval.WalkDuration, walkPaceRange);

            for (int i = 0; i < runWalkInterval.Count; i++)
            {
                var runStep = SimpleStep.SimpleStepBuilder
                    .CreateBuilder()
                    .WithType(StepType.Run)
                    .WithKilometers(runDistance)
                    .WithPaceRange(runPaceRange)
                    .Build();

                var walkStep = SimpleStep.SimpleStepBuilder
                    .CreateBuilder()
                    .WithType(StepType.Walk)
                    .WithKilometers(walkDistance)
                    .WithPaceRange(walkPaceRange)
                    .Build();

                repeatBuilder
                    .WithStep(runStep)
                    .WithStep(walkStep);
            }

            var repeat = repeatBuilder.Build();

            var step = Step.StepBuilder
                .CreateBuilder()
                .WithRepeat(repeat)
                .Build();

            workoutBuilder.WithStep(step);
        }

        return workoutBuilder.Build();
    }
}
