using System.ComponentModel;
using System.Globalization;
using RunningPlanner.Core.Extensions;

namespace RunningPlanner.Core.Models;

/// <summary>
/// Represents a workout consisting of one or more steps.
/// </summary>
public record Workout
{
    /// <summary>
    /// Gets the type of workout.
    /// </summary>
    public WorkoutType Type { get; }

    /// <summary>
    /// Gets the steps that make up this workout.
    /// </summary>
    public IReadOnlyList<Step> Steps { get; }

    /// <summary>
    /// Gets the name of the workout.
    /// </summary>
    public string Name => BuildWorkoutName();

    /// <summary>
    /// Gets the total time for the workout.
    /// </summary>
    public TimeSpan TotalTime => CalculateTotalTime();

    /// <summary>
    /// Gets the estimated time for the workout.
    /// </summary>
    public TimeSpan EstimatedTime => CalculateEstimatedTime();

    /// <summary>
    /// Gets the total distance for the workout.
    /// </summary>
    public Distance TotalDistance => CalculateTotalDistance();

    /// <summary>
    /// Gets the estimated distance for the workout.
    /// </summary>
    public Distance EstimatedDistance => CalculateEstimatedDistance();

    private Workout(WorkoutType type, IEnumerable<Step> steps)
    {
        Type = type;

        if (Type is WorkoutType.Rest)
        {
            Steps = new List<Step>().AsReadOnly();

            return;
        }

        var stepsList = steps?.ToList() ?? [];

        if (stepsList.Count < 1)
        {
            throw new ArgumentException("Workout must contain at least one step.");
        }

        // Ensure no null steps
        if (stepsList.Any(step => step == null))
        {
            throw new ArgumentNullException(nameof(steps), "Steps collection cannot contain null elements.");
        }

        Steps = stepsList.AsReadOnly();
    }

    /// <summary>
    /// Builds the workout name based on its type and contents.
    /// </summary>
    private string BuildWorkoutName()
    {
        if (Type is WorkoutType.Rest)
        {
            return Type.GetDescription();
        }

        var workoutName = new List<string>();

        workoutName.Add(Type.GetDescription());

        var steps = Steps
            .SelectMany<Step, SimpleStep>(step => step is SimpleStep simpleStep
                ? [simpleStep]
                : (step as Repeat)?.Steps ?? []);

        var metric = steps
            .Select(x => x.Duration is Duration.DistanceDuration distanceDuration
                ? distanceDuration.Metric
                : DistanceMetric.Invalid)
            .FirstOrDefault();

        workoutName.Add(
            $"{TotalDistance.DistanceValue.ToString(CultureInfo.InvariantCulture)} {(metric is DistanceMetric.Kilometers ? "km" : metric.ToString())}");

        switch (Type)
        {
            case WorkoutType.EasyRun
                or WorkoutType.MediumRun
                or WorkoutType.LongRun:
                workoutName.Add(steps.First().IntensityTarget.PaceFormatted());

                break;
            case WorkoutType.HillRepeat
                or WorkoutType.Intervals
                or WorkoutType.Repetition
                or WorkoutType.TempoRunRepeat
                or WorkoutType.ThresholdRepeat:
            {
                var stepQuantity = steps.Count() / 2;
                var stepQuantityText = $"{stepQuantity}";

                var stepRun = steps.First(x => x.Type == StepType.Run);

                var stepRunText = stepRun.TotalDistance.DistanceValue >= 1m
                    ? $"{stepRun.TotalDistance.DistanceValue.RoundTo(0)}km"
                    : $"{(stepRun.TotalDistance.DistanceValue * 1000).RoundTo(0)}m";

                var stepRecover = steps.First(x => x.Type is StepType.Walk or StepType.Recover or StepType.Rest);

                var stepRecoverText = stepRecover.TotalDistance.DistanceValue >= 1m
                    ? $"{stepRun.TotalDistance.DistanceValue.RoundTo(0)}km {stepRecover.Type.GetDescription()}"
                    : $"{(stepRun.TotalDistance.DistanceValue * 1000).RoundTo(0)}m {stepRecover.Type.GetDescription()}";

                workoutName.Add(
                    $"{stepQuantityText} x ({stepRunText}{stepRun.IntensityTarget.PaceFormatted()} + {stepRecoverText})");

                break;
            }
            case WorkoutType.TempoRun
                or WorkoutType.Threshold:
            {
                var stepRun = steps.First(x => x.Type == StepType.Run);

                var stepRunText = stepRun.TotalDistance.DistanceValue >= 1m
                    ? $"{stepRun.TotalDistance.DistanceValue.RoundTo(0)}km"
                    : $"{(stepRun.TotalDistance.DistanceValue * 1000).RoundTo(0)}m";

                workoutName.Add($"{stepRunText}{stepRun.IntensityTarget.PaceFormatted()}");

                break;
            }
            case WorkoutType.EasyRunWithStrides:
            {
                var onlyStrideSteps = steps.Skip(1).Take(steps.Count());
                var stepQuantity = onlyStrideSteps.Count() / 2;
                var stepQuantityText = $"{stepQuantity}";

                var stepRun = onlyStrideSteps.First(x => x.Type == StepType.Run);

                var stepRunText = stepRun.TotalDistance.DistanceValue >= 1m
                    ? $"{stepRun.TotalDistance.DistanceValue.RoundTo(0)}km"
                    : $"{(stepRun.TotalDistance.DistanceValue * 1000).RoundTo(0)}m";

                var stepRecover =
                    onlyStrideSteps.First(x => x.Type is StepType.Walk or StepType.Recover or StepType.Rest);

                var stepRecoverText = stepRecover.TotalDistance.DistanceValue >= 1m
                    ? $"{stepRun.TotalDistance.DistanceValue.RoundTo(0)}km {stepRecover.Type.GetDescription()}"
                    : $"{(stepRun.TotalDistance.DistanceValue * 1000).RoundTo(0)}m {stepRecover.Type.GetDescription()}";

                workoutName.Add(
                    $"{stepQuantityText} x ({stepRunText}{stepRun.IntensityTarget.PaceFormatted()} + {stepRecoverText})");

                break;
            }
        }

        return string.Join(" - ", workoutName);
    }

