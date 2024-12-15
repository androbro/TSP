using TSP.Application.DTOs;
using TSP.Application.Services.RouteOptimization.Common;

namespace TSP.Application.Interfaces;

public interface IRouteService
{
    Task<RouteDto> CalculateRouteAsync(IEnumerable<PointDto> points, OptimizationAlgorithm algorithm);
}