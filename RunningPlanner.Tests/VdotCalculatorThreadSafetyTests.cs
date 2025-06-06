using System.Diagnostics;
using RunningPlanner.Core.Exceptions;
using RunningPlanner.Core.Models.Paces;

namespace RunningPlanner.Tests;

public class VdotCalculatorThreadSafetyTests
{
    [Fact]
    public void GetTrainingPacesFromRace_ConcurrentAccess_ThreadSafe()
    {
        // Arrange
        const int threadCount = 10;
        const int iterationsPerThread = 100;
        var tasks = new List<Task<TrainingPaces>>();
        var exceptions = new List<Exception>();

        // Act - Run multiple threads concurrently accessing VdotCalculator
        for (int i = 0; i < threadCount; i++)
        {
            var task = Task.Run(() =>
            {
                var results = new List<TrainingPaces>();
                
                for (int j = 0; j < iterationsPerThread; j++)
                {
                    try
                    {
                        // Test different race distances and times
                        decimal distance = 42.2m;
                        
                        var time = TimeSpan.FromHours(4); // Vary times
                        
                        var paces = VdotCalculator.GetTrainingPacesFromRace(distance, time);
                        results.Add(paces);
                    }
                    catch (Exception ex)
                    {
                        lock (exceptions)
                        {
                            exceptions.Add(ex);
                        }
                    }
                }
                
                return results.First(); // Return first result for verification
            });
            
            tasks.Add(task);
        }

        // Wait for all tasks to complete
        Task.WaitAll(tasks.ToArray());

        // Assert - No exceptions should occur and all results should be valid
        Assert.Empty(exceptions);
        Assert.All(tasks, task => 
        {
            Assert.True(task.IsCompletedSuccessfully);
            Assert.NotNull(task.Result);
            Assert.True(task.Result.Vdot >= 30 && task.Result.Vdot <= 85);
        });
    }

    [Fact]
    public void GetTrainingPacesFromRace_ValidInputs_ReturnsConsistentResults()
    {
        // Arrange
        var distance = 10m;
        var time = TimeSpan.FromMinutes(45);

        // Act - Get the same calculation multiple times
        var result1 = VdotCalculator.GetTrainingPacesFromRace(distance, time);
        var result2 = VdotCalculator.GetTrainingPacesFromRace(distance, time);
        var result3 = VdotCalculator.GetTrainingPacesFromRace(distance, time);

        // Assert - Results should be identical (same reference due to immutable collections)
        Assert.Equal(result1.Vdot, result2.Vdot);
        Assert.Equal(result2.Vdot, result3.Vdot);
        Assert.Equal(result1.EasyPace.Min, result2.EasyPace.Min);
        Assert.Equal(result1.MarathonPace, result2.MarathonPace);
        Assert.Equal(result1.ThresholdPace, result2.ThresholdPace);
    }

    [Fact]
    public void GetTrainingPacesFromRace_InvalidDistance_ThrowsVdotCalculationException()
    {
        // Arrange
        var invalidDistance = 7.5m; // Not supported
        var time = TimeSpan.FromMinutes(30);

        // Act & Assert
        var exception = Assert.Throws<VdotCalculationException>(() =>
            VdotCalculator.GetTrainingPacesFromRace(invalidDistance, time));
        
        Assert.Equal(invalidDistance, exception.Distance);
        Assert.Equal(time, exception.Time);
        Assert.Contains("Unsupported distance", exception.Message);
    }

