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

    public async Task<RouteDto> CalculateRouteAsync(IEnumerable<PointDto> points, OptimizationAlgorithmDto algorithmDto, CancellationToken cancellationToken)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(TimeSpan.FromMinutes(5));

        var startTime = DateTime.Now;
        IRouteOptimizationStrategy strategy = null;

        try
        {
            var tcs = new TaskCompletionSource<Route>();
            using var registration = cts.Token.Register(() => tcs.TrySetCanceled());

            strategy = _strategyFactory.GetStrategy(algorithmDto);
            var routePoints = points.Select(p => new Point(p.X, p.Y)).ToList();

            var computationTask = Task.Run(() =>
            {
                var optimizedRoute = strategy.OptimizeRoute(routePoints, cts.Token);
                cts.Token.ThrowIfCancellationRequested();
                return optimizedRoute;
            }, cts.Token);

            var completedTask = await Task.WhenAny(computationTask, tcs.Task);
            
            if (completedTask.IsCanceled || cts.Token.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }

            var optimizedRoute = await computationTask;
            var calculationTime = (DateTime.Now - startTime).TotalMilliseconds;
            return MapToRouteDto(optimizedRoute, calculationTime);
        }
        catch (OperationCanceledException)
        {
            if (strategy is IDisposable disposable)
            {
                disposable.Dispose();
            }
            GC.Collect();
            throw;
        }
        finally
        {
            if (strategy is IDisposable disposable)
            {
                disposable.Dispose();
            }
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
        throw new NotImplementedException();
    }
}