using PersonalManagementProject.Application.Features.Departmans.DTOs;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;

namespace SnifferApi.Application.Abstractions.Services;

public interface IDepartmanService
{
    Task<IDataResult<PaginatedResponse<DepartmanDto>>> GetAllAsync();
    Task<IDataResult<DepartmanDto>> GetByIdAsync(int id);
    Task<IResult> CreateAsync(CreateDepartmanDto dto);
    Task<IResult> UpdateAsync(UpdateDepartmanDto dto);
    Task<IResult> DeleteAsync(int id);
    Task<IResult> UpdateDepartmanForEmployeeAsync(UpdateDepartmanForEmployeeDto dto);
}


