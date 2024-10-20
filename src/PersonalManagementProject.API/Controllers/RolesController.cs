using Microsoft.AspNetCore.Mvc;
using PersonalManagementProject.Application.Features.Auth.Roles.DTOs;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRoleDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AssignPermissionToRoleAsync([FromBody] AssignPermissionToRoleDto dto)
        {
            var result = await _service.AssignPermissionToRoleAsync(dto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToEmployeeAsync([FromBody] AssignRoleToEmployeeDto dto)
        {
            var result = await _service.AssignRoleToEmployeeAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateRoleDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
    }
}
