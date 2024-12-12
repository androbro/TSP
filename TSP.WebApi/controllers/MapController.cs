using MediatR;
using Microsoft.AspNetCore.Mvc;
using TSP.Application.DTOs;
using TSP.Application.UseCases.Map.Commands.CreateMap;

namespace TSP.WebApi.controllers;

[ApiController]
[Route("api/[controller]")]
public class MapController: Controller
{
    private readonly IMediator _mediator;

    public MapController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("generate")]
    public async Task<ActionResult<List<PointDto>>> GenerateMap([FromBody] CreateMapCommand command)
    {
        var points = await _mediator.Send(command);
        return Ok(points);
    }
}