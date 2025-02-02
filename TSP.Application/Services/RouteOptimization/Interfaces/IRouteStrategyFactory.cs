using TSP.Application.DTOs;

namespace TSP.Application.Services.RouteOptimization.Interfaces;

public interface IRouteStrategyFactory
{
    IRouteOptimizationStrategy? GetStrategy(OptimizationAlgorithmDto algorithm);
}