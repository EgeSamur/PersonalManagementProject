using PersonalManagementProject.Application.Features.Leaves.DTOs;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;

namespace SnifferApi.Application.Abstractions.Services;

public interface ILeaveService
{
    Task<IDataResult<PaginatedResponse<LeaveDto>>> GetAllAsync();
    Task<IDataResult<LeaveDto>> GetByIdAsync(int id);
    Task<IResult> CreateAsync(CreateLeaveDto dto);
    Task<IResult> UpdateAsync(UpdateLeaveDto dto);
    Task<IResult> DeleteAsync(int id);
}


