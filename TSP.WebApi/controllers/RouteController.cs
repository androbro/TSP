using MediatR;
using Microsoft.AspNetCore.Mvc;
using TSP.Application.DTOs;
using TSP.Application.Interfaces;
using TSP.Application.UseCases.Route.Command.CreateRoute;

namespace TSP.WebApi.controllers;

[ApiController]
[Route("api/[controller]")]
public class RouteController: Controller
{
    private readonly IMediator _mediator;

    public RouteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("calculate")]
    public async Task<ActionResult<RouteDto>> CalculateRoute([FromBody] CreateRouteCommand createRouteCommand, CancellationToken cancellationToken)
    {
        var route = await _mediator.Send(createRouteCommand, cancellationToken);
        return Ok(route);
    }
}