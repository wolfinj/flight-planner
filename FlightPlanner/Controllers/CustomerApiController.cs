using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApiController : ControllerBase
{
    private readonly IFlightService _flightService;

    public CustomerApiController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    [Route("airports")]
    [HttpGet]
    public IActionResult GetAirports(string search)
    {
        var result = _flightService.GetAirportsByKeyword(search);
        
        return Ok(result);
    }

    [Route("flights/{id:int}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = _flightService.GetCompleteFlightById(id);
        
        if (flight == null) return NotFound("Flight not found.");

        return Ok(flight);
    }

    [Route("flights/search")]
    [HttpPost]
    public IActionResult PutFlight(SearchFlightsRequest flight)
    {
        if (flight.From == flight.To) return BadRequest();

        var result = _flightService.SearchFlight(flight);

        return Ok(result);
    }
}
