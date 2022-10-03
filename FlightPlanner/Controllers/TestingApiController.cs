using FlightPlanner.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

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
        
        return Ok("Db cleared.");
    }
}
