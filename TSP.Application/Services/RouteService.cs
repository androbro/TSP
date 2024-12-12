using TSP.Application.DTOs;
using TSP.Application.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services;

public class RouteService : IRouteService
{
    public async Task<RouteDto> CalculateSimpleRouteAsync()
    {
        var startTime = DateTime.Now;

        // Simulate processing time
        await Task.Delay(2000);

        // Create some sample points
        var points = new List<Point>
        {
            new() { X = 0, Y = 0 },
            new() { X = 3, Y = 4 },
            new() { X = 6, Y = 8 }
        };

        // Calculate total distance (using Euclidean distance)
        double totalDistance = 0;
        for (int i = 0; i < points.Count - 1; i++)
        {
            var p1 = points[i];
            var p2 = points[i + 1];
            totalDistance += Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        var calculationTime = (DateTime.Now - startTime).TotalSeconds;

        // Map to DTO
        return new RouteDto
        {
            Points = points.Select(p => new PointDto { X = p.X, Y = p.Y }).ToList(),
            TotalDistance = Math.Round(totalDistance, 2),
            CalculationTime = $"{calculationTime:F1} seconds"
        };
    }
}