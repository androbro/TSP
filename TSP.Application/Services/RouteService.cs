using TSP.Application.DTOs;
using TSP.Application.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services;

public class RouteService : IRouteService
{
    public async Task<RouteDto> CalculateRouteAsync(IEnumerable<PointDto> points)
    {
        var startTime = DateTime.Now;
        
        // Convert DTOs to domain entities
        var routePoints = points.Select(p => new Point { X = p.X, Y = p.Y }).ToList();
        
        // Calculate optimal route using chosen algorithm (e.g., 2-opt)
        var optimizedRoute = await OptimizeRoute(routePoints);
        
        // Calculate total distance
        double totalDistance = CalculateTotalDistance(optimizedRoute);
        
        var calculationTime = (DateTime.Now - startTime).TotalSeconds;

        // Map back to DTO
        return new RouteDto
        {
            Points = optimizedRoute.Select(p => new PointDto { X = p.X, Y = p.Y }).ToList(),
            TotalDistance = Math.Round(totalDistance, 2),
            CalculationTime = $"{calculationTime:F1} seconds"
        };
    }

    private async Task<List<Point>> OptimizeRoute(List<Point> points)
    {
        // Implement your route optimization algorithm here (2-opt, nearest neighbor, etc.)
        // This is where the core TSP logic would go
        throw new NotImplementedException();
    }

    private double CalculateTotalDistance(List<Point> points)
    {
        double totalDistance = 0;
        for (int i = 0; i < points.Count - 1; i++)
        {
            var p1 = points[i];
            var p2 = points[i + 1];
            totalDistance += Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
        return totalDistance;
    }
}