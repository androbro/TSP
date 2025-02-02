using TSP.Application.DTOs;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;
using TSP.Domain.Enums;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class NearestNeighborStrategy : IRouteOptimizationStrategy
{
   public string algorithmName => OptimizationAlgorithm.BruteForce.ToString();
    public Route? route { get; set; }

    public Route OptimizeRoute(List<Point> points, CancellationToken cancellationToken)
    {
        // var shortestRoute = FindShortestRoute(points);
        //
        // return new Route
        // {
        //     Points = points,
        //     Connections = shortestRoute
        // };
        throw new NotImplementedException();
    }

    // private double CalculateDistance(Point p1, Point p2)
    // {
    //     return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
    // }
    //
    // private IEnumerable<Connection> FindShortestRoute(IEnumerable<Point> points)
    // {
    //     var pointsList = points.ToList();
    //     if (pointsList.Count <= 1) return Enumerable.Empty<Connection>();
    //
    //     var shortestDistance = double.MaxValue;
    //     var shortestPath = new List<Connection>();
    //
    //     // Generate all possible orderings of points (true brute force)
    //     var allPermutations = GeneratePermutations(pointsList);
    //
    //     foreach (var permutation in allPermutations)
    //     {
    //         var currentDistance = 0.0;
    //         var connections = new List<Connection>();
    //
    //         currentDistance = CalculateDistanceForPermutation(permutation, connections, currentDistance);
    //         // Add connection back to start to complete the circuit
    //         var finalDistance = CalculateDistance(permutation.Last(), permutation.First());
    //         
    //         connections.Add(new Connection
    //         {
    //             FromPoint = permutation.Last(),
    //             ToPoint = permutation.First(),
    //             Distance = finalDistance,
    //             IsOptimal = false
    //         });
    //         currentDistance += finalDistance;
    //
    //         shortestDistance = CheckIfCurrentPermutationIsOptimal(currentDistance, shortestDistance, connections, ref shortestPath);
    //     }
    //
    //     return shortestPath;
    // }
    //
    // private static double CheckIfCurrentPermutationIsOptimal(double currentDistance, double shortestDistance,
    //     List<Connection> connections, ref List<Connection> shortestPath)
    // {
    //     if (currentDistance < shortestDistance)
    //     {
    //         shortestDistance = currentDistance;
    //         shortestPath = connections;
    //         // Mark all connections in shortest path as optimal
    //         foreach (var connection in shortestPath)
    //         {
    //             connection.IsOptimal = true;
    //         }
    //     }
    //
    //     return shortestDistance;
    // }
    //
    // private double CalculateDistanceForPermutation(List<Point> permutation, List<Connection> connections, double currentDistance)
    // {
    //     for (var i = 0; i < permutation.Count - 1; i++)
    //     {
    //         var currentPoint = permutation[i];
    //         var nextPoint = permutation[i + 1];
    //         var distance = CalculateDistance(currentPoint, nextPoint);
    //
    //         connections.Add(new Connection
    //         {
    //             FromPoint = currentPoint,
    //             ToPoint = nextPoint,
    //             Distance = distance,
    //             IsOptimal = false
    //         });
    //
    //         currentDistance += distance;
    //     }
    //
    //     return currentDistance;
    // }
    //
    // private IEnumerable<List<Point>> GeneratePermutations(List<Point> points)
    // {
    //     // Base case: if there is only one point or no points, return the list as is
    //     if (points.Count <= 1)
    //     {
    //         yield return points;
    //         yield break;
    //     }
    //
    //     // Take the first point
    //     var firstPoint = points[0];
    //     // Get the remaining points
    //     var remainingPoints = points.Skip(1).ToList();
    //
    //     // Recursively generate all permutations of the remaining points
    //     foreach (var permutation in GeneratePermutations(remainingPoints))
    //     {
    //         // Insert the first point into every possible position of each permutation
    //         for (var i = 0; i <= permutation.Count; i++)
    //         {
    //             var newRoute = new List<Point>(permutation);
    //             newRoute.Insert(i, firstPoint);
    //             yield return newRoute;
    //         }
    //     }
    // }
}