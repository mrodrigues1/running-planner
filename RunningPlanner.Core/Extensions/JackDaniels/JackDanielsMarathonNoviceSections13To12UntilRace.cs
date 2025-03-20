// using RunningPlanner.Core.Models;
//
// public static class JackDanielsMarathonNoviceSections13To12UntilRace
// {
//     /// <summary>
//     /// Session A: 5:00 E + 3:00 W + 5 × 3:00 T pace, w/2:00 W following each T run + 10 ST
//     /// </summary>
//     public static Workout CreateSessionA(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default,
//         (TimeSpan min, TimeSpan max) tempoPaceRange = default,
//         (TimeSpan min, TimeSpan max) stridePaceRange = default)
//     {
//         // Create a list to hold all steps in the workout
//         var steps = new List<Step>();
//
//         // Step 1: 5:00 Easy running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             ));
//
//         // Step 2: 3:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(3)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 3: 5 × (3:00 T pace + 2:00 W)
//         var tempoRepeats = 5;
//         var tempoAndRecoverySteps = new List<SimpleStep>();
//
//         for (int i = 0; i < tempoRepeats; i++)
//         {
//             // 3:00 T pace run
//             tempoAndRecoverySteps.Add(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(3)),
//                     IntensityTarget.Pace(tempoPaceRange.min, tempoPaceRange.max))
//             );
//
//             // 2:00 W recovery
//             tempoAndRecoverySteps.Add(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(2)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             );
//         }
//
//         steps.Add(Step.FromRepeat(Repeat.Create(tempoAndRecoverySteps.Count, tempoAndRecoverySteps)));
//
//         // Step 4: 10 strides (implement as a short fast segment with recovery)
//         var strideSteps = new List<SimpleStep>();
//
//         var strideRepeats = 10;
//
//         for (int i = 0; i < strideRepeats; i++)
//         {
//             // Approximately 20 seconds of fast running for each stride
//             strideSteps.Add(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromSeconds(20)),
//                     IntensityTarget.Pace(stridePaceRange.min, stridePaceRange.max))
//             );
//
//             // 45-60 seconds of recovery
//             strideSteps.Add(
//                 SimpleStep.Create(
//                     StepType.Recover,
//                     Duration.ForTime(TimeSpan.FromSeconds(50)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             );
//         }
//
//         steps.Add(Step.FromRepeat(Repeat.Create(strideSteps.Count, strideSteps)));
//
//         return Workout.Create(WorkoutType.Intervals, steps);
//     }
//
//     /// <summary>
//     /// Session B: 3 × 10:00 E w/5:00 W (take less than 5:00 W if feeling good)
//     /// </summary>
//     public static Workout CreateSessionB(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default)
//     {
//         // Create a simple run-walk workout with 3 repeats of 10:00 easy running followed by 5:00 walking
//         var steps = new List<SimpleStep>();
//
//         var runWalkRepeats = 3;
//
//         for (int i = 0; i < runWalkRepeats; i++)
//         {
//             // 10:00 Easy running
//             steps.Add(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(10)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             );
//
//             // 5:00 Walking recovery (note: coach says this can be shortened if feeling good)
//             steps.Add(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             );
//         }
//
//         return Workout.Create(WorkoutType.WalkRun, Step.FromRepeat(Repeat.Create(steps.Count, steps)));
//     }
//
//     /// <summary>
//     /// Session C: Repeat of Session A
//     /// </summary>
//     public static Workout CreateSessionC(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default,
//         (TimeSpan min, TimeSpan max) tempoPaceRange = default,
//         (TimeSpan min, TimeSpan max) stridePaceRange = default)
//     {
//         // Simply call the existing implementation of Session A
//         return CreateSessionA(
//             walkPaceRange,
//             easyPaceRange,
//             tempoPaceRange,
//             stridePaceRange);
//     }
//
//     /// <summary>
//     /// Session D: 3 × 10:00 E w/5:00 W (may take less W if not that much recovery is needed)
//     /// </summary>
//     public static Workout CreateSessionD(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default)
//     {
//         // This is essentially the same as Session B
//         return CreateSessionB(walkPaceRange, easyPaceRange);
//     }
//
//     /// <summary>
//     /// Session E (–13 weeks): 5:00 E + 5:00 W + 3 × 5:00 T w/2:00 W recoveries + 15:00 E + 4:00 W
//     /// </summary>
//     public static Workout CreateSessionE13WeeksOut(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default,
//         (TimeSpan min, TimeSpan max) tempoPaceRange = default)
//     {
//         var steps = new List<Step>();
//
//         // Step 1: 5:00 Easy running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             ));
//
//         // Step 2: 5:00 Walking
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 3: 3 × (5:00 T pace + 2:00 W recovery)
//         var tempoAndRecoverySteps = new List<SimpleStep>();
//
//         var tempoRepeats = 3;
//
//         for (int i = 0; i < tempoRepeats; i++)
//         {
//             // 5:00 Tempo pace run
//             tempoAndRecoverySteps.Add(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(tempoPaceRange.min, tempoPaceRange.max))
//             );
//
//             // 2:00 Walking recovery
//             tempoAndRecoverySteps.Add(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(2)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             );
//         }
//
//         steps.Add(Step.FromRepeat(Repeat.Create(tempoAndRecoverySteps.Count, tempoAndRecoverySteps)));
//
//         // Step 4: 15:00 Easy running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(15)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             ));
//
//         // Step 5: 4:00 Walking cooldown
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(4)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         return Workout.Create(WorkoutType.Intervals, steps);
//     }
//
//     /// <summary>
//     /// Session E (–12 weeks): 5:00 E + 5:00 W + 2 × 5:00 T w/2:00 W recoveries + 25:00-30:00 E + 6:00 W
//     /// </summary>
//     public static Workout CreateSessionE12WeeksOut(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default,
//         (TimeSpan min, TimeSpan max) tempoPaceRange = default,
//         (TimeSpan min, TimeSpan max) stridePaceRange = default)
//     {
//         var steps = new List<Step>();
//
//         // Step 1: 5:00 Easy running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             ));
//
//         // Step 2: 5:00 Walking
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 3: 2 × (5:00 T pace + 2:00 W recovery)
//         var tempoAndRecoverySteps = new List<SimpleStep>();
//
//         for (int i = 0; i < 2; i++) // Note: only 2 repeats in this workout (vs 3 in the 13-weeks workout)
//         {
//             // 5:00 Tempo pace run
//             tempoAndRecoverySteps.Add(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(tempoPaceRange.min, tempoPaceRange.max))
//             );
//
//             // 2:00 Walking recovery
//             tempoAndRecoverySteps.Add(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(2)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             );
//         }
//
//         steps.Add(Step.FromRepeat(Repeat.Create(tempoAndRecoverySteps.Count, tempoAndRecoverySteps)));
//
//         // Step 4: 25:00-30:00 Easy running (using average of 27:30)
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(27.5)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             ));
//
//         // Step 5: 6:00 Walking cooldown
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(6)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         return Workout.Create(WorkoutType.Intervals, steps);
//     }
//
//     /// <summary>
//     /// Creates a workout for Session E based on the weeks remaining until race
//     /// </summary>
//     public static Workout CreateSessionE(int weeksUntilRace,
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default,
//         (TimeSpan min, TimeSpan max) tempoPaceRange = default,
//         (TimeSpan min, TimeSpan max) stridePaceRange = default)
//     {
//         return weeksUntilRace switch
//         {
//             13 => CreateSessionE13WeeksOut(walkPaceRange, easyPaceRange, tempoPaceRange),
//             12 => CreateSessionE12WeeksOut(walkPaceRange, easyPaceRange, tempoPaceRange, stridePaceRange),
//             _ => throw new ArgumentOutOfRangeException(
//                 nameof(weeksUntilRace),
//                 "This factory only handles weeks 13-12 until race.")
//         };
//     }
// }
