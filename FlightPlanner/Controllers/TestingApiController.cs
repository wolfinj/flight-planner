using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController , Authorize]
    public class TestingApiController : ControllerBase
    {
        [Route("clear")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetFlight()
        {
            return Ok("hi from admin api.");
        }
    }
}
