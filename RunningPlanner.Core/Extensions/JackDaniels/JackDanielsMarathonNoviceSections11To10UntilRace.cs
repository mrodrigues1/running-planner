// using RunningPlanner.Core.Models;
//
// public static class JackDanielsMarathonNoviceSections11To10UntilRace
// {
//     /// <summary>
//     /// Session A: 10:00 E + 5:00 W + 5 ST + 5:00 W + 2 × 10:00 E w/5:00 W
//     /// </summary>
//     public static Workout CreateSessionA(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default,
//         (TimeSpan min, TimeSpan max) stridePaceRange = default)
//     {
//         // Create a list to hold all steps in the workout
//         var steps = new List<Step>();
//
//         // Step 1: 10:00 Easy running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(10)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             ));
//
//         // Step 2: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 3: 5 strides (implement as a short fast segment with recovery)
//         var strideSteps = new List<SimpleStep>();
//
//         var strideRepeats = 5;
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
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             );
//         }
//
//         steps.Add(Step.FromRepeat(Repeat.Create(strideSteps.Count, strideSteps)));
//
//         // Step 4: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 5: 2 × 10:00 E w/5:00 W
//         var runWalkRepeats = 2;
//         var runWalkSteps = new List<SimpleStep>();
//
//         for (int i = 0; i < runWalkRepeats; i++)
//         {
//             // 10:00 E pace run
//             runWalkSteps.Add(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(10)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             );
//
//             // 5:00 W recovery
//             runWalkSteps.Add(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             );
//         }
//
//         steps.Add(Step.FromRepeat(Repeat.Create(runWalkSteps.Count, runWalkSteps)));
//
//         return Workout.Create(WorkoutType.WalkRun, steps);
//     }
//
//     /// <summary>
//     /// Session B: If you train today, repeat the A workout of this week.
//     /// </summary>
//     public static Workout CreateSessionB(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default)
//     {
//         return CreateSessionA(walkPaceRange, easyPaceRange);
//     }
//
//     /// <summary>
//     /// Session C: 5:00 E + 5:00 W + 20:00 E + 5:00 W + 5:00 T + 5:00 W + 5:00 E + 5:00 W
//     /// </summary>
//     public static Workout CreateSessionC(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default,
//         (TimeSpan min, TimeSpan max) tempoPaceRange = default)
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
//         // Step 2: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 3: 20:00 Easy running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(20)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             ));
//
//         // Step 4: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 5: 5:00 Tempo running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(tempoPaceRange.min, tempoPaceRange.max))
//             ));
//
//         // Step 6: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 7: 5:00 Easy running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             ));
//
//         // Step 8: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         return Workout.Create(WorkoutType.WalkRun, steps);
//     }
//
//     /// <summary>
//     /// Session D: If you run today, do 3 × 10:00 E w/5:00 W (may do less than 5:00 recoveries if desired)
//     /// </summary>
//     public static Workout CreateSessionD(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default)
//     {
//         // Create a list to hold all steps in the workout
//         var steps = new List<Step>();
//
//         // Step 5: 3 × 10:00 E w/5:00 W
//         var runWalkRepeats = 3;
//         var runWalkSteps = new List<SimpleStep>();
//
//         for (int i = 0; i < runWalkRepeats; i++)
//         {
//             // 10:00 E pace run
//             runWalkSteps.Add(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(10)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             );
//
//             // 5:00 W recovery
//             runWalkSteps.Add(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             );
//         }
//
//         steps.Add(Step.FromRepeat(Repeat.Create(runWalkSteps.Count, runWalkSteps)));
//
//         return Workout.Create(WorkoutType.WalkRun, steps);
//     }
//
//     /// <summary>
//     /// Session E (–11 weeks): 10:00 E + 5:00 W + 5 ST + 5:00 W + 20:00 T + 5:00W + 10 E
//     /// </summary>
//     public static Workout CreateSessionE11WeeksOut(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default,
//         (TimeSpan min, TimeSpan max) stridePaceRange = default,
//         (TimeSpan min, TimeSpan max) tempoPaceRange = default)
//     {
//         // Create a list to hold all steps in the workout
//         var steps = new List<Step>();
//
//         // Step 1: 10:00 Easy running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(10)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             ));
//
//         // Step 2: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 3: 5 strides (implement as a short fast segment with recovery)
//         var strideSteps = new List<SimpleStep>();
//
//         var strideRepeats = 5;
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
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             );
//         }
//
//         steps.Add(Step.FromRepeat(Repeat.Create(strideSteps.Count, strideSteps)));
//
//         // Step 4: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 5: 20:00 Tempo running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(20)),
//                     IntensityTarget.Pace(tempoPaceRange.min, tempoPaceRange.max))
//             ));
//
//         // Step 6: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))));
//
//         // Step 7: 10:00 E pace run
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(10)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))));
//
//         return Workout.Create(WorkoutType.WalkRun, steps);
//     }
//
//     /// <summary>
//     /// Session E (–10 weeks): 10:00 E + 5:00 W + 5 ST + 5:00 W + 20:00 T + 5:00W + 20 E
//     /// </summary>
//     public static Workout CreateSessionE10WeeksOut(
//         (TimeSpan min, TimeSpan max) walkPaceRange = default,
//         (TimeSpan min, TimeSpan max) easyPaceRange = default,
//         (TimeSpan min, TimeSpan max) tempoPaceRange = default,
//         (TimeSpan min, TimeSpan max) stridePaceRange = default)
//     {
//         // Create a list to hold all steps in the workout
//         var steps = new List<Step>();
//
//         // Step 1: 10:00 Easy running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(10)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))
//             ));
//
//         // Step 2: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 3: 5 strides (implement as a short fast segment with recovery)
//         var strideSteps = new List<SimpleStep>();
//
//         var strideRepeats = 5;
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
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             );
//         }
//
//         steps.Add(Step.FromRepeat(Repeat.Create(strideSteps.Count, strideSteps)));
//
//         // Step 4: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))
//             ));
//
//         // Step 5: 20:00 Tempo running
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(20)),
//                     IntensityTarget.Pace(tempoPaceRange.min, tempoPaceRange.max))
//             ));
//
//         // Step 6: 5:00 Walking recovery
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Walk,
//                     Duration.ForTime(TimeSpan.FromMinutes(5)),
//                     IntensityTarget.Pace(walkPaceRange.min, walkPaceRange.max))));
//
//         // Step 7: 20:00 E pace run
//         steps.Add(
//             Step.FromSimpleStep(
//                 SimpleStep.Create(
//                     StepType.Run,
//                     Duration.ForTime(TimeSpan.FromMinutes(20)),
//                     IntensityTarget.Pace(easyPaceRange.min, easyPaceRange.max))));
//
//         return Workout.Create(WorkoutType.WalkRun, steps);
//     }
//
//     /// <summary>
//     /// Creates a workout for Session E based on the weeks remaining until race
//     /// </summary>
//     public static Workout CreateSessionE(int weeksUntilRace)
//     {
//         return weeksUntilRace switch
//         {
//             11 => CreateSessionE11WeeksOut(),
//             10 => CreateSessionE10WeeksOut(),
//             _ => throw new ArgumentOutOfRangeException(
//                 nameof(weeksUntilRace),
//                 "This factory only handles weeks 11-10 until race.")
//         };
//     }
// }
