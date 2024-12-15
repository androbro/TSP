using TSP.Domain.Enums;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class TwoOptStrategy: IRouteOptimizationStrategy
{
    public string AlgorithmName => OptimizationAlgorithm.TwoOpt.ToString();

    public async Task<List<Point>> OptimizeRoute(List<Point> points)
    {
        // Implementation here
        throw new NotImplementedException();
    }
}