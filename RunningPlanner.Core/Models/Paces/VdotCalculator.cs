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
