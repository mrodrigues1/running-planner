using System.Globalization;

namespace RunningPlanner.Core.Models;

public class WorkoutSample
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
    /// Gets the total distance for the workout.
    /// </summary>
    public Distance TotalDistance => CalculateTotalDistance();

    private WorkoutSample(WorkoutType type, IEnumerable<Step> steps)
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
            return Type.ToString();
        }

        var workoutName = new List<string>();

        workoutName.Add(Type.ToString());

        var metric = Steps
            .SelectMany<Step, SimpleStepSample>(
                step => step is SimpleStepSample SampleSimpleStep
                    ? [SampleSimpleStep]
                    : (step as RepeatSample)?.Steps ?? [])
            .Select(
                x => x.Duration is Duration.DistanceDuration distanceDuration
                    ? distanceDuration.Metric
                    : DistanceMetric.Invalid)
            .FirstOrDefault();

        workoutName.Add(
            $"{TotalDistance.DistanceValue.ToString(CultureInfo.InvariantCulture)} {metric.ToString() ?? "km"}");

        return string.Join(" - ", workoutName);
    }

    /// <summary>
    /// Calculates the total time for the workout.
    /// </summary>
    private TimeSpan CalculateTotalTime()
    {
        var totalTicks = Steps
            .SelectMany<Step, SimpleStepSample>(
                step => step is SimpleStepSample SampleSimpleStep
                    ? [SampleSimpleStep]
                    : (step as RepeatSample)?.Steps ?? [])
            .Sum(step => step.TotalTime.Ticks);

        return new TimeSpan(totalTicks);
    }

    /// <summary>
    /// Calculates the total distance for the workout.
    /// </summary>
    private Distance CalculateTotalDistance()
    {
        var distance = Steps
            .SelectMany<Step, SimpleStepSample>(
                step => step is SimpleStepSample SampleSimpleStep
                    ? [SampleSimpleStep]
                    : (step as RepeatSample)?.Steps ?? [])
            .Sum(step => step.TotalDistance.DistanceValue);

        return Distance.Kilometers(Math.Round(distance, 1, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Creates a standard workout with the specified type and steps.
    /// </summary>
    public static WorkoutSample Create(WorkoutType type, IEnumerable<Step> steps) =>
        new(type, steps);

    /// <summary>
    /// Creates a standard workout with the specified type and steps.
    /// </summary>
    public static WorkoutSample Create(WorkoutType type, params Step[] steps) =>
        new(type, steps);

    /// <summary>
    /// Creates an easy run workout.
    /// </summary>
    public static WorkoutSample CreateEasyRun(decimal distance)
    {
        var step = SimpleStepSample.CreateWithKilometers(StepType.Run, distance);

        return new WorkoutSample(WorkoutType.EasyRun, [step]);
    }

    /// <summary>
    /// Creates a medium run workout.
    /// </summary>
    public static WorkoutSample CreateMediumRun(decimal distance)
    {
        var step = SimpleStepSample.CreateWithKilometers(StepType.Run, distance);

        return new WorkoutSample(WorkoutType.MediumRun, [step]);
    }

    /// <summary>
    /// Creates a long run workout.
    /// </summary>
    public static WorkoutSample CreateLongRun(decimal distance, (TimeSpan min, TimeSpan max) pace)
    {
        var step = SimpleStepSample.CreateWithKilometers(
            StepType.Run,
            distance);

        return new WorkoutSample(WorkoutType.LongRun, [step]);
    }

    /// <summary>
    /// Creates a race pace workout.
    /// </summary>
    public static WorkoutSample CreateRacePace(decimal distance, (TimeSpan min, TimeSpan max) pace = default)
    {
        var step = SimpleStepSample.CreateWithKilometers(
            StepType.Run,
            distance);

        return new WorkoutSample(WorkoutType.RacePace, [step]);
    }

    /// <summary>
    /// Creates a rest workout.
    /// </summary>
    public static WorkoutSample CreateRest()
    {
        return new WorkoutSample(WorkoutType.Rest, []);
    }

    /// <summary>
    /// Creates a race workout.
    /// </summary>
    public static WorkoutSample CreateRace(decimal distance)
    {
        var step = SimpleStepSample.CreateWithKilometers(StepType.Run, distance);

        return new WorkoutSample(WorkoutType.Race, [step]);
    }

    /// <summary>
    /// Creates a race workout.
    /// </summary>
    public static WorkoutSample CreateRace(decimal distance, (TimeSpan min, TimeSpan max) paceRange)
    {
        var step = SimpleStepSample.CreateWithKilometers(
            StepType.Run,
            distance);

        return new WorkoutSample(WorkoutType.Race, [step]);
    }

    /// <summary>
    /// Creates a hill workout consisting of repeats, warmup, and cooldown based on the specified parameters.
    /// </summary>
    /// <param name="repeats">Number of hill repeats in the workout.</param>
    /// <param name="totalDistance">Total workout distance in kilometers.</param>
    /// <param name="warmupDistance">Distance for the warmup in kilometers (default is 2.0).</param>
    /// <param name="cooldownDistance">Distance for the cooldown in kilometers (default is 2.0).</param>
    /// <returns>A hill workout object containing the specified warmup, hill repeats, cooldown, and any necessary additional distance.</returns>
    public static WorkoutSample CreateHillRepeat(
        int repeats,
        decimal hillUpDistance,
        decimal hillDownDistance,
        decimal totalDistance,
        decimal warmupDistance = 2.0m,
        decimal cooldownDistance = 2.0m)
    {
        // For hill workouts:
        // - Warmup as specified (default 2km)
        // - Repeats
        // - Cooldown as specified (default 2km)
        decimal remainingDistance = totalDistance - (warmupDistance + cooldownDistance);

        // Each hill repeat is approximately:
        decimal totalRepeatDistance = repeats * (hillUpDistance + hillDownDistance);

        // If there's additional distance to cover, we'll add it as an easy run
        decimal additionalEasyDistance = Math.Max(0, remainingDistance - totalRepeatDistance);

        var warmUpStep = SimpleStepSample.CreateWithKilometers(
            StepType.WarmUp,
            warmupDistance);

        var steps = new List<SimpleStepSample>();

        for (var i = 0; i < repeats; i++)
        {
            var runStep = SimpleStepSample
                .CreateWithKilometers(
                    StepType.Run,
                    hillUpDistance);

            var restStep = SimpleStepSample
                .CreateWithKilometers(
                    StepType.Recover,
                    hillDownDistance);

            steps.Add(runStep);
            steps.Add(restStep);
        }

        var totalRepeats = repeats * 2;

        var repeatStep = RepeatSample.Create(totalRepeats, steps);

        var coolDownStep = SimpleStepSample.CreateWithKilometers(
            StepType.CoolDown,
            cooldownDistance + additionalEasyDistance);

        return
            new WorkoutSample(
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
    public static WorkoutSample CreateInterval(
        int repeats,
        int meters,
        int recoveryMeters,
        decimal totalDistance,
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

        // Additional easy distance if needed to match total
        decimal remainingDistance = totalDistance -
                                    (warmupDistance +
                                     intervalTotalDistance +
                                     cooldownDistance +
                                     (restBeforeStartIntervalDistance ?? 0.0m));
        decimal additionalEasyDistance = Math.Max(0, remainingDistance);

        var warmUpStep = SimpleStepSample.CreateWithKilometers(
            StepType.WarmUp,
            warmupDistance);

        var steps = new List<SimpleStepSample>();

        for (var i = 0; i < repeats; i++)
        {
            var runStep = SimpleStepSample
                .CreateWithKilometers(
                    StepType.Run,
                    intervalDistance);

            var restStep = SimpleStepSample
                .CreateWithKilometers(
                    StepType.Recover,
                    recoveryDistance);

            steps.Add(runStep);
            steps.Add(restStep);
        }

        var totalRepeats = repeats * 2;

        var repeatStep = RepeatSample.Create(totalRepeats, steps);

        var coolDownStep = SimpleStepSample.CreateWithKilometers(
            StepType.CoolDown,
            cooldownDistance + additionalEasyDistance);

        var workoutSteps = new List<Step>();
        workoutSteps.Add(warmUpStep);

        if (restBeforeStartIntervalDistance.HasValue)
        {
            var restStep = SimpleStepSample.CreateWithKilometers(
                StepType.Rest,
                restBeforeStartIntervalDistance.Value);
            workoutSteps.Add(restStep);
        }

        workoutSteps.Add(repeatStep);
        workoutSteps.Add(coolDownStep);

        return
            new WorkoutSample(
                WorkoutType.Intervals,
                workoutSteps);
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
    public static WorkoutSample CreateStrideWorkout(
        decimal distance,
        int strideCount)
    {
        var runStep = SimpleStepSample.CreateWithKilometers(
            StepType.Run,
            distance);

        var steps = new List<SimpleStepSample>();

        for (var i = 0; i < strideCount; i++)
        {
            var runSampleSimpleStep = SimpleStepSample
                .CreateWithKilometers(
                    StepType.Run,
                    0.1m);
            var restSampleSimpleStep = SimpleStepSample
                .CreateWithKilometers(
                    StepType.Recover,
                    0.1m);

            steps.Add(runSampleSimpleStep);
            steps.Add(restSampleSimpleStep);
        }

        var totalRepeats = strideCount * 2;

        var repeatStep = RepeatSample.Create(totalRepeats, steps);

        return Create(
            WorkoutType.EasyRunWithStrides,
            new List<Step>
            {
                runStep,
                repeatStep
            });
    }

    /// <summary>
    /// Creates a tempo workout sample based on the given parameters, including warm-up, threshold, and cool-down distances.
    /// </summary>
    /// <param name="thresholdDistance">The distance of the tempo section (run at a faster pace).</param>
    /// <param name="totalDistance">The total distance of the workout.</param>
    /// <param name="warmupDistance">The distance of the warm-up section. Default is 2.0 km.</param>
    /// <param name="cooldownDistance">The distance of the cool-down section. Default is 2.0 km.</param>
    /// <returns>A WorkoutSample object representing the full tempo workout, including any additional easy running distance needed to match the total distance.</returns>
    public static WorkoutSample CreateTempo(
        decimal thresholdDistance,
        decimal totalDistance,
        decimal warmupDistance = 2.0m,
        decimal cooldownDistance = 2.0m)
    {
        // For tempo workouts:
        // - Warmup as specified (default 2km)
        // - Tempo section at faster pace (using the distance specified)
        // - Cooldown as specified (default 2km)


        // Additional easy distance if needed to match total
        decimal remainingDistance = totalDistance - (warmupDistance + thresholdDistance + cooldownDistance);
        decimal additionalEasyDistance = Math.Max(0, remainingDistance);

        var warmUpStep = SimpleStepSample.CreateWithKilometers(
            StepType.WarmUp,
            warmupDistance);

        var thresholdStep = SimpleStepSample.CreateWithKilometers(
            StepType.Run,
            warmupDistance);

        var coolDown = SimpleStepSample.CreateWithKilometers(
            StepType.CoolDown,
            cooldownDistance + additionalEasyDistance);

        return Create(
            WorkoutType.Threshold,
            new List<Step>
            {
                warmUpStep,
                thresholdStep,
                coolDown
            });
    }
    
    /// <summary>
    /// Creates a tempo workout sample with specified tempo, warmup, and cooldown durations.
    /// </summary>
    /// <param name="tempoMinutes">The duration of the tempo run in minutes.</param>
    /// <param name="warmupMinutes">The duration of the warmup in minutes. Defaults to 10 minutes if not specified.</param>
    /// <param name="cooldownMinutes">The duration of the cooldown in minutes. Defaults to 10 minutes if not specified.</param>
    /// <returns>A <see cref="WorkoutSample"/> configured as a tempo workout.</returns>
    public static WorkoutSample CreateTempo(
        int tempoMinutes,
        int warmupMinutes = 10,
        int cooldownMinutes = 10)
    {
        var warmUpStep = SimpleStepSample.CreateWithMinutes(StepType.WarmUp, warmupMinutes);

        var tempoStep = SimpleStepSample.CreateWithMinutes(StepType.Run, tempoMinutes);

        var coolDown = SimpleStepSample.CreateWithMinutes(StepType.CoolDown, cooldownMinutes);

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
    /// Creates a walk/run workout with specified intervals of running and walking.
    /// Each interval specifies the number of repetitions, run duration, and walk duration.
    /// The run and walk durations are paired with their respective pace ranges to calculate distances.
    /// </summary>
    /// <param name="runPaceRange">The minimum and maximum paces for the running segments.</param>
    /// <param name="walkPaceRange">The minimum and maximum paces for the walking segments.</param>
    /// <param name="runWalkIntervals">A list of tuples representing intervals for the workout, where each tuple contains
    ///     the number of repetitions, run duration, and walk duration.</param>
    /// <returns>A workout object of type WalkRun configured with the specified intervals, paces, and steps.</returns>
    public static WorkoutSample CreateRunWalkWorkout(
        (TimeSpan min, TimeSpan max) runPaceRange,
        (TimeSpan min, TimeSpan max) walkPaceRange,
        params WalkRunInterval[] runWalkIntervals)
    {
        var steps = new List<Step>();

        foreach (var runWalkInterval in runWalkIntervals)
        {
            var runDistance = CalculateDistanceBasedOnDuration(runWalkInterval.RunDuration, runPaceRange);
            var walkDistance = CalculateDistanceBasedOnDuration(runWalkInterval.WalkDuration, walkPaceRange);

            var SampleSimpleSteps = new List<SimpleStepSample>();

            for (int i = 0; i < runWalkInterval.RepeatCount; i++)
            {
                var runStep = SimpleStepSample
                    .CreateWithKilometers(
                        StepType.Run,
                        runDistance);

                var walkStep = SimpleStepSample
                    .CreateWithKilometers(
                        StepType.Walk,
                        walkDistance);

                SampleSimpleSteps.Add(runStep);
                SampleSimpleSteps.Add(walkStep);
            }

            // Create a repeat for the walk/run intervals
            var totalRepeats = runWalkInterval.RepeatCount * 2; // *2 because each interval has 2 steps (walk + run)

            steps.Add(RepeatSample.Create(totalRepeats, SampleSimpleSteps));
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

        return distance;
    }
}
