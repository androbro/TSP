using TSP.Domain.Enums;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class SimulatedAnnealingStrategy: IRouteOptimizationStrategy
{
    public string algorithmName => OptimizationAlgorithm.SimulatedAnnealing.ToString();
    public Route? route { get; set; }
    
    public Route OptimizeRoute(List<Point> points, CancellationToken cancellationToken)
    {
        // Implementation here
        throw new NotImplementedException();
    }
}