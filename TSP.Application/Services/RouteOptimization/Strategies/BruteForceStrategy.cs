using System.Diagnostics;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;
using TSP.Domain.Enums;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class BruteForceStrategy : IRouteOptimizationStrategy
{
    public string algorithmName { get; } = "Brute Force";
    public Route? route { get; set; }
    
    public Route OptimizeRoute(List<Point> points)
    {
        ValidateInput(points);
        
        var stopwatch = StartPerformanceTimer();
        var (startPoint, remainingPoints) = SeparateStartPoint(points);
        var possibleRoutes = GenerateAllPossibleRoutes(startPoint, remainingPoints, stopwatch);
        var optimalRoute = FindOptimalRoute(possibleRoutes);
        
        MarkOptimalConnections(optimalRoute);
        return optimalRoute;
    }

    private static void ValidateInput(List<Point> points)
    {
        if (points.Count <= 1)
            throw new ArgumentException("Need at least 2 points for route optimization");
    }

    private static Stopwatch StartPerformanceTimer()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        return stopwatch;
    }

    private static (Point startPoint, List<Point> remainingPoints) SeparateStartPoint(List<Point> points)
    {
        var startPoint = points[0];
        var remainingPoints = points.Skip(1).ToList();
        return (startPoint, remainingPoints);
    }

    private static List<Route> GenerateAllPossibleRoutes(Point startPoint, List<Point> remainingPoints, Stopwatch stopwatch)
    {
        var possibleRoutes = new List<Route>();
        
        foreach (var permutation in GeneratePermutations(remainingPoints))
        {
            var completeRoute = CreateCompleteRoute(startPoint, permutation);
            var route = CreateRouteWithMetrics(completeRoute, stopwatch);
            possibleRoutes.Add(route);
        }

        return possibleRoutes;
    }

    private static List<Point> CreateCompleteRoute(Point startPoint, List<Point> permutation)
    {
        var completeRoute = new List<Point> { startPoint };
        completeRoute.AddRange(permutation);
        completeRoute.Add(startPoint); // Complete the circuit
        return completeRoute;
    }

    private static Route CreateRouteWithMetrics(List<Point> points, Stopwatch stopwatch)
    {
        return new Route(points, OptimizationAlgorithm.BruteForce)
        {
            Connections = CreateConnections(points),
            TotalDistance = CalculateTotalDistance(points),
            CalculationTime = stopwatch.ElapsedMilliseconds.ToString()
        };
    }

    private static Route FindOptimalRoute(List<Route> possibleRoutes)
    {
        return possibleRoutes.MinBy(r => r.TotalDistance) 
            ?? throw new InvalidOperationException("Failed to find optimal route");
    }

    private static void MarkOptimalConnections(Route optimalRoute)
    {
        foreach (var connection in optimalRoute.Connections)
        {
            connection.IsOptimal = true;
        }
    }

    private static List<Connection> CreateConnections(List<Point> points)
    {
        return Enumerable.Range(0, points.Count - 1)
            .Select(i => new Connection
            {
                FromPoint = points[i],
                ToPoint = points[i + 1],
                Distance = CalculateDistance(points[i], points[i + 1])
            })
            .ToList();
    }

    private static double CalculateTotalDistance(List<Point> points)
    {
        return Enumerable.Range(0, points.Count - 1)
            .Sum(i => CalculateDistance(points[i], points[i + 1]));
    }

    private static double CalculateDistance(Point p1, Point p2)
    {
        var dx = p2.X - p1.X;
        var dy = p2.Y - p1.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    private static IEnumerable<List<T>> GeneratePermutations<T>(List<T> items)
    {
        if (items.Count <= 1)
        {
            yield return new List<T>(items);
            yield break;
        }

        for (int i = 0; i < items.Count; i++)
        {
            var current = items[i];
            var remainingItems = items.Take(i).Concat(items.Skip(i + 1)).ToList();

            foreach (var permutation in GeneratePermutations(remainingItems))
            {
                permutation.Insert(0, current);
                yield return permutation;
            }
        }
    }
}