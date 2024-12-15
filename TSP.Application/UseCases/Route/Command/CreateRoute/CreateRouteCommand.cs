using MediatR;
using TSP.Application.DTOs;
using TSP.Application.Services.RouteOptimization.Common;

namespace TSP.Application.UseCases.Route.Command.CreateRoute;



public class CreateRouteCommand : IRequest<RouteDto>
{
    public PointDto[] Points { get; set; }
    public OptimizationAlgorithm Algorithm { get; set; }
}