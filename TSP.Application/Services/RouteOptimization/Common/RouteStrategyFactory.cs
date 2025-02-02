using TSP.Application.DTOs;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Application.Services.RouteOptimization.Strategies;

namespace TSP.Application.Services.RouteOptimization.Common;

public class RouteStrategyFactory : IRouteStrategyFactory
{
    //Ienmerable<IRouteOptimizationStrategy> _strategies;
    private readonly IServiceProvider _serviceProvider;

    public RouteStrategyFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IRouteOptimizationStrategy? GetStrategy(OptimizationAlgorithmDto algorithm)
    {
        return algorithm switch
        {
            OptimizationAlgorithmDto.BruteForce => new BruteForceStrategy(),
            OptimizationAlgorithmDto.NearestNeighbor => new NearestNeighborStrategy(),
            OptimizationAlgorithmDto.GeneticAlgorithm => new GeneticAlgorithmStrategy(),
            OptimizationAlgorithmDto.LinKernighan => new LinKernighanStrategy(),
            OptimizationAlgorithmDto.SimulatedAnnealing => new SimulatedAnnealingStrategy(),
            OptimizationAlgorithmDto.TwoOpt => new TwoOptStrategy(),
            _ => throw new ArgumentException($"Unsupported algorithm: {algorithm}")
        };
    }
}