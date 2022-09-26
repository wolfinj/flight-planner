using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApiController : ControllerBase
{
    [Route("airports")]
    [HttpGet]
    public IActionResult GetAirports(string search)
    {
        var result = FlightStorage.GetAirportsByKeyword(search);

        return Ok(result);
    }

    [Route("flights/{id}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = FlightStorage.GetFlightById(id);
        if (flight == null) return NotFound("Flight not found.");

        return Ok(flight);
    }

    [Route("flights/search")]
    [HttpPost]
    public IActionResult PutFlight(SearchFlightsRequest flight)
    {
        if (flight.From == flight.To) return BadRequest();

        PageResult result = FlightStorage.SearchFlight(flight);

        return Ok(result);
    }
}
