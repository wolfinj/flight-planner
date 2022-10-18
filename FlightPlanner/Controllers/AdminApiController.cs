using FlightPlanner.Core.Exceptions;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("admin-api/flights")]
[ApiController, Authorize]
public class AdminApiController : ControllerBase
{
    private readonly IFlightService _flightService;

    public AdminApiController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    [HttpGet]
    public IActionResult GetAllFlights()
    {
        return Ok(_flightService.GetAll());
    }
    
    [Route("{id:int}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = _flightService.GetCompleteFlightById(id);

        if (flight == null) return NotFound("Flight not found.");

        return Ok(flight);
    }

    [Route("")]
    [HttpPut]
    public IActionResult PutFlight(Flight flight)
    {
        IServiceResult newFlight;
        try
        {
            newFlight=_flightService.AddFlight(flight);
        }
        catch (FlightAlreadyExistException e)
        {
            return Conflict(e.Message);
        }
        catch (FlightIsNotValidException e)
        {
            return BadRequest(e.Message);
        }

        if (newFlight.Success)
        {
            var uri = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}/{flight.Id}";
            return Created(uri, newFlight.Entity);
        }

        return Problem(newFlight.FormattedErrors);
    }

    [Route("{id:int}")]
    [HttpDelete]
    public IActionResult DelFlight(int id)
    {
        var flight = _flightService.GetById(id);
        if (flight!=null)
        {
            _flightService.Delete(flight);
            
        }
        return Ok();
    }
}
