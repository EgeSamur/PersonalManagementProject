using PersonalManagementProject.Application.Features.Employees.DTOs;
using PersonalManagementProject.Shared.Utils.Results.Abstract;

namespace SnifferApi.Application.Abstractions.Services;

public interface IEmployeeService
{
    //Task<IDataResult<PaginatedResponse<EmpolyeeDto>>> GetAllAsync();
    //Task<IDataResult<EmpolyeeDto>> GetByIdAsync(int id);
    Task<IResult> CreateAsync(CreateEmployeeDto dto);
    Task<IResult> UpdateAsync(UpdateEmployeeDto dto);
    Task<IResult> DeleteAsync(int id);
}


