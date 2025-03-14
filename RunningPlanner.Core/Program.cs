namespace RunningPlanner.Core;

class Program
{
    static void Main(string[] args)
    {


        var trainingPlanBuilder = new HalHigdonMarathonIntermediate2();
        var trainingPlan = trainingPlanBuilder.GenerateDefaultTrainingPlan();


    }
}