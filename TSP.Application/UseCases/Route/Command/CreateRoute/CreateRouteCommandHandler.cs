using System.ComponentModel.DataAnnotations;
using MediatR;
using TSP.Application.DTOs;
using TSP.Application.Interfaces;

namespace TSP.Application.UseCases.Route.Command.CreateRoute;

public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, RouteDto>
{
    private readonly IRouteService _routeService;

    public CreateRouteCommandHandler(IRouteService routeService)
    {
        _routeService = routeService;
    }
    
public async Task<RouteDto> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
{
    if (request.Points == null || request.Points.Length < 2)
    {
        throw new ValidationException("At least two points are required to create a route");
    }

    // Pass both points and algorithm choice, along with the cancellation token
    var route = await _routeService.CalculateRouteAsync(request.Points, request.Algorithm, cancellationToken);

    return route;
}
}