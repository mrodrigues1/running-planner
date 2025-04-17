namespace RunningPlanner.Core.Models;

public record TrainingPhaseDistribution(
    int BaseWeeks,
    int BuildWeeks,
    int PeakWeeks,
    int TaperWeeks,
    int RaceWeeks = 1)
{
    public Dictionary<TrainingPhase, int> ToDictionary() =>
        new()
        {
            {TrainingPhase.Base, BaseWeeks},
            {TrainingPhase.Build, BuildWeeks},
            {TrainingPhase.Peak, PeakWeeks},
            {TrainingPhase.Taper, TaperWeeks - 1}, // -1 for race week
            {TrainingPhase.Race, RaceWeeks}
        };
}
