using TSP.Application.DTOs;

namespace TSP.Application.Interfaces;

public interface IRouteService
{
    Task<RouteDto> CalculateRouteAsync(IEnumerable<PointDto> points);
}