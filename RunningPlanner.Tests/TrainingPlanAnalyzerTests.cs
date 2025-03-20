using RunningPlanner.Core.Models;
using RunningPlanner.Core.TrainingPlans.HalHigdon;

namespace RunningPlanner.Tests;

public class TrainingPlanAnalyzerTests
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var plan = new HalHigdonMarathonAdvanced1();

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