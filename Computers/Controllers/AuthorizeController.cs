using Computers.Models.Dto;
using Computers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Computers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthorizeController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var answer = await _loginService.LoginAsync(model);
            if (answer.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(answer);
            }
            return BadRequest(answer);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration(RegistrationDto model)
        {
            var answer =  await _loginService.RegistrationAsync(model);
            if (answer.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(answer);
            }
            return BadRequest(answer);
        }
    }
}
