using PersonalManagementProject.Application.Features.Auth.Roles.DTOs;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;

namespace SnifferApi.Application.Abstractions.Services;

public interface IRoleService
{
    Task<IDataResult<PaginatedResponse<RoleDto>>> GetAllAsync();
    Task<IDataResult<RoleDto>> GetByIdAsync(int id);
    Task<IResult> CreateAsync(CreateRoleDto dto);
    Task<IResult> AssignRoleToEmployeeAsync(AssignRoleToEmployeeDto dto);
    Task<IResult> AssignPermissionToRoleAsync(AssignPermissionToRoleDto dto);
    Task<IResult> UpdateAsync(UpdateRoleDto dto);
    Task<IResult> DeleteAsync(int id);
}
