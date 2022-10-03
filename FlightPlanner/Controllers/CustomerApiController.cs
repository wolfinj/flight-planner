using FlightPlanner.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApiController : ControllerBase
{
    private readonly FlightPlannerDbContext _context;
        
    public CustomerApiController(FlightPlannerDbContext context)
    {
        _context = context;
    }
    
    [Route("airports")]
    [HttpGet]
    public IActionResult GetAirports(string search)
    {
        var result = FlightStorage.GetAirportsByKeyword(search, _context);

        return Ok(result);
    }

    [Route("flights/{id}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = FlightStorage.GetFlightById(id, _context);
        if (flight == null) return NotFound("Flight not found.");

        return Ok(flight);
    }

    [Route("flights/search")]
    [HttpPost]
    public IActionResult PutFlight(SearchFlightsRequest flight)
    {
        if (flight.From == flight.To) return BadRequest();

        PageResult result = FlightStorage.SearchFlight(flight, _context);

        return Ok(result);
    }
}
