using TSP.Application.DTOs;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Interfaces;

public interface IRouteOptimizationStrategy
{
    Task<Route> OptimizeRoute(List<Point> points);
    string algorithmName { get; }
    Route? route { get; set; }
}