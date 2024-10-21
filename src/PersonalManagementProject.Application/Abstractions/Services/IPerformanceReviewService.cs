using PersonalManagementProject.Application.Features.PerformanceReviews.DTOs;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;

namespace SnifferApi.Application.Abstractions.Services;

public interface IPerformanceReviewService
{
    Task<IDataResult<PaginatedResponse<PerformanceReviewDto>>> GetAllAsync();
    Task<IDataResult<PerformanceReviewDto>> GetByIdAsync(int id);
    Task<IResult> CreateAsync(CreatePerformanceReviewDto dto);
    Task<IResult> UpdateAsync(UpdatePerformanceReviewDto dto);
    Task<IResult> DeleteAsync(int id);
}


