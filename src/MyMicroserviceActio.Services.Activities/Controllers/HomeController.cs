using Microsoft.AspNetCore.Mvc;

namespace MyMicroserviceActio.Services.Activities.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from MyMicroserviceActio.Services.Activites API!");
    }
}