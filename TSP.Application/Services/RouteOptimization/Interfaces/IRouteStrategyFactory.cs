using TSP.Application.Services.RouteOptimization.Common;

namespace TSP.Application.Services.RouteOptimization.Interfaces;

public interface IRouteStrategyFactory
{
    IRouteOptimizationStrategy GetStrategy(OptimizationAlgorithm algorithm);
}