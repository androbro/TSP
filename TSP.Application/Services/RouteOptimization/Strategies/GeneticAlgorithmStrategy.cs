using TSP.Domain.Enums;
using TSP.Application.Services.RouteOptimization.Interfaces;
using TSP.Domain.Entities;

namespace TSP.Application.Services.RouteOptimization.Strategies;

public class GeneticAlgorithmStrategy: IRouteOptimizationStrategy
{
    public string algorithmName => OptimizationAlgorithm.GeneticAlgorithm.ToString();
    public Route? route { get; set; }
    
    public Route OptimizeRoute(List<Point> points)
    {
        // Implementation here
        throw new NotImplementedException();
    }
}