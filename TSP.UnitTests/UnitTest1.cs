using TSP.Domain.Entities;
using TSP.Application.Services.RouteOptimization.Strategies;
using TSP.Domain.Enums;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System;

public class BruteForceStrategyTests
{
    [Fact]
    public void OptimizeRoute_ReturnsShortestRoute_ForMultiplePoints()
    {
        // Arrange
        var points = new List<Point>
        {
            new(0, 124),  // Point a (start/end)
            new(12, 141), // Point b
            new(22, 244), // Point c
            new(3, 123)   // Point d
        };
        var strategy = new BruteForceStrategy();

        // Act
        var result = strategy.OptimizeRoute(points);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(OptimizationAlgorithm.BruteForce, result.Algorithm);
        Assert.Equal(5, result.Points.Count); // 4 original points + return to start
        Assert.Equal(4, result.Connections.Count); // Should have n connections for n points including return
        
        // Verify start and end points are the same (complete circuit)
        Assert.Equal(result.Points[0].X, result.Points[^1].X);
        Assert.Equal(result.Points[0].Y, result.Points[^1].Y);
        
        // Verify all connections are marked as optimal
        Assert.All(result.Connections, connection => Assert.True(connection.IsOptimal));
        
        // Verify all points are used exactly once (except start point which is used twice)
        var uniquePoints = result.Points.Take(result.Points.Count - 1).Distinct().Count();
        Assert.Equal(points.Count, uniquePoints);
        
        // Verify the calculation time is recorded
        Assert.False(string.IsNullOrEmpty(result.CalculationTime));
    }

    [Fact]
    public void OptimizeRoute_ThrowsException_ForEmptyPointsList()
    {
        // Arrange
        var strategy = new BruteForceStrategy();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => strategy.OptimizeRoute(new List<Point>()));
    }

    [Fact]
    public void OptimizeRoute_ThrowsException_ForSinglePoint()
    {
        // Arrange
        var strategy = new BruteForceStrategy();
        var points = new List<Point> { new(1, 1) };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => strategy.OptimizeRoute(points));
    }

    [Fact]
    public void OptimizeRoute_ValidatesRouteTotalDistance()
    {
        // Arrange
        var points = new List<Point>
        {
            new(0, 0),   // Point a (start/end)
            new(3, 0),   // Point b
            new(0, 4)    // Point c
        };
        var strategy = new BruteForceStrategy();

        // Act
        var result = strategy.OptimizeRoute(points);

        // Assert
        // Expected path: (0,0) -> (3,0) -> (0,4) -> (0,0)
        // Distance calculation:
        // (0,0) to (3,0) = 3
        // (3,0) to (0,4) = 5
        // (0,4) to (0,0) = 4
        // Total = 12
        var expectedDistance = 12.0;
        Assert.Equal(expectedDistance, result.TotalDistance, 0.1); // Using delta for floating-point comparison
    }

    [Fact]
    public void OptimizeRoute_PreservesStartPoint()
    {
        // Arrange
        var startPoint = new Point(0, 0);
        var points = new List<Point>
        {
            startPoint,
            new(1, 1),
            new(2, 2),
            new(3, 3)
        };
        var strategy = new BruteForceStrategy();

        // Act
        var result = strategy.OptimizeRoute(points);

        // Assert
        Assert.Equal(startPoint.X, result.Points[0].X);
        Assert.Equal(startPoint.Y, result.Points[0].Y);
        Assert.Equal(startPoint.X, result.Points[^1].X);
        Assert.Equal(startPoint.Y, result.Points[^1].Y);
    }
}