using Microsoft.AspNetCore.Mvc;

namespace Tools.Controllers.Api
{
    [ApiController]
    [Route("/api/")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}