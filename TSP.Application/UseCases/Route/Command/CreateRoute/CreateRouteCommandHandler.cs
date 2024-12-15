using System.ComponentModel.DataAnnotations;
using TSP.Application.DTOs;
using TSP.Application.Interfaces;

namespace TSP.Application.UseCases.Route.Command.CreateRoute;

public class CreateRouteCommandHandler
{
    private readonly IRouteService _routeService;

    public CreateRouteCommandHandler(IRouteService routeService)
    {
        _routeService = routeService;
    }
    
    public async Task<RouteDto> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
    {
        // Validate command
        if (request.Points == null || request.Points.Length < 2)
        {
            throw new ValidationException("At least two points are required to create a route");
        }

        // Call service to perform the calculation
        var route = await _routeService.CalculateRouteAsync(request.Points);

        return route;
    }
}