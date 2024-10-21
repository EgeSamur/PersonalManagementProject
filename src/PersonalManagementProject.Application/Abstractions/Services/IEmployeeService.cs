using PersonalManagementProject.Application.Features.Employees.DTOs;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;

namespace SnifferApi.Application.Abstractions.Services;

public interface IEmployeeService
{
    Task<IDataResult<PaginatedResponse<EmployeeDto>>> GetAllAsync();
    Task<IDataResult<EmployeeDto>> GetByIdAsync(int id);
    Task<IResult> CreateAsync(CreateEmployeeDto dto);
    Task<IResult> UpdateAsync(UpdateEmployeeDto dto);
    Task<IResult> DeleteAsync(int id);
}


