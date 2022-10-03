using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    
    
    [Route("testing-api")]
    [ApiController , Authorize]
    public class TestingApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        
        public TestingApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }
        
        [Route("clear")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetFlight()
        {
            FlightStorage.Clear(_context);
            // _context.Flights.RemoveRange(_context.Flights);
            // _context.Airports.RemoveRange(_context.Airports);
            // _context.SaveChanges();
            return Ok("Db cleared.");
        }
    }
}
