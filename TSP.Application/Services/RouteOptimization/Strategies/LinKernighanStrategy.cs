using TSP.Application.Services.RouteOptimization.Common;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class LinKernighanStrategy: IRouteOptimizationStrategy
{
    public string AlgorithmName => OptimizationAlgorithm.LinKernighan.ToString();

    public async Task<List<Point>> OptimizeRoute(List<Point> points)
    {
        // Implementation here
        throw new NotImplementedException();
    }
}