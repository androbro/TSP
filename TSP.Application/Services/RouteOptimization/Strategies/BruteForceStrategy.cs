using TSP.Application.Services.RouteOptimization.Common;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class BruteForceStrategy: IRouteOptimizationStrategy
{
    public string AlgorithmName => OptimizationAlgorithm.BruteForce.ToString();

    public async Task<List<Point>> OptimizeRoute(List<Point> points)
    {
        // Implementation here
        throw new NotImplementedException();
    }
}