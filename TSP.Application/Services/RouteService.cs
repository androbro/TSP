using TSP.Application.DTOs;
using TSP.Application.Interfaces;
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

    public async Task<RouteDto> CalculateRouteAsync(IEnumerable<PointDto> points, OptimizationAlgorithmDto algorithmDto,
        CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        // Get the right strategy based on the algorithm
        var strategy = _strategyFactory.GetStrategy(algorithmDto);

        var routePoints = points.Select(p => new Point(p.X, p.Y)).ToList();
        
        // fix cancellation token
        var optimizedRoute = await Task.Run(() => strategy.OptimizeRoute(routePoints), cancellationToken);

        var calculationTime = (DateTime.Now - startTime).TotalSeconds;

        // Map back to DTO
        return new RouteDto
        {
            Points = optimizedRoute.Points.Select(p => new PointDto { X = p.X, Y = p.Y, Id = p.Id }).ToList(),
            TotalDistance = optimizedRoute.TotalDistance,
            CalculationTime = $"{calculationTime} ms",
            Connections = optimizedRoute.Connections
                .Select(c => new ConnectionDto
                {
                    IsOptimal = c.IsOptimal,
                    Distance = c.Distance,
                    FromPoint = c.FromPoint,
                    ToPoint = c.ToPoint
                })
                .ToList()
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
}