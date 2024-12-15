using TSP.Application.DTOs;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class NearestNeighborStrategy : IRouteOptimizationStrategy
{
    public string algorithmName => OptimizationAlgorithmDto.NearestNeighbor.ToString();
    public Route? route { get; set; }
    
    public async Task<Route> OptimizeRoute(List<Point> points)
    {
        // Implementation here
        throw new NotImplementedException();
    }
}