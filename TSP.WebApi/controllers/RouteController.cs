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
        try 
        {
            //seems swagger doesnt trigger the cancellation token
            Console.WriteLine("Starting calculation..."); // Debug log
        
            cancellationToken.Register(() => 
            {
                Console.WriteLine("Cancellation was triggered at controller level"); // Debug log
            });
        
            var route = await _mediator.Send(createRouteCommand, cancellationToken);
            return Ok(route);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Caught cancellation in controller"); // Debug log
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return StatusCode(499, "Client Closed Request");
        }
    }
}