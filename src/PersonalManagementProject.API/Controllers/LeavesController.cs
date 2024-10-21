using Microsoft.AspNetCore.Mvc;
using PersonalManagementProject.Application.Features.Leaves.DTOs;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private readonly ILeaveService _service;

        public LeavesController(ILeaveService service)
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateLeaveDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateLeaveDto dto)
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
