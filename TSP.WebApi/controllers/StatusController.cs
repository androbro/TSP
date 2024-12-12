using Microsoft.AspNetCore.Mvc;

namespace TSP.WebApi.controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController: Controller
{
        [HttpGet]
        public IActionResult Get()
        {
            var status = new
            {
                Status = "Operational",
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0",
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"
            };
        
            return Ok(status);
        }

}