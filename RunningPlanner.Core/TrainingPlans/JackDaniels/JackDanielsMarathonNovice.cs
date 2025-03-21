﻿// using RunningPlanner.Core.Models;
//
// namespace RunningPlanner.Core.TrainingPlans.JackDaniels;
//
// /// <summary>
// /// Implementation of Jack Daniels' Novice Marathon Training Plan.
// /// </summary>
// public class JackDanielsMarathonNovice
// {
//     public TrainingPlan TrainingPlan { get; private set; }
//
//     public JackDanielsMarathonNovice()
//     {
//         TrainingPlan = GenerateDefaultTrainingPlan();
//     }
//
//     private TrainingPlan GenerateDefaultTrainingPlan()
//     {
//         return TrainingPlan.Create(
//             new List<TrainingWeek>
//             {
//                 GenerateWeek1(),
//                 GenerateWeek2(),
//                 GenerateWeek3(),
//                 GenerateWeek4(),
//                 GenerateWeek5(),
//                 GenerateWeek6(),
//                 GenerateWeek7()
//                 // Add remaining weeks here as needed
//             });
//     }
//
//     /// <summary>
//     /// Week 1 of the Jack Daniels Novice Marathon plan (18 weeks until race)
//     /// </summary>
//     private TrainingWeek GenerateWeek1()
//     {
//         return TrainingWeek.Create(
//             weekNumber: 1,
//             trainingPhase: TrainingPhase.Base,
//             monday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
//             tuesday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
//             wednesday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
//                 new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
//             thursday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
//                 new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
//             friday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
//                 new WalkRunInterval(2, TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(3))),
//             saturday: Workout.CreateRest(),
//             sunday: Workout.CreateRest());
//     }
//
//     /// <summary>
//     /// Week 2 of the Jack Daniels Novice Marathon plan (17 weeks until race)
//     /// </summary>
//     private TrainingWeek GenerateWeek2()
//     {
//         return TrainingWeek.Create(
//             weekNumber: 2,
//             trainingPhase: TrainingPhase.Base,
//             monday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
//             tuesday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
//             wednesday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
//                 new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
//             thursday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
//                 new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
//             friday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
//                 new WalkRunInterval(2, TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(3))),
//             saturday: Workout.CreateRest(),
//             sunday: Workout.CreateRest());
//     }
//
//     /// <summary>
//     /// Week 3 of the Jack Daniels Novice Marathon plan (16 weeks until race)
//     /// </summary>
//     private TrainingWeek GenerateWeek3()
//     {
//         return TrainingWeek.Create(
//             weekNumber: 3,
//             trainingPhase: TrainingPhase.Base,
//             monday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
//             tuesday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(15, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))),
//             wednesday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
//                 new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
//             thursday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
//                 new WalkRunInterval(3, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
//             friday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(9, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)),
//                 new WalkRunInterval(2, TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(3))),
//             saturday: Workout.CreateRest(),
//             sunday: Workout.CreateRest());
//     }
//
//     /// <summary>
//     /// Week 4 of the Jack Daniels Novice Marathon plan (15 weeks until race)
//     /// </summary>
//     private TrainingWeek GenerateWeek4()
//     {
//         return TrainingWeek.Create(
//             weekNumber: 4,
//             trainingPhase: TrainingPhase.Base,
//             monday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(4, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5))),
//             tuesday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(4, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5))),
//             wednesday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(10, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
//             thursday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(10, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
//             friday: Workout.CreateRest(),
//             saturday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(5, TimeSpan.FromMinutes(4), TimeSpan.FromMinutes(4))),
//             sunday: Workout.CreateRest());
//     }
//
//     /// <summary>
//     /// Week 5 of the Jack Daniels Novice Marathon plan (14 weeks until race)
//     /// </summary>
//     private TrainingWeek GenerateWeek5()
//     {
//         return TrainingWeek.Create(
//             weekNumber: 5,
//             trainingPhase: TrainingPhase.Base,
//             monday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(4, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5))),
//             tuesday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(4, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5))),
//             wednesday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(10, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
//             thursday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(10, TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))),
//             friday: Workout.CreateRest(),
//             saturday: Workout.CreateRunWalkWorkout(
//                 EasyPaceRange(),
//                 WalkPaceRange(),
//                 new WalkRunInterval(3, TimeSpan.FromMinutes(4), TimeSpan.FromMinutes(4)),
//                 new WalkRunInterval(1, TimeSpan.FromMinutes(15), TimeSpan.Zero),
//                 new WalkRunInterval(1, TimeSpan.Zero, TimeSpan.FromMinutes(6))),
//             sunday: Workout.CreateRest());
//     }
//
//     /// <summary>
//     /// Week 6 of the Jack Daniels Novice Marathon plan (13 weeks until race)
//     /// </summary>
//     private TrainingWeek GenerateWeek6()
//     {
//         return TrainingWeek.Create(
//             weekNumber: 5,
//             trainingPhase: TrainingPhase.Base,
//             monday: JackDanielsMarathonNoviceSections13To12UntilRace.CreateSessionA(
//                 WalkPaceRange(),
//                 EasyPaceRange(),
//                 TempoPaceRange(),
//                 StridePaceRange()),
//             tuesday: JackDanielsMarathonNoviceSections13To12UntilRace.CreateSessionB(WalkPaceRange(), EasyPaceRange()),
//             wednesday: JackDanielsMarathonNoviceSections13To12UntilRace.CreateSessionC(
//                 WalkPaceRange(),
//                 EasyPaceRange(),
//                 TempoPaceRange(),
//                 StridePaceRange()),
//             thursday: JackDanielsMarathonNoviceSections13To12UntilRace.CreateSessionD(WalkPaceRange(), EasyPaceRange()),
//             friday: JackDanielsMarathonNoviceSections13To12UntilRace.CreateSessionE(
//                 13,
//                 WalkPaceRange(),
//                 EasyPaceRange(),
//                 TempoPaceRange(),
//                 StridePaceRange()),
//             saturday: Workout.CreateRest(),
//             sunday: Workout.CreateRest());
//     }
//
//     /// <summary>
//     /// Week 7 of the Jack Daniels Novice Marathon plan (12 weeks until race)
//     /// </summary>
//     private TrainingWeek GenerateWeek7()
//     {
//         return TrainingWeek.Create(
//             weekNumber: 7,
//             trainingPhase: TrainingPhase.Base,
//             monday: JackDanielsMarathonNoviceSections13To12UntilRace.CreateSessionA(
//                 WalkPaceRange(),
//                 EasyPaceRange(),
//                 TempoPaceRange(),
//                 StridePaceRange()),
//             tuesday: JackDanielsMarathonNoviceSections13To12UntilRace.CreateSessionB(WalkPaceRange(), EasyPaceRange()),
//             wednesday: JackDanielsMarathonNoviceSections13To12UntilRace.CreateSessionC(
//                 WalkPaceRange(),
//                 EasyPaceRange(),
//                 TempoPaceRange(),
//                 StridePaceRange()),
//             thursday: JackDanielsMarathonNoviceSections13To12UntilRace.CreateSessionD(WalkPaceRange(), EasyPaceRange()),
//             friday: JackDanielsMarathonNoviceSections13To12UntilRace.CreateSessionE(
//                 12,
//                 WalkPaceRange(),
//                 EasyPaceRange(),
//                 TempoPaceRange(),
//                 StridePaceRange()),
//             saturday: Workout.CreateRest(),
//             sunday: Workout.CreateRest());
//     }
//
//     // Pace calculation methods for Novice runners
//     private static (TimeSpan min, TimeSpan max) EasyPaceRange()
//     {
//         // Define easy pace range for novice runners (per kilometer)
//         return (TimeSpan.FromMinutes(7).Add(TimeSpan.FromSeconds(30)),
//             TimeSpan.FromMinutes(9).Add(TimeSpan.FromSeconds(0)));
//     }
//
//     private static (TimeSpan min, TimeSpan max) WalkPaceRange()
//     {
//         // Define walking pace range for recovery segments (per kilometer)
//         return (TimeSpan.FromMinutes(9).Add(TimeSpan.FromSeconds(30)),
//             TimeSpan.FromMinutes(12).Add(TimeSpan.FromSeconds(0)));
//     }
//
//     private static (TimeSpan min, TimeSpan max) TempoPaceRange()
//     {
//         // Define tempo pace range for novice runners (per kilometer)
//         return (TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)),
//             TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)));
//     }
//
//     private static (TimeSpan min, TimeSpan max) StridePaceRange()
//     {
//         // Define stride pace range for recovery segments (per kilometer)
//         return (TimeSpan.FromMinutes(5),
//             TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(30)));
//     }
// }
