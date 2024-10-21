using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalManagementProject.Application.Features.PerformanceReviews.DTOs;
using PersonalManagementProject.Application.Features.SalaryPayments.DTOs;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalaryPaymentsController : ControllerBase
    {
        private readonly ISalaryPaymentService _service;

        public SalaryPaymentsController(ISalaryPaymentService service)
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateSalaryPaymentDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateSalaryPaymentDto dto)
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
