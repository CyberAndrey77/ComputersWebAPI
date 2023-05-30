using Computers.Models.Dto;
using Computers.Services;
using Computers.Attributes;
using Microsoft.AspNetCore.Mvc;
using Computers.Models;
using Computers.Repository;

namespace Computers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class ComputerController : ControllerBase
    {
        private readonly IComputerService _computerService;
        private readonly IAuthorizationRepository _authUsers;
        public ComputerController(IComputerService computerService, IAuthorizationRepository authUsers)
        {
            _computerService = computerService;
            _authUsers = authUsers;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateComputer(ComputerDto computer, [FromHeader] Guid accessToken)
        {
            _authUsers.GetAuthorizationUser(accessToken, out var user);
            var answer = await _computerService.CreateComputerAsync(user, computer);
            return answer.StatusCode == System.Net.HttpStatusCode.OK ? Ok(answer) : BadRequest(answer);
        }

        [HttpGet]
        public async Task<IActionResult> GetComputers(int lastId, int count, [FromHeader] Guid accessToken)
        {
            _authUsers.GetAuthorizationUser(accessToken, out var user);
            return Ok(await _computerService.GetComputersAsync(user, lastId, count));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComputer(int id, ComputerDto computer, [FromHeader] Guid accessToken)
        {
            _authUsers.GetAuthorizationUser(accessToken, out var user);
            var answer = await _computerService.UpdateComputerAsync(user, computer, id);
            return answer.StatusCode == System.Net.HttpStatusCode.OK ? Ok(answer) : BadRequest(answer);
        }

        [CustomAuthorize(Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputer(int id, [FromHeader] Guid accessToken) 
        {
            var answer = await _computerService.DeleteComputerAsync(id);
            return answer.StatusCode == System.Net.HttpStatusCode.OK ? Ok(answer) : BadRequest(answer);
        }
    }
}
