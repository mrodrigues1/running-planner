namespace RunningPlanner.Core.Models.Paces;

/// <summary>
/// Implements Jack Daniels' VDOT system for calculating training paces based on race performances.
/// </summary>
public class VdotCalculator
{
    // Main VDOT table mapping race performances to VDOT values
    private static readonly Dictionary<int, VdotEntry> VdotTable = new()
    {
        // VDOT values from 30 to 85 with corresponding race times for common distances
        { 30, new VdotEntry(30, "8:30", "9:11", "17:56", "19:19", "30:40", "63:46", "98:14", "2:21:04", "4:49:17") },
        { 31, new VdotEntry(31, "8:15", "8:55", "17:27", "18:48", "29:51", "62:03", "95:36", "2:17:21", "4:41:57") },
        { 32, new VdotEntry(32, "8:02", "8:41", "16:59", "18:18", "29:05", "60:26", "93:07", "2:13:49", "4:34:59") },
        { 33, new VdotEntry(33, "7:49", "8:27", "16:33", "17:50", "28:21", "58:54", "90:45", "2:10:27", "4:28:22") },
        { 34, new VdotEntry(34, "7:37", "8:14", "16:09", "17:24", "27:39", "57:26", "88:30", "2:07:16", "4:22:03") },
        { 35, new VdotEntry(35, "7:25", "8:01", "15:45", "16:58", "27:00", "56:03", "86:22", "2:04:13", "4:16:03") },
        { 36, new VdotEntry(36, "7:14", "7:49", "15:23", "16:34", "26:22", "54:44", "84:20", "2:01:19", "4:10:19") },
        { 37, new VdotEntry(37, "7:04", "7:38", "15:01", "16:11", "25:46", "53:29", "82:24", "1:58:34", "4:04:50") },
        { 38, new VdotEntry(38, "6:54", "7:27", "14:41", "15:49", "25:12", "52:17", "80:33", "1:55:55", "3:59:35") },
        { 39, new VdotEntry(39, "6:44", "7:17", "14:21", "15:29", "24:39", "51:09", "78:47", "1:53:24", "3:54:34") },
        { 40, new VdotEntry(40, "6:35", "7:07", "14:03", "15:09", "24:08", "50:03", "77:06", "1:50:59", "3:49:45") },
        { 41, new VdotEntry(41, "6:27", "6:58", "13:45", "14:49", "23:38", "49:01", "75:29", "1:48:40", "3:45:09") },
        { 42, new VdotEntry(42, "6:19", "6:49", "13:28", "14:31", "23:09", "48:01", "73:56", "1:46:27", "3:40:43") },
        { 43, new VdotEntry(43, "6:11", "6:41", "13:11", "14:13", "22:41", "47:04", "72:27", "1:44:20", "3:36:28") },
        { 44, new VdotEntry(44, "6:03", "6:32", "12:55", "13:56", "22:15", "46:09", "71:02", "1:42:17", "3:32:23") },
        { 45, new VdotEntry(45, "5:56", "6:25", "12:40", "13:40", "21:50", "45:16", "69:40", "1:40:20", "3:28:26") },
        { 46, new VdotEntry(46, "5:49", "6:17", "12:26", "13:25", "21:25", "44:25", "68:22", "1:38:27", "3:24:39") },
        { 47, new VdotEntry(47, "5:42", "6:10", "12:12", "13:10", "21:02", "43:36", "67:06", "1:36:38", "3:21:00") },
        { 48, new VdotEntry(48, "5:36", "6:03", "11:58", "12:55", "20:39", "42:50", "65:53", "1:34:53", "3:17:29") },
        { 49, new VdotEntry(49, "5:30", "5:56", "11:45", "12:41", "20:18", "42:04", "64:44", "1:33:12", "3:14:06") },
        { 50, new VdotEntry(50, "5:24", "5:50", "11:33", "12:28", "19:57", "41:21", "63:36", "1:31:35", "3:10:49") },
        { 51, new VdotEntry(51, "5:18", "5:44", "11:21", "12:15", "19:36", "40:39", "62:31", "1:30:02", "3:07:39") },
        { 52, new VdotEntry(52, "5:13", "5:38", "11:09", "12:02", "19:17", "39:59", "61:29", "1:28:31", "3:04:36") },
        { 53, new VdotEntry(53, "5:07", "5:32", "10:58", "11:50", "18:58", "39:20", "60:28", "1:27:04", "3:01:39") },
        { 54, new VdotEntry(54, "5:02", "5:27", "10:47", "11:39", "18:40", "38:42", "59:30", "1:25:40", "2:58:47") },
        { 55, new VdotEntry(55, "4:57", "5:21", "10:37", "11:28", "18:22", "38:06", "58:33", "1:24:18", "2:56:01") },
        { 56, new VdotEntry(56, "4:53", "5:16", "10:27", "11:17", "18:05", "37:31", "57:39", "1:23:00", "2:53:20") },
        { 57, new VdotEntry(57, "4:48", "5:11", "10:17", "11:06", "17:49", "36:57", "56:46", "1:21:43", "2:50:45") },
        { 58, new VdotEntry(58, "4:44", "5:06", "10:08", "10:56", "17:33", "36:24", "55:55", "1:20:30", "2:48:14") },
        { 59, new VdotEntry(59, "4:39", "5:02", "9:58", "10:46", "17:17", "35:52", "55:06", "1:19:18", "2:45:47") },
        { 60, new VdotEntry(60, "4:35", "4:57", "9:50", "10:37", "17:03", "35:22", "54:18", "1:18:09", "2:43:25") },
        { 61, new VdotEntry(61, "4:31", "4:53", "9:41", "10:27", "16:48", "34:52", "53:32", "1:17:02", "2:41:08") },
        { 62, new VdotEntry(62, "4:27", "4:49", "9:33", "10:18", "16:34", "34:23", "52:47", "1:15:57", "2:38:54") },
        { 63, new VdotEntry(63, "4:24", "4:45", "9:25", "10:10", "16:20", "33:55", "52:03", "1:14:54", "2:36:44") },
        { 64, new VdotEntry(64, "4:20", "4:41", "9:17", "10:01", "16:07", "33:28", "51:21", "1:13:53", "2:34:38") },
        { 65, new VdotEntry(65, "4:16", "4:37", "9:09", "9:53", "15:54", "33:01", "50:40", "1:12:53", "2:32:35") },
        { 66, new VdotEntry(66, "4:13", "4:33", "9:02", "9:45", "15:42", "32:35", "50:00", "1:11:56", "2:30:36") },
        { 67, new VdotEntry(67, "4:10", "4:30", "8:55", "9:37", "15:29", "32:11", "49:22", "1:11:00", "2:28:40") },
        { 68, new VdotEntry(68, "4:06", "4:26", "8:48", "9:30", "15:18", "31:46", "48:44", "1:10:05", "2:26:47") },
        { 69, new VdotEntry(69, "4:03", "4:23", "8:41", "9:23", "15:06", "31:23", "48:08", "1:09:12", "2:24:57") },
        { 70, new VdotEntry(70, "4:00", "4:19", "8:34", "9:16", "14:55", "31:00", "47:32", "1:08:21", "2:23:10") },
        { 71, new VdotEntry(71, "3:57", "4:16", "8:28", "9:09", "14:44", "30:38", "46:58", "1:07:31", "2:21:26") },
        { 72, new VdotEntry(72, "3:54", "4:13", "8:22", "9:02", "14:33", "30:16", "46:24", "1:06:42", "2:19:44") },
        { 73, new VdotEntry(73, "3:52", "4:10", "8:16", "8:55", "14:23", "29:55", "45:51", "1:05:54", "2:18:05") },
        { 74, new VdotEntry(74, "3:49", "4:07", "8:10", "8:49", "14:13", "29:34", "45:19", "1:05:08", "2:16:29") },
        { 75, new VdotEntry(75, "3:46", "4:04", "8:04", "8:43", "14:03", "29:14", "44:48", "1:04:23", "2:14:55") },
        { 76, new VdotEntry(76, "3:44", "4:02", "7:58", "8:37", "13:54", "28:55", "44:18", "1:03:39", "2:13:23") },
        { 77, new VdotEntry(77, "3:41", "3:58", "7:53", "8:31", "13:44", "28:36", "43:49", "1:02:56", "2:11:54") },
        { 78, new VdotEntry(78, "3:38.8", "3:56.2", "7:48", "8:25", "13:35", "28:17", "43:20", "1:02:15", "2:10:27") },
        { 79, new VdotEntry(79, "3:36.5", "3:53.7", "7:43", "8:20", "13:26", "27:59", "42:52", "1:01:34", "2:09:02") },
        { 80, new VdotEntry(80, "3:34.2", "3:51.2", "7:37.5", "8:14.2", "13:17.8", "27:41", "42:25", "1:00:54", "2:07:38") },
        { 81, new VdotEntry(81, "3:31.9", "3:48.7", "7:32.5", "8:08.9", "13:09.3", "27:24", "41:58", "1:00:15", "2:06:17") },
        { 82, new VdotEntry(82, "3:29.7", "3:46.4", "7:27.7", "8:03.7", "13:01.1", "27:07", "41:32", "59:38", "2:04:57") },
        { 83, new VdotEntry(83, "3:27.6", "3:44.0", "7:23.0", "7:58.6", "12:53.0", "26:51", "41:06", "59:01", "2:03:40") },
        { 84, new VdotEntry(84, "3:25.5", "3:41.8", "7:18.5", "7:53.6", "12:45.2", "26:34", "40:42", "58:25", "2:02:24") },
        { 85, new VdotEntry(85, "3:23.5", "3:39.6", "7:14.0", "7:48.8", "12:37.4", "26:19", "40:17", "57:50", "2:01:10") }
    };

