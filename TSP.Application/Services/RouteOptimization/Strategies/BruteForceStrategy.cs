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
        if (points.Count <= 1)
            throw new ArgumentException("Need at least 2 points for route optimization");

        Stopwatch stopwatch = new();
        stopwatch.Start();
        var startPoint = points[0];  // Keep track of start point
        var pointsToPermute = points.Skip(1).ToList();  // Permute all except start
        
        List<Route> possibleRoutes = new();
        foreach (var permutation in GeneratePermutations(pointsToPermute))
        {
            // Add start point to beginning and end to complete the circuit
            var fullRoute = new List<Point> { startPoint };
            fullRoute.AddRange(permutation);
            fullRoute.Add(startPoint);
            
            // Create route and calculate its total distance
            var route = new Route(fullRoute, OptimizationAlgorithm.BruteForce)
            {
                Connections = CreateConnections(fullRoute),
                TotalDistance = CalculateTotalDistance(fullRoute),
                CalculationTime = stopwatch.ElapsedMilliseconds.ToString()
            };
            
            possibleRoutes.Add(route);
        }

        // Find the route with minimum total distance
        var optimalRoute = possibleRoutes.MinBy(r => r.TotalDistance) 
            ?? throw new InvalidOperationException("Failed to find optimal route");
            
        // Mark connections as optimal
        foreach (var connection in optimalRoute.Connections)
        {
            connection.IsOptimal = true;
        }

        return optimalRoute;
    }

    private static List<Connection> CreateConnections(List<Point> points)
    {
        var connections = new List<Connection>();
        for (int i = 0; i < points.Count - 1; i++)
        {
            connections.Add(new Connection
            {
                FromPoint = points[i],
                ToPoint = points[i + 1],
                Distance = CalculateDistance(points[i], points[i + 1])
            });
        }
        return connections;
    }

    private static double CalculateTotalDistance(List<Point> points)
    {
        double totalDistance = 0;
        for (int i = 0; i < points.Count - 1; i++)
        {
            totalDistance += CalculateDistance(points[i], points[i + 1]);
        }
        return totalDistance;
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