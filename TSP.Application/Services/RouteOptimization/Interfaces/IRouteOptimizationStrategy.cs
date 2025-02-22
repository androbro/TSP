using TSP.Application.DTOs;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Interfaces;

public interface IRouteOptimizationStrategy
{
    Route OptimizeRoute(List<Point> points, CancellationToken cancellationToken);
    string algorithmName { get; }
    Route? route { get; set; }
}