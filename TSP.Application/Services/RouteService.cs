using TSP.Application.DTOs;
using TSP.Application.Interfaces;
using TSP.Application.Services.RouteOptimization.Common;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services;

public class RouteService : IRouteService
{  
    private readonly IRouteStrategyFactory _strategyFactory;

    public RouteService(IRouteStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }
    
    public async Task<RouteDto> CalculateRouteAsync(IEnumerable<PointDto> points, OptimizationAlgorithm algorithm)
    {
        var startTime = DateTime.Now;
        
        // Get the right strategy based on the algorithm
        var strategy = _strategyFactory.GetStrategy(algorithm);
        
        var routePoints = points.Select(p => new Point { X = p.X, Y = p.Y }).ToList();
        var optimizedRoute = await strategy.OptimizeRoute(routePoints);

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
        // 1. BRUTE FORCE
        // 2. NEAREST NEIGHBOR
        // 3. 2-OPT
        // 4. SIMULATED ANNEALING
        // 5. GENETIC ALGORITHM
        // 6. LIN-KERNIGHAN
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