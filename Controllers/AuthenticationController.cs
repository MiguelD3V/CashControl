using Cashcontrol.API.Models.Dtos.User;
using Cashcontrol.API.Services.Validators.Interfaces;
using Cashcontrol.API.Services.Workers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cashcontrol.API.Controllers
{
    [ApiController]
    [Route("CashControl/api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IUserService _service;
        public AuthenticationController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("Registrer")]
        public async Task<ActionResult> Register([FromBody] RegistrerRequestDto user)
        {  
            var registeredUser = await _service.RegisterAsync(user);
            
            return Ok(registeredUser);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto user)
        {
            var loggedInUser = await _service.LoginAsync(user);
            return Ok(loggedInUser);
        }
    }
}
