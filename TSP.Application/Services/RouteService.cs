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

  public async Task<RouteDto> CalculateRouteAsync(
    IEnumerable<PointDto> points,             // Input points to calculate route
    OptimizationAlgorithmDto algorithmDto,    // Which algorithm to use
    CancellationToken cancellationToken)      // For cancelling the operation
{
    // 1. Create a linked cancellation token that includes our own timeout
    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
    cts.CancelAfter(TimeSpan.FromMinutes(5)); // Auto-cancel after 5 minutes

    var startTime = DateTime.Now;  // Track when we started
    IRouteOptimizationStrategy strategy = null;  // Will hold our algorithm strategy

    try
    {
        // 2. Get the appropriate algorithm strategy (like BruteForce, NearestNeighbor, etc.)
        strategy = _strategyFactory.GetStrategy(algorithmDto);
        
        // 3. Convert input DTOs to domain Points
        var routePoints = points.Select(p => new Point(p.X, p.Y)).ToList();

        // 4. Set up cancellation registration
        using var registration = cancellationToken.Register(() => 
        {
            // When the original token is cancelled, also cancel our timeout token
            cts.Cancel();
        });

        // 5. Run the heavy computation on a background thread
        var result = await Task.Run(() =>
        {
            // Do the actual route optimization
            var optimizedRoute = strategy.OptimizeRoute(routePoints, cts.Token);
            
            // Check if we were cancelled after computation
            cts.Token.ThrowIfCancellationRequested();
            
            return optimizedRoute;
        }, cts.Token);  // Can be cancelled before starting the Task

        // 6. Calculate how long it took
        var calculationTime = (DateTime.Now - startTime).TotalMilliseconds;

        // 7. Convert the result back to a DTO
        return MapToRouteDto(result, calculationTime);
    }
    finally
    {
        // 8. Cleanup
        // If our strategy implements IDisposable, dispose it
        if (strategy is IDisposable disposable)
        {
            disposable.Dispose();
        }
        // Try to free up memory
        GC.Collect();
    }
}

    private static RouteDto MapToRouteDto(Route optimizedRoute, double calculationTime)
    {
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