﻿namespace RunningPlanner.Core.Models.Paces;

/// <summary>
/// Represents training paces for a specific VDOT value
/// </summary>
public class TrainingPaces
{
    public int Vdot { get; }
    public (TimeSpan Min, TimeSpan Max) EasyPace { get; }
    public (TimeSpan Min, TimeSpan Max) MarathonPace { get; }
    public (TimeSpan Min, TimeSpan Max) ThresholdPace { get; }
    public (TimeSpan Min, TimeSpan Max) IntervalPace { get; }
    public (TimeSpan Min, TimeSpan Max) RepetitionPace { get; }
    public (TimeSpan Min, TimeSpan Max) RecoveryPace { get; }
    public (TimeSpan Min, TimeSpan Max) RestPace { get; }

    public TrainingPaces(
        int vdot,
        string easyRange,
        string marathonPace,
        string thresholdPace,
        string intervalPace,
        string repetitionPace)
    {
        Vdot = vdot;
        EasyPace = ParsePaceRange(easyRange);
        MarathonPace = ConvertToPaceRange(marathonPace);
        ThresholdPace = ConvertToPaceRange(thresholdPace);
        IntervalPace = ConvertToPaceRange(intervalPace);
        RepetitionPace = ConvertToPaceRange(repetitionPace);
        RecoveryPace = CalculateRecoveryPace(EasyPace);
        RestPace = CalculateRestPace(EasyPace);
    }

    private static (TimeSpan Min, TimeSpan Max) ParsePaceRange(string paceRange)
    {
        var parts = paceRange.Split('-');

        return (ParseSinglePace(parts[0]), ParseSinglePace(parts[1]));
    }

    private static TimeSpan ParseSinglePace(string pace)
    {
        return TimeSpan.ParseExact(pace, @"m\:ss", null);
    }

    private static (TimeSpan Min, TimeSpan Max) ConvertToPaceRange(string pace)
    {
        var paceTime = TimeSpan.ParseExact(pace, @"m\:ss", null);

        return (paceTime.Add(TimeSpan.FromSeconds(-5)), paceTime.Add(TimeSpan.FromSeconds(5)));
    }

    private (TimeSpan Min, TimeSpan Max) CalculateRecoveryPace((TimeSpan Min, TimeSpan Max) easyPace)
    {
        return (easyPace.Min.Add(TimeSpan.FromMinutes(1)), easyPace.Max.Add(TimeSpan.FromMinutes(3)));
    }

    private (TimeSpan Min, TimeSpan Max) CalculateRestPace((TimeSpan Min, TimeSpan Max) easyPace)
    {
        return (easyPace.Min.Add(TimeSpan.FromMinutes(3)), easyPace.Max.Add(TimeSpan.FromMinutes(5)));
    }

    /// <summary>
    /// Returns all training paces in a formatted string
    /// </summary>
    public string GetFormattedPaces()
    {
        return $"VDOT: {Vdot}\n" +
               $"Easy Pace: {EasyPace.Min:m\\:ss}-{EasyPace.Max:m\\:ss} per km\n" +
               $"Marathon Pace: {MarathonPace:m\\:ss} per km\n" +
               $"Threshold Pace: {ThresholdPace:m\\:ss} per km\n" +
               $"Interval Pace: {IntervalPace:m\\:ss} per km\n" +
               $"Repetition Pace: {RepetitionPace:m\\:ss} per km\n" +
               $"Recovery Pace: {RecoveryPace:m\\:ss} per km\n +" +
               $"Rest Pace: {RestPace:m\\:ss} per km";
    }
}
