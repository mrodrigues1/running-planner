using RunningPlanner.Core;
using RunningPlanner.Core.Models;

namespace RunningPlanner.UnitTests;

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
}