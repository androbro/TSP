using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;
using TSP.Domain.Enums;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class Permutation
{
    public Point BasePoint { get; set; }
    public Point DestinationPoint { get; set; }
    public double Distance { get; set; }
}

public class BruteForceStrategy : IRouteOptimizationStrategy
{
    public string algorithmName => OptimizationAlgorithm.BruteForce.ToString();
    public Route? route { get; set; }
    
    public async Task<Route> OptimizeRoute(List<Point> points)
    {
        var permutations = GetPermutations(points);
        var shortestRoute = FindShortestRoute(permutations, points);
        
        return new Route
        {
            Points = points,
            Connections = shortestRoute
        };
    }
    
    private IEnumerable<Permutation> GetPermutations(List<Point> points)
    {
        var permutations = new List<Permutation>();
        
        foreach (var point in points)
        {
            foreach (var otherPoint in points.Where(p => p.Id != point.Id))
            {
                var distance = CalculateDistance(point, otherPoint);
                permutations.Add(new Permutation
                {
                    BasePoint = point,
                    DestinationPoint = otherPoint,
                    Distance = distance
                });
            }
        }
        
        return permutations;
    }

    private double CalculateDistance(Point p1, Point p2)
    {
        return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
    }
    
    private IEnumerable<Connection> FindShortestRoute(IEnumerable<Permutation> permutations, IEnumerable<Point> points)
    {
        var pointsList = points.ToList();
        if (pointsList.Count <= 1) return [];

        var shortestDistance = double.MaxValue;
        var shortestPath = new List<Connection>();
        
        // Generate all possible orderings of points (true brute force)
        var allPossibleRoutes = GenerateAllPossibleRoutes(pointsList);
        
        foreach (var route in allPossibleRoutes)
        {
            var currentDistance = 0.0;
            var connections = new List<Connection>();
            
            // Calculate total distance for this route
            for (var i = 0; i < route.Count - 1; i++)
            {
                var currentPoint = route[i];
                var nextPoint = route[i + 1];
                var distance = CalculateDistance(currentPoint, nextPoint);
                
                connections.Add(new Connection
                {
                    FromPoint = currentPoint,
                    ToPoint = nextPoint,
                    Distance = distance,
                    IsOptimal = false
                });
                
                currentDistance += distance;
            }
            
            // Add connection back to start to complete the circuit
            var finalDistance = CalculateDistance(route.Last(), route.First());
            connections.Add(new Connection
            {
                FromPoint = route.Last(),
                ToPoint = route.First(),
                Distance = finalDistance,
                IsOptimal = false
            });
            currentDistance += finalDistance;
            
            // Update shortest path if this route is shorter
            if (currentDistance < shortestDistance)
            {
                shortestDistance = currentDistance;
                shortestPath = connections;
                // Mark all connections in shortest path as optimal
                foreach (var connection in shortestPath)
                {
                    connection.IsOptimal = true;
                }
            }
        }
        
        return shortestPath;
    }
    
    private IEnumerable<List<Point>> GenerateAllPossibleRoutes(List<Point> points)
    {
        if (points.Count <= 1)
        {
            yield return points;
            yield break;
        }

        var firstPoint = points[0];
        var remainingPoints = points.Skip(1).ToList();
        
        foreach (var permutation in GenerateAllPossibleRoutes(remainingPoints))
        {
            for (var i = 0; i <= permutation.Count; i++)
            {
                var newRoute = new List<Point>(permutation);
                newRoute.Insert(i, firstPoint);
                yield return newRoute;
            }
        }
    }
}