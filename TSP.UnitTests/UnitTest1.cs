using TSP.Domain.Entities;
using TSP.Application.Services.RouteOptimization.Strategies;
using Xunit;
using System.Collections.Generic;
using System.Linq;

public class BruteForceStrategyTests
{
    [Fact]
    public void OptimizeRoute_ReturnsShortestRoute_ForMultiplePoints()
    {
        // Arrange
        var points = new List<Point>
        {
            new(0, 0),
            new(1, 1),
            new(2, 2)
        };
        var strategy = new BruteForceStrategy();

        // Act
        var result = strategy.OptimizeRoute(points);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Points.Count);
    }

    [Fact]
    public void OptimizeRoute_ReturnsSinglePointRoute_ForSinglePoint()
    {
        // Arrange
        var points = new List<Point>
        {
            new(0, 0)
        };
        var strategy = new BruteForceStrategy();

        // Act
        var result = strategy.OptimizeRoute(points);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Points);
        Assert.Empty(result.Connections);
    }

    [Fact]
    public void OptimizeRoute_ReturnsEmptyRoute_ForNoPoints()
    {
        // Arrange
        var points = new List<Point>();
        var strategy = new BruteForceStrategy();

        // Act
        var result = strategy.OptimizeRoute(points);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Points);
        Assert.Empty(result.Connections);
    }

    [Fact]
    public void OptimizeRoute_ReturnsShortestRoute_ForTwoPoints()
    {
        // Arrange
        var points = new List<Point>
        {
            new Point(0, 0),
            new Point(1, 1)
        };
        var strategy = new BruteForceStrategy();

        // Act
        var result = strategy.OptimizeRoute(points);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Points.Count);
    }
}