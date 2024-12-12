using Microsoft.AspNetCore.Mvc;
using TSP.Application.DTOs;
using TSP.Application.Interfaces;

namespace TSP.WebApi.controllers;

[ApiController]
[Route("api/[controller]")]
public class RouteController: Controller
{
    private readonly IRouteService _routeService;

    public RouteController(IRouteService routeService)
    {
        _routeService = routeService;
    }

    [HttpGet("calculate")]
    public async Task<ActionResult<RouteDto>> CalculateRoute()
    {
        var result = await _routeService.CalculateSimpleRouteAsync();
        return Ok(result);
    }
}