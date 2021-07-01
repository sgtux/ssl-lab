using Microsoft.AspNetCore.Mvc;

namespace Storage.Controllers
{
    [ApiController]
    [Route("/")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Welcome to Storage Server!");
    }
}