    /// <summary>
    /// Calculates the total time for the workout.
    /// </summary>
    private TimeSpan CalculateTotalTime()
    {
        var totalTicks = Steps
            .SelectMany<Step, SimpleStep>(step => step is SimpleStep simpleStep
                ? [simpleStep]
                : (step as Repeat)?.Steps ?? [])
            .Sum(step => step.TotalTime.Ticks);

        return new TimeSpan(totalTicks);
    }

    /// <summary>
    /// Calculates the estimated time for the workout.
    /// </summary>
    private TimeSpan CalculateEstimatedTime()
    {
        var totalEstimatedTicks = Steps
            .SelectMany<Step, SimpleStep>(step => step is SimpleStep simpleStep
                ? [simpleStep]
                : (step as Repeat)?.Steps ?? [])
            .Sum(step => step.EstimatedTime.Ticks);

        return new TimeSpan(totalEstimatedTicks);
    }

    /// <summary>
    /// Calculates the total distance for the workout.
    /// </summary>
    private Distance CalculateTotalDistance()
    {
        var distance = Steps
            .SelectMany<Step, SimpleStep>(step => step is SimpleStep simpleStep
                ? [simpleStep]
                : (step as Repeat)?.Steps ?? [])
            .Sum(step => step.TotalDistance.DistanceValue);

        return Distance.Kilometers(Math.Round(distance, 1, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Calculates the estimated distance for the workout.
    /// </summary>
    private Distance CalculateEstimatedDistance()
    {
        var estimatedDistance = Steps
            .SelectMany<Step, SimpleStep>(step => step is SimpleStep simpleStep
                ? [simpleStep]
                : (step as Repeat)?.Steps ?? [])
            .Sum(step => step.EstimatedDistance.DistanceValue);

        return Distance.Kilometers(estimatedDistance);
    }

    /// <summary>
    /// Creates a standard workout with the specified type and steps.
    /// </summary>
    public static Workout Create(WorkoutType type, IEnumerable<Step> steps) =>
        new(type, steps);

    /// <summary>
    /// Creates a standard workout with the specified type and steps.
    /// </summary>
    public static Workout Create(WorkoutType type, params Step[] steps) =>
        new(type, steps);

    /// <summary>
    /// Creates an easy run workout.
    /// </summary>
    public static Workout CreateEasyRun(decimal distance, (TimeSpan min, TimeSpan max) pace)
    {
        var step = SimpleStep.CreateWithKilometers(StepType.Run, distance, IntensityTarget.Pace(pace.min, pace.max));

        return new Workout(WorkoutType.EasyRun, [step]);
    }

    /// <summary>
    /// Creates a medium run workout.
    /// </summary>
    public static Workout CreateMediumRun(decimal distance, (TimeSpan min, TimeSpan max) pace)
    {
        var step = SimpleStep.CreateWithKilometers(StepType.Run, distance, IntensityTarget.Pace(pace.min, pace.max));

        return new Workout(WorkoutType.MediumRun, [step]);
    }

    /// <summary>
    /// Creates a long run workout.
    /// </summary>
    public static Workout CreateLongRun(decimal distance, (TimeSpan min, TimeSpan max) pace)
    {
        var step = SimpleStep.CreateWithKilometers(StepType.Run, distance, IntensityTarget.Pace(pace.min, pace.max));

        return new Workout(WorkoutType.LongRun, [step]);
    }

    /// <summary>
    /// Creates a race pace workout.
    /// </summary>
    public static Workout CreateRacePace(decimal distance, (TimeSpan min, TimeSpan max) pace = default)
    {
        var step = SimpleStep.CreateWithKilometers(StepType.Run, distance, IntensityTarget.Pace(pace.min, pace.max));

        return new Workout(WorkoutType.RacePace, [step]);
    }

    /// <summary>
    /// Creates a rest workout.
    /// </summary>
    public static Workout CreateRest()
    {
        return new Workout(WorkoutType.Rest, []);
    }

    /// <summary>
    /// Creates a race workout.
    /// </summary>
    public static Workout CreateRace(decimal distance)
    {
        var step = SimpleStep.CreateWithNoTarget(StepType.Run, Duration.ForKilometers(distance));

        return new Workout(WorkoutType.Race, [step]);
    }

    /// <summary>
    /// Creates a race workout.
    /// </summary>
    public static Workout CreateRace(decimal distance, (TimeSpan min, TimeSpan max) paceRange)
    {
        var step = SimpleStep.CreateWithKilometers(
            StepType.Run,
            distance,
            IntensityTarget.Pace(paceRange.min, paceRange.max));

        return new Workout(WorkoutType.Race, [step]);
    }

    /// <summary>
    /// Creates a hill workout consisting of repeats, warmup, and cooldown based on the specified parameters.
    /// </summary>
    /// <param name="repeats">Number of hill repeats in the workout.</param>
    /// <param name="totalDistance">Total workout distance in kilometers.</param>
    /// <param name="easyPaceRange">The pace range for the easy running segments during warmup, cooldown, and additional distance.</param>
    /// <param name="hillPaceRange">The pace range for the uphill portions of the hill repeats.</param>
    /// <param name="recoveryPaceRange">The pace range for the recovery portions (downhill) of the hill repeats.</param>
    /// <param name="hillUpDistance"></param>
    /// <param name="hillDownDistance"></param>
    /// <param name="warmupDistance">Distance for the warmup in kilometers (default is 2.0).</param>
    /// <param name="cooldownDistance">Distance for the cooldown in kilometers (default is 2.0).</param>
    /// <returns>A hill workout object containing the specified warmup, hill repeats, cooldown, and any necessary additional distance.</returns>
    public static Workout CreateHillRepeat(
        int repeats,
        decimal totalDistance,
        (TimeSpan min, TimeSpan max) easyPaceRange,
        (TimeSpan min, TimeSpan max) hillPaceRange,
        (TimeSpan min, TimeSpan max) recoveryPaceRange,
        decimal hillUpDistance = 0.4m,
        decimal hillDownDistance = 0.4m,
        decimal warmupDistance = 2.0m,
        decimal cooldownDistance = 2.0m)
    {
        decimal remainingDistance = totalDistance - (warmupDistance + cooldownDistance);

        // Each hill repeat is approximate:
        decimal totalRepeatDistance = repeats * (hillUpDistance + hillDownDistance);

        // If there's additional distance to cover, we'll add it as an easy run
        decimal additionalEasyDistance = Math.Max(0, remainingDistance - totalRepeatDistance);

        var warmUpStep = SimpleStep.CreateWithKilometers(
            StepType.WarmUp,
            warmupDistance,
            IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max));

        var steps = new List<SimpleStep>();

        for (var i = 0; i < repeats; i++)
        {
            var runStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Run,
                    hillUpDistance,
                    IntensityTarget.Pace(hillPaceRange.min, hillPaceRange.max));

            var restStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Recover,
                    hillDownDistance,
                    IntensityTarget.Pace(recoveryPaceRange.min, recoveryPaceRange.max));

            steps.Add(runStep);
            steps.Add(restStep);
        }

        var totalRepeats = repeats * 2;

        var repeatStep = Repeat.Create(totalRepeats, steps);

        var coolDownStep = SimpleStep.CreateWithKilometers(
            StepType.CoolDown,
            cooldownDistance + additionalEasyDistance,
            IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max));

