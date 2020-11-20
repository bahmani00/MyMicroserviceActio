using Microsoft.AspNetCore.Mvc;

namespace MyMicroserviceActio.Api.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from MyMicroserviceActio API!");
    }
}