using TSP.Application.DTOs;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Interfaces;

public interface IRouteOptimizationStrategy
{
    Task<List<Point>> OptimizeRoute(List<Point> points);
    string AlgorithmName { get; }
}