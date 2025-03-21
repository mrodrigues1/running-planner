using RunningPlanner.Core.Models;
using RunningPlanner.Core.TrainingPlans.HalHigdon;

namespace RunningPlanner.Tests;

public class TrainingPlanAnalyzerTests
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var plan = new HalHigdonMarathonNovice1();
        var plan2 = new HalHigdonMarathonNovice2();
        var plan3 = new HalHigdonMarathonIntermediate1();
        var plan4 = new HalHigdonMarathonIntermediate2();
        var plan5 = new HalHigdonMarathonAdvanced1();
        var plan6 = new HalHigdonMarathonAdvanced2();

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