    [Theory]
    [InlineData(5, 20)]    // 5K in 20 minutes
    [InlineData(10, 45)]   // 10K in 45 minutes  
    [InlineData(21.1, 95)] // Half marathon in 1:35
    [InlineData(42.2, 200)] // Marathon in 3:20
    public void GetTrainingPacesFromRace_ValidDistancesAndTimes_ReturnsValidPaces(decimal distance, int minutes)
    {
        // Arrange
        var time = TimeSpan.FromMinutes(minutes);

        // Act
        var result = VdotCalculator.GetTrainingPacesFromRace(distance, time);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Vdot >= 30 && result.Vdot <= 85);
        Assert.True(result.EasyPace.Min > TimeSpan.Zero);
        Assert.True(result.EasyPace.Max > result.EasyPace.Min);
        Assert.True(result.MarathonPace.Min > TimeSpan.Zero);
        Assert.True(result.ThresholdPace.Min > TimeSpan.Zero);
        Assert.True(result.IntervalPace.Min > TimeSpan.Zero);
        Assert.True(result.RepetitionPace.Min > TimeSpan.Zero);
        
        // // Verify pace progression (repetition < interval < threshold < marathon < easy)
        // Assert.True(result.RepetitionPace < result.IntervalPace);
        // Assert.True(result.IntervalPace < result.ThresholdPace);
        // Assert.True(result.ThresholdPace < result.MarathonPace);
        // Assert.True(result.MarathonPace < result.EasyPace.Max);
    }

    [Fact]
    public void GetTrainingPacesFromRace_ExtremeTimes_HandlesGracefully()
    {
        // Arrange & Act & Assert - Very fast time (should get max VDOT)
        var fastResult = VdotCalculator.GetTrainingPacesFromRace(5m, TimeSpan.FromMinutes(12));
        Assert.True(fastResult.Vdot >= 80); // Should be high VDOT

        // Very slow time (should get min VDOT)
        var slowResult = VdotCalculator.GetTrainingPacesFromRace(5m, TimeSpan.FromMinutes(60));
        Assert.True(slowResult.Vdot <= 35); // Should be low VDOT
    }

    [Fact]
    public void VdotCalculator_PerformanceTest_CompletesQuickly()
    {
        // Arrange
        const int iterations = 1000;
        var distances = new[] { 5m, 10m, 21.1m, 42.2m };
        var times = new[] { 
            TimeSpan.FromMinutes(15), 
            TimeSpan.FromMinutes(30), 
            TimeSpan.FromMinutes(60), 
            TimeSpan.FromMinutes(120) 
        };

        // Act
        var stopwatch = Stopwatch.StartNew();
        
        for (int i = 0; i < iterations; i++)
        {
            var distance = distances[i % distances.Length];
            var time = times[i % times.Length];
            var result = VdotCalculator.GetTrainingPacesFromRace(distance, time);
            Assert.NotNull(result);
        }
        
        stopwatch.Stop();

        // Assert - Should complete quickly (under 100ms for 1000 calculations)
        Assert.True(stopwatch.ElapsedMilliseconds < 100, 
            $"Performance test took {stopwatch.ElapsedMilliseconds}ms, expected < 100ms");
    }

    [Fact]
    public void VdotCalculator_MemoryConsistency_ImmutableCollections()
    {
        // This test verifies that the collections are truly immutable and thread-safe
        // by ensuring multiple calls return consistent data
        
        // Arrange
        var results = new List<TrainingPaces>();
        
        // Act - Get multiple results
        for (int i = 0; i < 10; i++)
        {
            results.Add(VdotCalculator.GetTrainingPacesFromRace(10m, TimeSpan.FromMinutes(45)));
        }
        
        // Assert - All results should be identical (immutable collections ensure consistency)
        var first = results.First();
        Assert.All(results, result =>
        {
            Assert.Equal(first.Vdot, result.Vdot);
            Assert.Equal(first.EasyPace.Min, result.EasyPace.Min);
            Assert.Equal(first.EasyPace.Max, result.EasyPace.Max);
            Assert.Equal(first.MarathonPace, result.MarathonPace);
            Assert.Equal(first.ThresholdPace, result.ThresholdPace);
            Assert.Equal(first.IntervalPace, result.IntervalPace);
            Assert.Equal(first.RepetitionPace, result.RepetitionPace);
        });
    }
}