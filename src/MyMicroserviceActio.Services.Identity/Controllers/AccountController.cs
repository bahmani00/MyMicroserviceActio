using System.Threading.Tasks;
using MyMicroserviceActio.Common.Commands;
using MyMicroserviceActio.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyMicroserviceActio.Services.Identity.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}