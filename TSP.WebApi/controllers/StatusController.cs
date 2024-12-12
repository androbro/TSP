using Microsoft.AspNetCore.Mvc;

namespace TSP.WebApi.controllers;

[ApiController]
[Route("TSP/[controller]")]
public class StatusController: Controller
{
        public StatusController(){}
        
        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok();
        }

}