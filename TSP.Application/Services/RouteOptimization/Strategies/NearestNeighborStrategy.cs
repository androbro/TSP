using TSP.Application.DTOs;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class NearestNeighborStrategy : IRouteOptimizationStrategy
{
    public string AlgorithmName => OptimizationAlgorithmDto.NearestNeighbor.ToString();

    public async Task<List<Point>> OptimizeRoute(List<Point> points)
    {
        // Implementation here
        throw new NotImplementedException();
    }
}