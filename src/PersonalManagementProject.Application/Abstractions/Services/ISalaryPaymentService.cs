using PersonalManagementProject.Application.Features.SalaryPayments.DTOs;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;

namespace SnifferApi.Application.Abstractions.Services;

public interface ISalaryPaymentService
{
    Task<IDataResult<PaginatedResponse<SalaryPaymentDto>>> GetAllAsync();
    Task<IDataResult<SalaryPaymentDto>> GetByIdAsync(int id);
    Task<IResult> CreateAsync(CreateSalaryPaymentDto dto);
    Task<IResult> UpdateAsync(UpdateSalaryPaymentDto dto);
    Task<IResult> DeleteAsync(int id);
}

