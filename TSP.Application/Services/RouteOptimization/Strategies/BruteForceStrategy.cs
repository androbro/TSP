using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;
using TSP.Domain.Enums;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class BruteForceStrategy: IRouteOptimizationStrategy
{
    public string algorithmName => OptimizationAlgorithm.BruteForce.ToString();
    public Route? route { get; set; }
    
    public async Task<Route> OptimizeRoute(List<Point> points)
    {
        //find all possible permutations of the points
        var permutations = getPermutations(points);
        
        //return a basic response so i know the method is being called
        return new Route
        {
            Points = points
        };
    }
    
    private IEnumerable<IEnumerable<Point>> getPermutations(List<Point> points)
    {
        var permutations = new List<List<Point>>();

        foreach (var checkPoint in points)
        {
            var point = checkPoint;
            var otherPoints = points.Where(p => p.Id != point.Id).ToList();
            foreach (var currentPoint in otherPoints)
            {
                //find the distance between the current point and the other points
                var distance = Math.Sqrt(Math.Pow(checkPoint.X - currentPoint.X, 2) + Math.Pow(checkPoint.Y - currentPoint.Y, 2));
                Console.WriteLine($"Distance between {checkPoint.Id} and {currentPoint.Id} is {distance}");
                permutations.Add([checkPoint, currentPoint]);
            }
        }
        
        return permutations;
    }
}