        return
            new Workout(
                WorkoutType.HillRepeat,
                new List<Step>
                {
                    warmUpStep,
                    repeatStep,
                    coolDownStep
                });
    }

    /// <summary>
    /// Creates an interval workout with warm-up, repeated interval and recovery steps,
    /// and a cool-down step, ensuring the total distance matches the specified value.
    /// </summary>
    /// <param name="repeats">The number of interval and recovery repetitions.</param>
    /// <param name="meters">The distance in meters for each interval.</param>
    /// <param name="recoveryMeters">The distance in meters for each recovery period.</param>
    /// <param name="totalDistance">The total distance for the workout, in kilometers.</param>
    /// <param name="easyPaceRange">The pace range for easy intensity during warm-up and cool-down.</param>
    /// <param name="intervalPaceRange">The pace range for the interval runs.</param>
    /// <param name="recoveryPaceRange">The pace range for the recovery runs.</param>
    /// <param name="warmupDistance">The distance for the warm-up step, in kilometers. Defaults to 2.0 km.</param>
    /// <param name="cooldownDistance">The distance for the cool-down step, in kilometers. Defaults to 2.0 km.</param>
    /// <returns>A <see cref="Workout"/> object representing the interval workout.</returns>
    public static Workout CreateInterval(
        int repeats,
        int meters,
        int recoveryMeters,
        decimal totalDistance,
        (TimeSpan min, TimeSpan max) easyPaceRange,
        (TimeSpan min, TimeSpan max) intervalPaceRange,
        (TimeSpan min, TimeSpan max) recoveryPaceRange,
        decimal warmupDistance = 2.0m,
        decimal cooldownDistance = 2.0m,
        decimal? restBeforeStartIntervalDistance = null)
    {
        // For interval workouts:
        // - Warmup as specified (default 2km)
        // - Interval repeats (with recovery)
        // - Cooldown as specified (default 2km)

        // Convert meters to kilometers for intervals
        decimal intervalDistance = meters / 1000.0m;
        decimal recoveryDistance = recoveryMeters / 1000.0m;

        // Total distance for intervals and recovery
        decimal intervalTotalDistance = repeats * (intervalDistance + recoveryDistance);

        var currentWorkoutTotalDistance = warmupDistance +
                                          intervalTotalDistance +
                                          cooldownDistance +
                                          (restBeforeStartIntervalDistance ?? 0.0m);

        // Additional easy distance if needed to match total
        decimal remainingDistance = currentWorkoutTotalDistance - totalDistance;
        decimal additionalEasyDistance = Math.Max(0, remainingDistance);

        var warmUpStep = SimpleStep.CreateWithKilometers(
            StepType.WarmUp,
            warmupDistance,
            IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max));

        var steps = new List<SimpleStep>();

        for (var i = 0; i < repeats; i++)
        {
            var runStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Run,
                    intervalDistance,
                    IntensityTarget.Pace(intervalPaceRange.min, intervalPaceRange.max));

            var restStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Recover,
                    recoveryDistance,
                    IntensityTarget.Pace(recoveryPaceRange.min, recoveryPaceRange.max));

            steps.Add(runStep);
            steps.Add(restStep);
        }

        var totalRepeats = repeats * 2;

        var repeatStep = Repeat.Create(totalRepeats, steps);

        var coolDownStep = SimpleStep.CreateWithKilometers(
            StepType.CoolDown,
            cooldownDistance + additionalEasyDistance,
            IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max));

        var workoutSteps = new List<Step>();
        workoutSteps.Add(warmUpStep);

        if (restBeforeStartIntervalDistance.HasValue)
        {
            var restStep = SimpleStep.CreateWithKilometers(
                StepType.Rest,
                restBeforeStartIntervalDistance.Value,
                IntensityTarget.Pace(recoveryPaceRange.min, recoveryPaceRange.max));
            workoutSteps.Add(restStep);
        }

        workoutSteps.Add(repeatStep);
        workoutSteps.Add(coolDownStep);

        return
            new Workout(
                WorkoutType.Intervals,
                workoutSteps);
    }

    /// <summary>
    /// Creates a tempo workout consisting of a warmup, a tempo section, a cooldown, and any necessary additional easy running distance based on the specified parameters.
    /// </summary>
    /// <param name="tempoMinutes">The duration of the tempo section in minutes.</param>
    /// <param name="totalDistance">The total distance of the workout in kilometers.</param>
    /// <param name="easyPaceRange">The pace range for the easy running segments during warmup, cooldown, and additional distance.</param>
    /// <param name="tempoPaceRange">The pace range for the tempo section of the workout.</param>
    /// <returns>A tempo workout object containing the specified warmup, tempo section, cooldown, and any necessary additional easy running distance.</returns>
    public static Workout CreateTempo(
        int tempoMinutes,
        decimal totalDistance,
        (TimeSpan min, TimeSpan max) easyPaceRange,
        (TimeSpan min, TimeSpan max) tempoPaceRange)
    {
        var tempoDistance = CalculateDistanceBasedOnDuration(TimeSpan.FromMinutes(tempoMinutes), tempoPaceRange);

        var remainingDistance = totalDistance - tempoDistance;

        while (remainingDistance <= 0)
        {
            tempoDistance = CalculateDistanceBasedOnDuration(TimeSpan.FromMinutes(tempoMinutes), tempoPaceRange);
            remainingDistance = totalDistance - tempoDistance;
            tempoMinutes--;
        }

        var warmupCooldownDistance = remainingDistance / 2;

        var warmUpStep = SimpleStep.CreateWithKilometers(
            StepType.WarmUp,
            warmupCooldownDistance,
            IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max));

        var tempoStep = SimpleStep.CreateWithKilometers(
            StepType.Run,
            tempoDistance,
            IntensityTarget.Pace(tempoPaceRange.min, tempoPaceRange.max));

        var coolDown = SimpleStep.CreateWithKilometers(
            StepType.CoolDown,
            warmupCooldownDistance,
            IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max));

        return Create(
            WorkoutType.TempoRun,
            new List<Step>
            {
                warmUpStep,
                tempoStep,
                coolDown
            });
    }

    /// <summary>
    /// Creates a stride workout that incorporates strides with defined pace ranges and recovery segments,
    /// along with warm-up and cool-down phases.
    /// </summary>
    /// <param name="totalDistance">The total distance of the workout in kilometers.</param>
    /// <param name="strideCount">The number of strides to include in the workout.</param>
    /// <param name="strideDistanceMeters"></param>
    /// <param name="easyPaceRange">The pace range for the easy run sections of the workout.</param>
    /// <param name="strideRange">The pace range for the strides.</param>
    /// <param name="recoveryPaceRange">The pace range for the recovery sections between strides.</param>
    /// <returns>A stride workout object with strides integrated into the workout flow.</returns>
    public static Workout CreateStrideWorkout(
        decimal totalDistance,
        int strideCount,
        decimal strideDistanceMeters,
        (TimeSpan min, TimeSpan max) easyPaceRange,
        (TimeSpan min, TimeSpan max) strideRange,
        (TimeSpan min, TimeSpan max) recoveryPaceRange)
    {
        var steps = new List<SimpleStep>();

        for (var i = 0; i < strideCount; i++)
        {
            var runSimpleStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Run,
                    strideDistanceMeters / 1000m,
                    IntensityTarget.Pace(strideRange.min, strideRange.max));

            var restSimpleStep = SimpleStep
                .CreateWithKilometers(
                    StepType.Recover,
                    strideDistanceMeters / 1000m,
                    IntensityTarget.Pace(recoveryPaceRange.min, recoveryPaceRange.max));

            steps.Add(runSimpleStep);
            steps.Add(restSimpleStep);
        }

        var totalRepeats = strideCount * 2;

        var repeatStep = Repeat.Create(totalRepeats, steps);

        var runStep = SimpleStep.CreateWithKilometers(
            StepType.Run,
            totalDistance - repeatStep.Steps.StepsDistance(),
            IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max));

        return Create(
            WorkoutType.EasyRunWithStrides,
            new List<Step>
            {
                runStep,
                repeatStep
            });
    }

    /// <summary>
    /// Creates a walk/run workout with specified intervals of running and walking.
    /// Each interval specifies the number of repetitions, run duration, and walk duration.
    /// The run and walk durations are paired with their respective pace ranges to calculate distances.
    /// </summary>
    /// <param name="runPaceRange">The minimum and maximum paces for the running segments.</param>
    /// <param name="walkPaceRange">The minimum and maximum paces for the walking segments.</param>
    /// <param name="runWalkIntervals">A list of tuples representing intervals for the workout, where each tuple contains
    ///     the number of repetitions, run duration, and walk duration.</param>
    /// <returns>A workout object of type WalkRun configured with the specified intervals, paces, and steps.</returns>
    public static Workout CreateRunWalkWorkout(
        (TimeSpan min, TimeSpan max) runPaceRange,
        (TimeSpan min, TimeSpan max) walkPaceRange,
        params WalkRunInterval[] runWalkIntervals)
    {
        var steps = new List<Step>();

        foreach (var runWalkInterval in runWalkIntervals)
        {
            var runDistance = CalculateDistanceBasedOnDuration(runWalkInterval.RunDuration, runPaceRange);
            var walkDistance = CalculateDistanceBasedOnDuration(runWalkInterval.WalkDuration, walkPaceRange);

            var simpleSteps = new List<SimpleStep>();

            for (int i = 0; i < runWalkInterval.RepeatCount; i++)
            {
                var runStep = SimpleStep
                    .CreateWithKilometers(
                        StepType.Run,
                        runDistance,
                        IntensityTarget.Pace(runPaceRange.min, runPaceRange.max));

                var walkStep = SimpleStep
                    .CreateWithKilometers(
                        StepType.Walk,
                        walkDistance,
                        IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max));

                simpleSteps.Add(runStep);
                simpleSteps.Add(walkStep);
            }

            // Create a repeat for the walk/run intervals
            var totalRepeats = runWalkInterval.RepeatCount * 2; // *2 because each interval has 2 steps (walk plus run)

            steps.Add(Repeat.Create(totalRepeats, simpleSteps));
        }

        return Create(WorkoutType.WalkRun, steps);
    }

    /// <summary>
    /// Calculates the distance covered based on the duration and specified pace range.
    /// </summary>
    /// <param name="duration">The time duration for which the distance is to be calculated.</param>
    /// <param name="paceRange">The pace range (minimum and maximum pace) used for the calculation.</param>
    /// <returns>The calculated distance based on the given duration and pace range.</returns>
    private static decimal CalculateDistanceBasedOnDuration(TimeSpan duration, (TimeSpan min, TimeSpan max) paceRange)
    {
        // Calculate average tempo pace in ticks per km, then convert to minutes
        decimal avgTempoTicksPerKm = (decimal) (paceRange.min.Ticks + paceRange.max.Ticks) / 2;
        decimal avgMinutesPerKm = avgTempoTicksPerKm / (decimal) TimeSpan.TicksPerMinute;

        decimal minutes = (decimal) duration.Ticks / TimeSpan.TicksPerMinute;

        // Calculate the distance covered during the tempo portion
        decimal distance = minutes / avgMinutesPerKm;

        return Math.Round(distance, 2, MidpointRounding.AwayFromZero);
    }
}

public enum WorkoutType
{
    Invalid,
    [Description("Cross Training")] Cross,
    [Description("Rest Day")] Rest,
    [Description("Walk/Run Intervals")] WalkRun,
    [Description("Easy Run")] EasyRun,
    [Description("Easy Run w/ Strides")] EasyRunWithStrides,
    [Description("Mid Distance Run")] MediumRun,
    [Description("Repetition")] Repetition,
    [Description("Intervals")] Intervals,
    [Description("Threshold")] Threshold,
    [Description("Threshold Intervals")] ThresholdRepeat,
    [Description("Tempo Run")] TempoRun,
    [Description("Tempo Run Intervals")] TempoRunRepeat,
    [Description("Fartlek")] Fartlek,
    [Description("Hill Repeat")] HillRepeat,
    [Description("Race Pace")] RacePace,
    [Description("Long Run")] LongRun,
    [Description("Race")] Race
}
