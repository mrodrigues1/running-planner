namespace RunningPlanner.Core.Models.Paces;

/// <summary>
/// Represents a VDOT table entry with race times for common distances
/// </summary>
public class VdotEntry
{
    public int Vdot { get; }

    public TimeSpan FifTeenHundred { get; set; }
    public TimeSpan Mile { get; set; }
    public TimeSpan ThreeKm { get; set; }
    public TimeSpan TwoMile { get; set; }
    public TimeSpan FiveKm { get; }
    public TimeSpan TenKm { get; }
    public TimeSpan FifteenKm { get; }
    public TimeSpan HalfMarathon { get; }
    public TimeSpan Marathon { get; }

    public VdotEntry(
        int vdot,
        string fiveKm,
        string tenKm,
        string halfMarathon,
        string marathon)
    {
        Vdot = vdot;
        FiveKm = ParseTime(fiveKm);
        TenKm = ParseTime(tenKm);
        HalfMarathon = ParseTime(halfMarathon);
        Marathon = ParseTime(marathon);
    }
    
    public VdotEntry(
        int vdot,
        string fifTeenHundred,
        string mile,
        string threeKm,
        string twoMile,
        string fiveKm,
        string tenKm,
        string fifteenKm,
        string halfMarathon,
        string marathon)
    {
        Vdot = vdot;
        FifTeenHundred = ParseTime(fifTeenHundred);
        Mile = ParseTime(mile);
        ThreeKm = ParseTime(threeKm);
        TwoMile = ParseTime(twoMile);
        FiveKm = ParseTime(fiveKm);
        TenKm = ParseTime(tenKm);
        FifteenKm = ParseTime(fifteenKm);
        HalfMarathon = ParseTime(halfMarathon);
        Marathon = ParseTime(marathon);
    }

    private static TimeSpan ParseTime(string timeString)
    {
        // Handle different time formats (mm:ss or h:mm:ss)
        if (timeString.Count(c => c == ':') == 1)
        {
            return TimeSpan.ParseExact(timeString, @"mm\:ss", null);
        }
        else
        {
            return TimeSpan.ParseExact(timeString, @"h\:mm\:ss", null);
        }
    }
}
