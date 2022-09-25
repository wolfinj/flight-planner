using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController , Authorize]
    public class AdminApiController : ControllerBase
    {
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlightById(id);
            if (flight==null) return  NotFound("Flight not found.");

            var flightJson = JsonConvert.SerializeObject(flight);
            
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
            return Created("Flight created.",flight);
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
