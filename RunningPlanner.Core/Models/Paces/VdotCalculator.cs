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
            { 30, new VdotEntry(30, "26:07", "54:10", "1:59:25", "4:08:24") },
            { 31, new VdotEntry(31, "25:16", "52:31", "1:55:44", "4:01:52") },
            { 32, new VdotEntry(32, "24:29", "50:59", "1:52:16", "3:55:40") },
            { 33, new VdotEntry(33, "23:45", "49:32", "1:48:59", "3:49:44") },
            { 34, new VdotEntry(34, "23:04", "48:09", "1:45:54", "3:44:05") },
            { 35, new VdotEntry(35, "22:25", "46:51", "1:42:59", "3:38:41") },
            { 36, new VdotEntry(36, "21:49", "45:37", "1:40:14", "3:33:32") },
            { 37, new VdotEntry(37, "21:15", "44:27", "1:37:38", "3:28:36") },
            { 38, new VdotEntry(38, "20:43", "43:21", "1:35:09", "3:23:53") },
            { 39, new VdotEntry(39, "20:12", "42:18", "1:32:48", "3:19:22") },
            { 40, new VdotEntry(40, "19:44", "41:18", "1:30:34", "3:15:03") },
            { 41, new VdotEntry(41, "19:17", "40:21", "1:28:26", "3:10:54") },
            { 42, new VdotEntry(42, "18:51", "39:27", "1:26:24", "3:06:55") },
            { 43, new VdotEntry(43, "18:27", "38:36", "1:24:28", "3:03:05") },
            { 44, new VdotEntry(44, "18:04", "37:47", "1:22:38", "2:59:25") },
            { 45, new VdotEntry(45, "17:42", "37:00", "1:20:53", "2:55:53") },
            { 46, new VdotEntry(46, "17:20", "36:15", "1:19:12", "2:52:29") },
            { 47, new VdotEntry(47, "17:00", "35:33", "1:17:36", "2:49:13") },
            { 48, new VdotEntry(48, "16:41", "34:52", "1:16:04", "2:46:04") },
            { 49, new VdotEntry(49, "16:22", "34:13", "1:14:36", "2:43:02") },
            { 50, new VdotEntry(50, "16:04", "33:36", "1:13:11", "2:40:06") },
            { 51, new VdotEntry(51, "15:47", "33:00", "1:11:49", "2:37:16") },
            { 52, new VdotEntry(52, "15:31", "32:26", "1:10:31", "2:34:33") },
            { 53, new VdotEntry(53, "15:15", "31:53", "1:09:15", "2:31:55") },
            { 54, new VdotEntry(54, "15:00", "31:21", "1:08:02", "2:29:22") },
            { 55, new VdotEntry(55, "14:46", "30:51", "1:06:52", "2:26:55") },
            { 56, new VdotEntry(56, "14:32", "30:22", "1:05:44", "2:24:32") },
            { 57, new VdotEntry(57, "14:19", "29:54", "1:04:38", "2:22:14") },
            { 58, new VdotEntry(58, "14:06", "29:27", "1:03:35", "2:20:00") },
            { 59, new VdotEntry(59, "13:53", "29:01", "1:02:33", "2:17:50") },
            { 60, new VdotEntry(60, "13:41", "28:35", "1:01:34", "2:15:44") },
            { 61, new VdotEntry(61, "13:30", "28:11", "1:00:36", "2:13:42") },
            { 62, new VdotEntry(62, "13:18", "27:47", "59:40", "2:11:43") },
            { 63, new VdotEntry(63, "13:08", "27:24", "58:46", "2:09:47") },
            { 64, new VdotEntry(64, "12:57", "27:02", "57:53", "2:07:55") },
            { 65, new VdotEntry(65, "12:47", "26:40", "57:01", "2:06:06") },
            { 66, new VdotEntry(66, "12:37", "26:19", "56:11", "2:04:20") },
            { 67, new VdotEntry(67, "12:28", "25:59", "55:23", "2:02:37") },
            { 68, new VdotEntry(68, "12:19", "25:39", "54:35", "2:00:56") },
            { 69, new VdotEntry(69, "12:10", "25:20", "53:49", "1:59:18") },
            { 70, new VdotEntry(70, "12:01", "25:01", "53:04", "1:57:42") },
            { 71, new VdotEntry(71, "11:53", "24:43", "52:20", "1:56:09") },
            { 72, new VdotEntry(72, "11:44", "24:25", "51:38", "1:54:38") },
            { 73, new VdotEntry(73, "11:36", "24:08", "50:56", "1:53:09") },
            { 74, new VdotEntry(74, "11:29", "23:51", "50:16", "1:51:42") },
            { 75, new VdotEntry(75, "11:21", "23:35", "49:36", "1:50:17") },
            { 76, new VdotEntry(76, "11:14", "23:19", "48:58", "1:48:54") },
            { 77, new VdotEntry(77, "11:07", "23:03", "48:20", "1:47:32") },
            { 78, new VdotEntry(78, "11:00", "22:48", "47:44", "1:46:13") },
            { 79, new VdotEntry(79, "10:53", "22:33", "47:08", "1:44:55") },
            { 80, new VdotEntry(80, "10:47", "22:19", "46:33", "1:43:38") },
            { 81, new VdotEntry(81, "10:40", "22:05", "45:59", "1:42:24") },
            { 82, new VdotEntry(82, "10:34", "21:51", "45:26", "1:41:10") },
            { 83, new VdotEntry(83, "10:28", "21:37", "44:53", "1:39:58") },
            { 84, new VdotEntry(84, "10:22", "21:24", "44:22", "1:38:48") },
            { 85, new VdotEntry(85, "10:16", "21:11", "43:51", "1:37:38") }
        };

        // Training paces lookup table (per VDOT value)
        private static readonly Dictionary<int, TrainingPaces> TrainingPacesTable = new()
        {
            // VDOT, Easy Pace (min:sec), Marathon Pace, Threshold Pace, Interval Pace, Repetition Pace (all per km)
            { 30, new TrainingPaces(30, "8:15-9:00", "7:52", "7:08", "6:36", "6:08") },
            { 31, new TrainingPaces(31, "8:05-8:50", "7:42", "6:59", "6:28", "6:00") },
            { 32, new TrainingPaces(32, "7:55-8:40", "7:33", "6:51", "6:21", "5:53") },
            { 33, new TrainingPaces(33, "7:46-8:30", "7:24", "6:43", "6:13", "5:46") },
            { 34, new TrainingPaces(34, "7:37-8:20", "7:15", "6:35", "6:06", "5:40") },
            { 35, new TrainingPaces(35, "7:28-8:10", "7:07", "6:27", "5:59", "5:34") },
            { 36, new TrainingPaces(36, "7:20-8:00", "6:59", "6:20", "5:53", "5:28") },
            { 37, new TrainingPaces(37, "7:12-7:50", "6:52", "6:13", "5:46", "5:22") },
            { 38, new TrainingPaces(38, "7:04-7:45", "6:44", "6:06", "5:40", "5:17") },
            { 39, new TrainingPaces(39, "6:57-7:35", "6:37", "6:00", "5:35", "5:12") },
            { 40, new TrainingPaces(40, "6:51-7:30", "6:30", "5:54", "5:29", "5:08") },
            { 41, new TrainingPaces(41, "6:45-7:20", "6:24", "5:48", "5:24", "5:03") },
            { 42, new TrainingPaces(42, "6:38-7:15", "6:18", "5:43", "5:19", "4:58") },
            { 43, new TrainingPaces(43, "6:32-7:10", "6:13", "5:38", "5:14", "4:54") },
            { 44, new TrainingPaces(44, "6:26-7:05", "6:07", "5:33", "5:10", "4:50") },
            { 45, new TrainingPaces(45, "6:20-7:00", "6:01", "5:28", "5:05", "4:46") },
            { 46, new TrainingPaces(46, "6:15-6:55", "5:55", "5:23", "5:01", "4:42") },
            { 47, new TrainingPaces(47, "6:10-6:50", "5:50", "5:18", "4:58", "4:38") },
            { 48, new TrainingPaces(48, "6:05-6:45", "5:45", "5:14", "4:54", "4:34") },
            { 49, new TrainingPaces(49, "6:00-6:40", "5:40", "5:10", "4:50", "4:31") },
            { 50, new TrainingPaces(50, "5:55-6:35", "5:35", "5:05", "4:47", "4:27") },
            { 51, new TrainingPaces(51, "5:52-6:30", "5:31", "5:01", "4:43", "4:24") },
            { 52, new TrainingPaces(52, "5:48-6:23", "5:27", "4:57", "4:40", "4:21") },
            { 53, new TrainingPaces(53, "5:43-6:18", "5:22", "4:53", "4:36", "4:17") },
            { 54, new TrainingPaces(54, "5:38-6:13", "5:18", "4:50", "4:33", "4:14") },
            { 55, new TrainingPaces(55, "5:33-6:08", "5:14", "4:46", "4:30", "4:11") },
            { 56, new TrainingPaces(56, "5:29-6:03", "5:10", "4:43", "4:26", "4:08") },
            { 57, new TrainingPaces(57, "5:25-5:58", "5:06", "4:39", "4:23", "4:05") },
            { 58, new TrainingPaces(58, "5:21-5:53", "5:02", "4:36", "4:20", "4:02") },
            { 59, new TrainingPaces(59, "5:17-5:49", "4:58", "4:33", "4:17", "3:59") },
            { 60, new TrainingPaces(60, "5:13-5:45", "4:55", "4:30", "4:14", "3:57") },
            { 61, new TrainingPaces(61, "5:09-5:40", "4:51", "4:27", "4:11", "3:54") },
            { 62, new TrainingPaces(62, "5:06-5:36", "4:48", "4:24", "4:08", "3:51") },
            { 63, new TrainingPaces(63, "5:02-5:32", "4:45", "4:21", "4:05", "3:48") },
            { 64, new TrainingPaces(64, "4:58-5:28", "4:41", "4:18", "4:03", "3:46") },
            { 65, new TrainingPaces(65, "4:55-5:24", "4:38", "4:15", "4:00", "3:43") },
            { 66, new TrainingPaces(66, "4:51-5:20", "4:35", "4:12", "3:57", "3:41") },
            { 67, new TrainingPaces(67, "4:48-5:16", "4:32", "4:09", "3:55", "3:38") },
            { 68, new TrainingPaces(68, "4:45-5:12", "4:29", "4:07", "3:52", "3:36") },
            { 69, new TrainingPaces(69, "4:41-5:08", "4:26", "4:04", "3:50", "3:34") },
            { 70, new TrainingPaces(70, "4:38-5:05", "4:23", "4:01", "3:47", "3:31") },
            { 71, new TrainingPaces(71, "4:35-5:01", "4:20", "3:59", "3:45", "3:29") },
            { 72, new TrainingPaces(72, "4:32-4:58", "4:17", "3:56", "3:43", "3:27") },
            { 73, new TrainingPaces(73, "4:29-4:54", "4:15", "3:54", "3:40", "3:25") },
            { 74, new TrainingPaces(74, "4:26-4:51", "4:12", "3:52", "3:38", "3:23") },
            { 75, new TrainingPaces(75, "4:23-4:48", "4:09", "3:49", "3:36", "3:21") },
            { 76, new TrainingPaces(76, "4:20-4:45", "4:07", "3:47", "3:34", "3:19") },
            { 77, new TrainingPaces(77, "4:17-4:42", "4:04", "3:45", "3:32", "3:17") },
            { 78, new TrainingPaces(78, "4:15-4:39", "4:02", "3:42", "3:30", "3:15") },
            { 79, new TrainingPaces(79, "4:12-4:36", "3:59", "3:40", "3:28", "3:13") },
            { 80, new TrainingPaces(80, "4:09-4:33", "3:57", "3:38", "3:26", "3:11") },
            { 81, new TrainingPaces(81, "4:07-4:30", "3:55", "3:36", "3:24", "3:09") },
            { 82, new TrainingPaces(82, "4:04-4:28", "3:52", "3:34", "3:22", "3:08") },
            { 83, new TrainingPaces(83, "4:02-4:25", "3:50", "3:32", "3:20", "3:06") },
            { 84, new TrainingPaces(84, "3:59-4:22", "3:48", "3:30", "3:18", "3:04") },
            { 85, new TrainingPaces(85, "3:57-4:19", "3:46", "3:28", "3:16", "3:02") }
        };

        /// <summary>
        /// Get VDOT value based on a race performance
        /// </summary>
        /// <param name="distance">Race distance in kilometers (5, 10, 21.1, or 42.2)</param>
        /// <param name="time">Race time as TimeSpan</param>
        /// <returns>VDOT value</returns>
        public static int GetVdotFromRacePerformance(decimal distance, TimeSpan time)
        {
            // Convert time to the appropriate format for comparison
            string timeString = distance switch
            {
                5m => time.TotalMinutes < 60 ? time.ToString(@"mm\:ss") : time.ToString(@"h\:mm\:ss"),
                10m => time.ToString(@"mm\:ss"),
                21.1m => time.ToString(@"h\:mm\:ss"),
                42.2m => time.ToString(@"h\:mm\:ss"),
                _ => throw new ArgumentException("Supported distances are 5, 10, 21.1, and 42.2 kilometers.")
            };

            foreach (var vdotEntry in VdotTable.OrderByDescending(v => v.Key))
            {
                // Compare race time to VDOT race times
                TimeSpan vdotTime = distance switch
                {
                    5m => vdotEntry.Value.FiveKm,
                    10m => vdotEntry.Value.TenKm,
                    21.1m => vdotEntry.Value.HalfMarathon,
                    42.2m => vdotEntry.Value.Marathon,
                    _ => throw new ArgumentException("Supported distances are 5, 10, 21.1, and 42.2 kilometers.")
                };

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
        public static TrainingPaces GetTrainingPaces(int vdot)
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