    // Training paces lookup table (per VDOT value)
    private static readonly Dictionary<int, TrainingPaces> TrainingPacesTable = new()
    {
        // VDOT, Easy Pace Range (min-max per km), Marathon, Threshold, Interval, Repetition
        { 30, new TrainingPaces(30, "7:27-8:14", "7:03", "6:24", "5:52", "5:27") },
        { 31, new TrainingPaces(31, "7:16-8:02", "6:52", "6:14", "5:43", "5:18") },
        { 32, new TrainingPaces(32, "7:05-7:52", "6:40", "6:05", "5:34", "5:10") },
        { 33, new TrainingPaces(33, "6:55-7:41", "6:30", "5:56", "5:26", "5:03") },
        { 34, new TrainingPaces(34, "6:45-7:31", "6:20", "5:48", "5:18", "4:56") },
        { 35, new TrainingPaces(35, "6:36-7:21", "6:10", "5:40", "5:11", "4:49") },
        { 36, new TrainingPaces(36, "6:27-7:11", "6:01", "5:33", "5:04", "4:43") },
        { 37, new TrainingPaces(37, "6:19-7:02", "5:53", "5:26", "5:00", "4:37") },
        { 38, new TrainingPaces(38, "6:11-6:54", "5:45", "5:19", "4:54", "4:32") },
        { 39, new TrainingPaces(39, "6:03-6:46", "5:37", "5:12", "4:48", "4:26") },
        { 40, new TrainingPaces(40, "5:56-6:38", "5:29", "5:06", "4:42", "4:21") },
        { 41, new TrainingPaces(41, "5:49-6:31", "5:22", "5:00", "4:36", "4:16") },
        { 42, new TrainingPaces(42, "5:42-6:23", "5:16", "4:54", "4:31", "4:12") },
        { 43, new TrainingPaces(43, "5:35-6:16", "5:09", "4:49", "4:26", "4:07") },
        { 44, new TrainingPaces(44, "5:29-6:10", "5:03", "4:43", "4:21", "4:03") },
        { 45, new TrainingPaces(45, "5:23-6:03", "4:57", "4:38", "4:16", "3:59") },
        { 46, new TrainingPaces(46, "5:17-5:57", "4:51", "4:33", "4:12", "3:55") },
        { 47, new TrainingPaces(47, "5:12-5:51", "4:46", "4:29", "4:07", "3:51") },
        { 48, new TrainingPaces(48, "5:07-5:45", "4:41", "4:24", "4:03", "3:48") },
        { 49, new TrainingPaces(49, "5:01-5:40", "4:36", "4:20", "3:59", "3:44") },
        { 50, new TrainingPaces(50, "4:56-5:34", "4:31", "4:15", "3:55", "3:41") },
        { 51, new TrainingPaces(51, "4:52-5:29", "4:27", "4:11", "3:52", "3:38") },
        { 52, new TrainingPaces(52, "4:47-5:24", "4:22", "4:07", "3:48", "3:35") },
        { 53, new TrainingPaces(53, "4:43-5:19", "4:18", "4:04", "3:44", "3:31") },
        { 54, new TrainingPaces(54, "4:38-5:14", "4:14", "4:00", "3:41", "3:28") },
        { 55, new TrainingPaces(55, "4:34-5:10", "4:10", "3:56", "3:37", "3:25") },
        { 56, new TrainingPaces(56, "4:30-5:05", "4:06", "3:53", "3:34", "3:22") },
        { 57, new TrainingPaces(57, "4:26-5:01", "4:03", "3:50", "3:31", "3:19") },
        { 58, new TrainingPaces(58, "4:22-4:57", "3:59", "3:46", "3:28", "3:16") },
        { 59, new TrainingPaces(59, "4:19-4:53", "3:56", "3:43", "3:25", "3:14") },
        { 60, new TrainingPaces(60, "4:15-4:49", "3:52", "3:40", "3:23", "3:11") },
        { 61, new TrainingPaces(61, "4:11-4:45", "3:49", "3:37", "3:20", "3:09") },
        { 62, new TrainingPaces(62, "4:08-4:41", "3:46", "3:34", "3:17", "3:06") },
        { 63, new TrainingPaces(63, "4:05-4:38", "3:43", "3:32", "3:15", "3:04") },
        { 64, new TrainingPaces(64, "4:02-4:34", "3:40", "3:29", "3:12", "3:02") },
        { 65, new TrainingPaces(65, "3:59-4:31", "3:37", "3:26", "3:10", "3:00") },
        { 66, new TrainingPaces(66, "3:56-4:28", "3:34", "3:24", "3:08", "2:58") },
        { 67, new TrainingPaces(67, "3:53-4:24", "3:31", "3:21", "3:05", "2:56") },
        { 68, new TrainingPaces(68, "3:50-4:21", "3:29", "3:19", "3:03", "2:54") },
        { 69, new TrainingPaces(69, "3:47-4:18", "3:26", "3:16", "3:01", "2:52") },
        { 70, new TrainingPaces(70, "3:44-4:15", "3:24", "3:14", "2:59", "2:50") },
        { 71, new TrainingPaces(71, "3:42-4:12", "3:21", "3:12", "2:57", "2:48") },
        { 72, new TrainingPaces(72, "3:40-4:10", "3:19", "3:10", "2:55", "2:46") },
        { 73, new TrainingPaces(73, "3:37-4:07", "3:16", "3:08", "2:53", "2:44") },
        { 74, new TrainingPaces(74, "3:34-4:04", "3:14", "3:06", "2:51", "2:43") },
        { 75, new TrainingPaces(75, "3:32-4:01", "3:12", "3:04", "2:49", "2:41") },
        { 76, new TrainingPaces(76, "3:30-3:58", "3:10", "3:02", "2:48", "2:39") },
        { 77, new TrainingPaces(77, "3:28-3:56", "3:08", "3:00", "2:46", "2:37") },
        { 78, new TrainingPaces(78, "3:25-3:53", "3:06", "2:58", "2:44", "2:36") },
        { 79, new TrainingPaces(79, "3:23-3:51", "3:03", "2:56", "2:42", "2:34") },
        { 80, new TrainingPaces(80, "3:21-3:49", "3:01", "2:54", "2:41", "2:33") },
        { 81, new TrainingPaces(81, "3:19-3:46", "3:00", "2:53", "2:39", "2:31") },
        { 82, new TrainingPaces(82, "3:17-3:44", "2:58", "2:51", "2:38", "2:30") },
        { 83, new TrainingPaces(83, "3:15-3:42", "2:56", "2:49", "2:36", "2:28") },
        { 84, new TrainingPaces(84, "3:13-3:40", "2:54", "2:48", "2:35", "2:27") },
        { 85, new TrainingPaces(85, "3:11-3:38", "2:52", "2:46", "2:33", "2:25") }
    };

