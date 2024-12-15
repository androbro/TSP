using TSP.Application.Services.RouteOptimization.Strategies;
using TSP.Domain.Entities;
using FluentAssertions;

namespace TSP.UnitTests.RouteOptimization;

public class BruteForceStrategyTests
{
    private readonly BruteForceStrategy _strategy;

    public BruteForceStrategyTests()
    {
        _strategy = new BruteForceStrategy();
    }

    [Fact]
    public async Task OptimizeRoute_WithTwoPoints_ShouldReturnValidRoute()
    {
        // Arrange
        var points = new List<Point>
        {
            new() { Id = Guid.NewGuid(), X = 0, Y = 0 },
            new() { Id = Guid.NewGuid(), X = 3, Y = 4 }
        };

        // Act
        var result = await _strategy.OptimizeRoute(points);

        // Assert
        result.Should().NotBeNull();
        result.Points.Should().HaveCount(2);
        result.Points.Should().Contain(points[0]);
        result.Points.Should().Contain(points[1]);
    }

    [Fact]
    public async Task OptimizeRoute_WithThreePoints_ShouldReturnAllPoints()
    {
        // Arrange
        var points = new List<Point>
        {
            new() { Id = Guid.NewGuid(), X = 0, Y = 0 },
            new() { Id = Guid.NewGuid(), X = 3, Y = 4 },
            new() { Id = Guid.NewGuid(), X = 6, Y = 8 }
        };

        // Act
        var result = await _strategy.OptimizeRoute(points);

        // Assert
        result.Should().NotBeNull();
        result.Points.Should().HaveCount(3);
        result.Points.Should().Contain(points[0]);
        result.Points.Should().Contain(points[1]);
        result.Points.Should().Contain(points[2]);
    }

    [Fact]
    public async Task OptimizeRoute_WithEmptyList_ShouldReturnEmptyRoute()
    {
        // Arrange
        var points = new List<Point>();

        // Act
        var result = await _strategy.OptimizeRoute(points);

        // Assert
        result.Should().NotBeNull();
        result.Points.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(DistanceTestData))]
    public async Task OptimizeRoute_ShouldCalculateCorrectDistances(Point p1, Point p2, double expectedDistance)
    {
        // Arrange
        var points = new List<Point> { p1, p2 };

        // Act
        var result = await _strategy.OptimizeRoute(points);

        // Assert
        // Calculate actual distance between points in result
        var actualDistance = Math.Sqrt(
            Math.Pow(result.Points[0].X - result.Points[1].X, 2) + 
            Math.Pow(result.Points[0].Y - result.Points[1].Y, 2));

        actualDistance.Should().BeApproximately(expectedDistance, 0.001);
    }

    public static IEnumerable<object[]> DistanceTestData()
    {
        yield return new object[] 
        { 
            new Point { Id = Guid.NewGuid(), X = 0, Y = 0 },
            new Point { Id = Guid.NewGuid(), X = 3, Y = 4 },
            5.0 // Pythagorean triple 3-4-5
        };
        
        yield return new object[] 
        { 
            new Point { Id = Guid.NewGuid(), X = 0, Y = 0 },
            new Point { Id = Guid.NewGuid(), X = 5, Y = 12 },
            13.0 // Pythagorean triple 5-12-13
        };
        
        yield return new object[] 
        { 
            new Point { Id = Guid.NewGuid(), X = 2, Y = 2 },
            new Point { Id = Guid.NewGuid(), X = 2, Y = 2 },
            0.0 // Same point
        };
    }

    [Fact]
    public async Task OptimizeRoute_WithSinglePoint_ShouldReturnSinglePointRoute()
    {
        // Arrange
        var point = new Point { Id = Guid.NewGuid(), X = 1, Y = 1 };
        var points = new List<Point> { point };

        // Act
        var result = await _strategy.OptimizeRoute(points);

        // Assert
        result.Should().NotBeNull();
        result.Points.Should().HaveCount(1);
        result.Points.Should().Contain(point);
    }

    [Fact]
    public void AlgorithmName_ShouldReturnBruteForce()
    {
        // Act & Assert
        _strategy.algorithmName.Should().Be("BruteForce");
    }
}