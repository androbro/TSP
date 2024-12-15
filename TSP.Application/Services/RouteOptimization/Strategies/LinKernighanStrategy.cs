using TSP.Domain.Enums;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class LinKernighanStrategy: IRouteOptimizationStrategy
{
    public string algorithmName => OptimizationAlgorithm.LinKernighan.ToString();
    public Route? route { get; set; }

    public async Task<Route> OptimizeRoute(List<Point> points)
    {
        // Implementation here
        throw new NotImplementedException();
    }
}