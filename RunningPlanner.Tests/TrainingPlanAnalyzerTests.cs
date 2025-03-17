using RunningPlanner.Core.Models;
using RunningPlanner.Core.TrainingPlans;

namespace RunningPlanner.Tests;

public class TrainingPlanAnalyzerTests
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var halHigdonMarathonIntermediate2 = new HalHigdonMarathonIntermediate2();
        var trainingPlanAnalyzer = new TrainingPlanAnalyzer(halHigdonMarathonIntermediate2.TrainingPlan);

        // Act

        // Assert
    }
    
    [Fact]
    public void Test2()
    {
        // Arrange
        var halHigdonMarathonIntermediate1 = new HalHigdonMarathonIntermediate1();
        var trainingPlanAnalyzer = new TrainingPlanAnalyzer(halHigdonMarathonIntermediate1.TrainingPlan);

        // Act

        // Assert
    }
}