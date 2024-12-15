using TSP.Domain.Enums;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class GeneticAlgorithmStrategy: IRouteOptimizationStrategy
{
    public string AlgorithmName => OptimizationAlgorithm.GeneticAlgorithm.ToString();

    public async Task<List<Point>> OptimizeRoute(List<Point> points)
    {
        // Implementation here
        throw new NotImplementedException();
    }
}