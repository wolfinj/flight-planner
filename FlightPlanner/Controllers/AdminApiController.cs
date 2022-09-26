using FlightPlanner.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlightById(id);
            if (flight == null) return NotFound("Flight not found.");

            return Ok(flight);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            try
            {
                flight = FlightStorage.AddFlight(flight);
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

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DelFlight(int id)
        {
            FlightStorage.DeleteFlightById(id);

            return Ok();
        }
    }
}
