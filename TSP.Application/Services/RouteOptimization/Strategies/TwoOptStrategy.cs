using TSP.Domain.Enums;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class TwoOptStrategy: IRouteOptimizationStrategy
{
    public string algorithmName => OptimizationAlgorithm.TwoOpt.ToString();
    public Route? route { get; set; }

    public async Task<Route> OptimizeRoute(List<Point> points)
    {
        // Implementation here
        throw new NotImplementedException();
    }
}