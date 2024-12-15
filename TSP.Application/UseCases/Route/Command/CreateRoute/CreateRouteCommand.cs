using MediatR;
using TSP.Application.DTOs;

namespace TSP.Application.UseCases.Route.Command.CreateRoute;



public class CreateRouteCommand : IRequest<RouteDto>
{
    public PointDto[] Points { get; set; }
    public OptimizationAlgorithmDto Algorithm { get; set; }
}