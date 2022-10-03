using FlightPlanner.Exceptions;
using FlightPlanner.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("admin-api/flights")]
[ApiController, Authorize]
public class AdminApiController : ControllerBase
{
    private readonly FlightPlannerDbContext _context;

    public AdminApiController(FlightPlannerDbContext context)
    {
        _context = context;
    }

    [Route("{id:int}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = FlightStorage.GetFlightById(id, _context);

        if (flight == null) return NotFound("Flight not found.");

        return Ok(flight);
    }

    [Route("")]
    [HttpPut]
    public IActionResult PutFlight(Flight flight)
    {
        try
        {
            flight = FlightStorage.AddFlight(flight, _context);
        }
        catch (FlightAlreadyExistException e)
        {
            return Conflict(e.Message);
        }
        catch (FlightIsNotValidException e)
        {
            return BadRequest(e.Message);
        }

        return Created("Flight created.", flight);
    }

    [Route("{id:int}")]
    [HttpDelete]
    public IActionResult DelFlight(int id)
    {
        FlightStorage.DeleteFlightById(id, _context);

        return Ok();
    }
}