    /// <summary>
    /// Get VDOT value based on a race performance
    /// </summary>
    /// <param name="distance">Race distance in kilometers (5, 10, 21.1, or 42.2)</param>
    /// <param name="time">Race time as TimeSpan</param>
    /// <returns>VDOT value</returns>
    private static int GetVdotFromRacePerformance(decimal distance, TimeSpan time)
    {
        Func<VdotEntry, TimeSpan> getVdotTime = distance switch
        {
            1.5m => entry => entry.FifTeenHundred,
            1.6m => entry => entry.Mile,
            3m => entry => entry.ThreeKm,
            3.2m => entry => entry.TwoMile,
            5m => entry => entry.FiveKm,
            10m => entry => entry.TenKm,
            15m => entry => entry.FifteenKm,
            21.1m => entry => entry.HalfMarathon,
            42.2m => entry => entry.Marathon,
            _ => throw new ArgumentException("Unsupported distance. Supported distances are 1.5K, 1 mile, 3K, 2 mile, 5K, 10K, 15K, half marathon, and marathon.")
        };

        foreach (var vdotEntry in VdotTable.OrderByDescending(v => v.Key))
        {
            // Compare race time to VDOT race times
            TimeSpan vdotTime = getVdotTime(vdotEntry.Value);

            if (time <= vdotTime)
            {
                return vdotEntry.Key;
            }
        }

        // If no match, return the lowest VDOT value
        return VdotTable.Keys.Min();
    }

    /// <summary>
    /// Get training paces based on a VDOT value
    /// </summary>
    /// <param name="vdot">VDOT value</param>
    /// <returns>Training paces object</returns>
    private static TrainingPaces GetTrainingPaces(int vdot)
    {
        // Clamp VDOT value to within our table range
        vdot = Math.Clamp(vdot, VdotTable.Keys.Min(), VdotTable.Keys.Max());

        if (TrainingPacesTable.TryGetValue(vdot, out var paces))
        {
            return paces;
        }

        throw new KeyNotFoundException($"No training paces found for VDOT {vdot}");
    }

    /// <summary>
    /// Get training paces directly from a race performance
    /// </summary>
    /// <param name="distance">Race distance in kilometers (5, 10, 21.1, or 42.2)</param>
    /// <param name="time">Race time as TimeSpan</param>
    /// <returns>Training paces object</returns>
    public static TrainingPaces GetTrainingPacesFromRace(decimal distance, TimeSpan time)
    {
        int vdot = GetVdotFromRacePerformance(distance, time);

        return GetTrainingPaces(vdot);
    }
}
