using FluentAssertions;
using TSP.Application.Services.RouteOptimization.Strategies;
using TSP.Domain.Entities;

namespace TSP.UnitTests;

public class BruteForceStrategyTests
{
    private readonly BruteForceStrategy _strategy = new();

    [Fact]
    public async Task OptimizeRoute_WithTwoPoints_ShouldReturnValidRouteAndConnections()
    {
        // Arrange
        var point1 = new Point { Id = Guid.NewGuid(), X = 0, Y = 0 };
        var point2 = new Point { Id = Guid.NewGuid(), X = 3, Y = 4 };
        var points = new List<Point> { point1, point2 };

        // Act
        var result = await _strategy.OptimizeRoute(points);

        // Assert
        result.Should().NotBeNull();
        result.Points.Should().HaveCount(2);
        result.Connections.Should().HaveCount(1);
        
        var connection = result.Connections.First();
        connection.Should().NotBeNull();
        connection.Distance.Should().BeApproximately(5.0, 0.001);
        connection.IsOptimal.Should().BeTrue();
        
        // Verify connection points match
        (connection.FromPoint == point1.Id && connection.ToPoint == point2.Id ||
         connection.FromPoint == point2.Id && connection.ToPoint == point1.Id)
            .Should().BeTrue();
    }

    [Fact]
    public async Task OptimizeRoute_WithThreePoints_ShouldCreateOptimalConnections()
    {
        // Arrange
        var point1 = new Point { Id = Guid.NewGuid(), X = 0, Y = 0 };
        var point2 = new Point { Id = Guid.NewGuid(), X = 3, Y = 0 };
        var point3 = new Point { Id = Guid.NewGuid(), X = 0, Y = 4 };
        var points = new List<Point> { point1, point2, point3 };

        // Act
        var result = await _strategy.OptimizeRoute(points);

        // Assert
        result.Should().NotBeNull();
        result.Points.Should().HaveCount(3);
        result.Connections.Should().HaveCount(2);

        // All connections should be marked as optimal
        result.Connections.Should().OnlyContain(c => c.IsOptimal);

        // Verify we have continuous path (each point is connected)
        var connectedPoints = new HashSet<Guid>();
        connectedPoints.Add(result.Connections.First().FromPoint);
        foreach (var connection in result.Connections)
        {
            connectedPoints.Add(connection.ToPoint);
        }
        connectedPoints.Should().HaveCount(3);
    }

    [Fact]
    public async Task OptimizeRoute_ShouldChooseShortestPath()
    {
        // Arrange
        var point1 = new Point { Id = Guid.NewGuid(), X = 0, Y = 0 };
        var point2 = new Point { Id = Guid.NewGuid(), X = 1, Y = 0 }; // Closer to point1
        var point3 = new Point { Id = Guid.NewGuid(), X = 10, Y = 0 }; // Further from point1
        var points = new List<Point> { point1, point2, point3 };

        // Act
        var result = await _strategy.OptimizeRoute(points);

        // Assert
        result.Should().NotBeNull();
        result.Connections.Should().HaveCount(2);

        // First connection should be to the closest point
        var firstConnection = result.Connections.First();
        firstConnection.FromPoint.Should().Be(point1.Id);
        firstConnection.ToPoint.Should().Be(point2.Id);
        firstConnection.Distance.Should().BeApproximately(1.0, 0.001);
    }

    [Fact]
    public async Task OptimizeRoute_WithEmptyList_ShouldReturnEmptyRouteAndConnections()
    {
        // Arrange
        var points = new List<Point>();

        // Act
        var result = await _strategy.OptimizeRoute(points);

        // Assert
        result.Should().NotBeNull();
        result.Points.Should().BeEmpty();
        result.Connections.Should().BeEmpty();
    }

    [Fact]
    public async Task OptimizeRoute_WithSinglePoint_ShouldReturnNoConnections()
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
        result.Connections.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(DistanceTestData))]
    public async Task OptimizeRoute_ShouldCalculateCorrectDistancesInConnections(
        Point p1, Point p2, double expectedDistance)
    {
        // Arrange
        var points = new List<Point> { p1, p2 };

        // Act
        var result = await _strategy.OptimizeRoute(points);

        // Assert
        result.Connections.Should().HaveCount(1);
        result.Connections.First().Distance.Should().BeApproximately(expectedDistance, 0.001);
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
    public async Task OptimizeRoute_ShouldNotCreateDuplicateConnections()
    {
        // Arrange
        var point1 = new Point { Id = Guid.NewGuid(), X = 0, Y = 0 };
        var point2 = new Point { Id = Guid.NewGuid(), X = 1, Y = 1 };
        var point3 = new Point { Id = Guid.NewGuid(), X = 2, Y = 2 };
        var points = new List<Point> { point1, point2, point3 };

        // Act
        var result = await _strategy.OptimizeRoute(points);

        // Assert
        result.Connections.Should().HaveCount(2); // Should only have n-1 connections for n points
        result.Connections.Select(c => c.FromPoint).Should().OnlyHaveUniqueItems();
        result.Connections.Select(c => c.ToPoint).Should().OnlyHaveUniqueItems();
    }

    [Fact]
    public void AlgorithmName_ShouldReturnBruteForce()
    {
        // Act & Assert
        _strategy.algorithmName.Should().Be("BruteForce");
    }
}