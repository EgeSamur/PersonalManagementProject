using Microsoft.AspNetCore.Mvc;
using PersonalManagementProject.Application.Features.Auth.Auth.DTOs;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> MeAsync(int id)
        {
            var result = await _service.Me(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _service.Login(dto);
            return Ok(result);
        }

    }
}
