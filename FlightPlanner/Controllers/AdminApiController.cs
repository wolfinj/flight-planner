using FlightPlanner.Exceptions;
using FlightPlanner.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        
        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }
        
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlightById(id, _context);
            // var flight = _context.Flights
            //     .Include(f=>f.From)
            //     .Include(f=>f.To)
            //     .FirstOrDefault(f => f.Id == id);
            
            if (flight == null) return NotFound("Flight not found.");

            return Ok(flight);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            try
            {
                flight = FlightStorage.AddFlight(flight, _context);
                // _context.Flights.Add(flight);
                // _context.SaveChanges();
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
            FlightStorage.DeleteFlightById(id, _context);
            
            // var flight = _context.Flights.FirstOrDefault(f => f.Id == id);
            // if (flight != null)
            // {
            //     _context.Flights.Remove(flight);
            //     _context.SaveChanges();
            //
            // }

            return Ok();
        }
    }
}
