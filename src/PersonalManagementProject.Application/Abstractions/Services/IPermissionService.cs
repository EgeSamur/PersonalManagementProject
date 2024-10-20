using PersonalManagementProject.Application.Features.Auth.Permissions.DTOs;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;

namespace SnifferApi.Application.Abstractions.Services;

public interface IPermissionService
{
    Task<IDataResult<PaginatedResponse<PermissionDto>>> GetAllAsync();
    Task<IDataResult<PermissionDto>> GetByIdAsync(int id);
    Task<IResult> CreateAsync(CreatePermissionDto dto);
    Task<IResult> AssignPermissionToEmployeeAsync(AssignPermissionToEmployeeDto dto);
    Task<IResult> UpdateAsync(UpdatePermissionDto dto);
    Task<IResult> DeleteAsync(int id);
}