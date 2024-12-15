using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Application.Services.RouteOptimization.Strategies;

namespace TSP.Application.Services.RouteOptimization.Common;

public class RouteStrategyFactory : IRouteStrategyFactory
{
    private readonly IServiceProvider _serviceProvider;

    public RouteStrategyFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IRouteOptimizationStrategy GetStrategy(OptimizationAlgorithm algorithm)
    {
        return algorithm switch
        {
            OptimizationAlgorithm.BruteForce => new BruteForceStrategy(),
            OptimizationAlgorithm.NearestNeighbor => new NearestNeighborStrategy(),
            OptimizationAlgorithm.GeneticAlgorithm => new GeneticAlgorithmStrategy(),
            OptimizationAlgorithm.LinKernighan => new LinKernighanStrategy(),
            OptimizationAlgorithm.SimulatedAnnealing => new SimulatedAnnealingStrategy(),
            OptimizationAlgorithm.TwoOpt => new TwoOptStrategy(),
            _ => throw new ArgumentException($"Unsupported algorithm: {algorithm}")
        };
    }
}