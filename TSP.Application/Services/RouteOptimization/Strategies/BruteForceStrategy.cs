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
        //return a basic response so i know the method is being called
        return new Route
        {
            Points = points
        };
    }
}